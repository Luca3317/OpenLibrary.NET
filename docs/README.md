# Docs

These docs relate to OpenLibrary.NET.

For documentation on the APIs offered by Open Library, see the [Open Library Developer Center](https://openlibrary.org/developers/api).
***
This is a quick overview of the main classes and concepts of OpenLibrary.NET.

For more information on the individual APIs, see their corresponding documentation file.

### OLData
Responses to individual requests are stored in the immutable OLData records (see the [OLData folder](https://github.com/Luca3317/OpenLibrary.NET/tree/main/src/OLData)).

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
### OLWork, OLAuthor, OLEdition
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

These APIs each have their own dedicated record class,
which serve to combine storage of the individual, related requests. 

For example, the works API's requests are represented by [OLWork](https://github.com/Luca3317/OpenLibrary.NET/blob/main/src/OLWork.cs).

You can create an OLWork instance like so:
```csharp
OLWork work = new OLWork("OL45804W");
```
Like OLData records, the fields in OLWork have no public setters. They can however be populated asynchronously:
```csharp
Console.WriteLine(work.Data == null); // true
OLWorkData workData = await work.GetDataAsync(); // This call is equivalent to the OLData example
Console.WriteLine(work.Data == null); // false
```
The authors API has its equivalent with the [OLAuthor](https://github.com/Luca3317/OpenLibrary.NET/blob/main/src/OLAuthor.cs) record class.

The editions API has its equivalent with the [OLEditon](https://github.com/Luca3317/OpenLibrary.NET/blob/main/src/OLEdition.cs) record class. OLEdition also has additional methods to get its cover.
