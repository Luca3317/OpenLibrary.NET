# Docs

These docs relate to OpenLibrary.NET.

For documentation on the APIs offered by Open Library, see the [Open Library Developer Center](https://openlibrary.org/developers/api).
***
This is a quick overview of the main classes and concepts of OpenLibrary.NET.

For more information on the individual APIs, see their corresponding documentation file.
### [OpenLibraryClient](https://github.com/Luca3317/OpenLibrary.NET/blob/main/src/OpenLibraryClient.cs)
The main interface of OpenLibrary.NET.

Uses HttpClient under the hood; reuse the OpenLibraryClient instance if you want to reuse the backing HttpClient instance.

Contains methods for making requests to all of the [supported OpenLibrary APIs](https://github.com/Luca3317/OpenLibrary.NET). The methods are also grouped by the API they belong to.
```csharp
OpenLibraryClient client = new OpenLibraryClient();

await client.Work.GetDataAsync("OL257943W");      // Request work data from the Works API
await client.Work.GetRatingsAsync("OL257943W");   // Request ratings from the Works API

await client.Author.GetDataAsync("OL48139A");     // Request author data from the Authors API

await client.Subject.GetDataAsync("magic");       // Request data about the subject from the Subjects API
await client.Subject.GetListsCountAsync("magic"); // Request number of lists that contain this subject from the Subjects API
```
OpenLibraryClient also contains various other, session-related methods, such as login, list creation and list deletion.
```csharp
bool createdList = await client.TryCreateListAsync("My list", "My lists description");
// createdList = false; You need to be logged in to create a list

await client.LoginAsync("email", "password");

createdList = await client.TryCreateListAsync("My list", "My lists description");
// createdList = true, assuming the prior login was successful (in which case it would've thrown)
```
***
### [OLLoader](https://github.com/Luca3317/OpenLibrary.NET/tree/main/src/OLLoader)
If you don't want to use the OpenLibraryClient, you can perform all of the GET requests using the corresponding OLLoader class (OpenLibraryClient merely wraps those functions for most of its methods).

```csharp
HttpClient client = new HttpClient();

OLWork work = new OLWork()
{
  Data = await OLWorkLoader.GetDataAsync(client, id),
  Ratings = await OLWorkLoader.GetRatingsAsync(client, id),
  Editions = await OLWorkLoader.GetEditionsAsync(client, id)
};
```
***
### [OLData](https://github.com/Luca3317/OpenLibrary.NET/tree/main/src/OLData)
Responses to individual requests are stored in the immutable OLData records.

For example, OLWorkData represents a request to the [works API](https://openlibrary.org/dev/docs/api/books) and could be created like so:
```csharp
OLWorkData workData = await OLWorkLoader.GetDataAsync("OL45804W");
// result: https://openlibrary.org/works/OL45804W.json
```
which would result in:
```csharp
OLWorkData workData {
  key = "/works/OL45804W",
  title = "Fantastic Mr. Fox",
  description = "The main character of Fantastic Mr. Fox is an extremely clever anthropomorphized fox named Mr. Fox. He lives ..." ],
  subjects = [ "Animals", "Hunger", "Open Library Staff Picks", "Juvenile Fiction", "Children's stories, English", "Foxes", ... ],
  authors = [ "/authors/OL34184A", "/authors/OL10649522A" ],
  coverKeys = [6498519, 8904777, 108274, 233884, 1119236, -1, 10222599, 10482837, 3216657, 10519563, 10835922, 10835924, ... ]
}
```
All OLData record classes implement JsonExtensionData. Therefore, fields that may not be supported by the class structure can still be accessed.

***
### [OLWork](https://github.com/Luca3317/OpenLibrary.NET/blob/main/src/OLWork.cs), [OLAuthor](https://github.com/Luca3317/OpenLibrary.NET/blob/main/src/OLAuthor.cs), [OLEditon](https://github.com/Luca3317/OpenLibrary.NET/blob/main/src/OLEdition.cs)
Some OpenLibrary APIs allow for various types of requests related to a common object.

For example, the works API can also be used to make requests for the ratings, "bookshelves", and editions of a work.

A request for a work's bookshelves could be created like this:
```csharp
OLBookshelvesData bookshelvesData = await OLWorkLoader.GetBookshelvesAsync("OL45804W");
// result: https://openlibrary.org/works/OL45804W/bookshelves.json
```
which would result in:
```csharp
OLBookshelvesData bookshelvesData {
  want_to_read = 676,
  currently_reading = 54,
  already_read = 133
}
```

Some of these APIs have their own dedicated record class,
which serve to combine storage of these individual, related requests. 

For example, the works API's requests are represented by [OLWork](https://github.com/Luca3317/OpenLibrary.NET/blob/main/src/OLWork.cs).

Using the OpenLibraryClient, you can get a populated OLWork like so:
```csharp
OpenLibraryClient client = new OpenLibraryClient();
string id = "OL45804W";

OLWork work = await client.GetWorkAsync(id, 10);
```
Be aware however that GetWorkAsync will make multiple requests.

If you want to initialize only some of the instance fields, you can populate OLWork this way:
```csharp
OLWork work = new OLWork()
{
  ID = id,
  Data = await client.Work.GetDataAsync(id),
  Ratings = await client.Work.GetRatingsAsync(id)
};
```
Like OLData records, OLWork is immutable. As with any record class, you can create a modified copy with ease:
```csharp
work = work with { Bookshelves = await client.Work.GetBookshelvesAsync(id) };
```
The authors API has its equivalent with the [OLAuthor](https://github.com/Luca3317/OpenLibrary.NET/blob/main/src/OLAuthor.cs) record class.

The editions API has its equivalent with the [OLEditon](https://github.com/Luca3317/OpenLibrary.NET/blob/main/src/OLEdition.cs) record class. OLEdition also has additional methods to get its cover.
