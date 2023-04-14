# Authors API 
### [Documentation on the OpenLibrary authors API](https://openlibrary.org/dev/docs/api/authors)

The Authors API serves to get information pertaining to an author and their body of work.

For more usage examples of this API, see [AuthorAPIExamples](https://github.com/Luca3317/OpenLibrary.NET/blob/main/examples/AuthorAPIExamples.cs).
***

OLAuthor serves to combine storage of the various requests supported by the authors API.

It contains [data about the author](https://github.com/Luca3317/OpenLibrary.NET/blob/main/src/OLData/OLAuthorData.cs) and a collection of [their works](https://github.com/Luca3317/OpenLibrary.NET/blob/main/src/OLData/OLWorkData.cs).

Get a populated instance of OLAuthor using OpenLibraryClient and a valid author's OLID (be aware this will make multiple requests):
```csharp
OpenLibraryClient client = new OpenLibraryClient();
OLAuthor author = await client.GetAuthorAsync("OL7715527A", 10);
```
***
Alternatively, you can make individual requests using the OpenLibraryClient 
```csharp
var parameters = new List<KeyValuePair<string, string>>() { ... };

OLAuthorData authorData = await client.Author.GetDataAsync("OL7715527A");
OLWorkData[] works = await client.Author.GetWorksAsync("OL7715527A", parameters);
```

or using the static OLAuthorLoader class.
```csharp
OLAuthorData authorData = await OLAuthorLoader.GetDataAsync("OL7715527A");
OLWorkData[] works = await OLAuthorLoader.GetWorksAsync("OL7715527A", parameters);
```
For valid parameters, see the link above.
Alternatively, see [OpenLibraryUtility's Maps](https://github.com/Luca3317/OpenLibrary.NET/blob/main/docs/Utilities.md#Maps).