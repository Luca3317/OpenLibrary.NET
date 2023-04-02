# Authors API 
### [Documentation on the OpenLibrary authors API](https://openlibrary.org/dev/docs/api/authors)

The Authors API serves to get information pertaining to an author and their body of work.
***

OLAuthor serves to combine storage of the various requests supported by the authors API.

It contains [data about the author](https://github.com/Luca3317/OpenLibrary.NET/blob/main/src/OLData/OLAuthorData.cs) and a collection of [their works](https://github.com/Luca3317/OpenLibrary.NET/blob/main/src/OLData/OLWorkData.cs).

Create a new instance of OLAuthor using a valid author's OLID:
```csharp
OLAuthor author = new OLAuthor("OL7715527A");
```

You can then make requests and populate the object asynchronously.
The requested data will be cached within OLAuthor and returned.
```csharp
OLAuthorData authorData = await author.GetDataAsync();  // Request data and returns it
authorData = await author.GetDataAsync();               // Returns cached data

ReadOnlyCollection<OLWorkData> works = await author.GetWorksAsync(10);  // Requests 10 works and returns the collection
works = await author.GetWorksAsync(25);                                 // Requests 15 works and returns the collection
works = await author.GetWorksAsync(10);                                 // Returns the cached collection
```
***
Alternatively, you can use the static OLAuthorLoader class to make individual requests.
```csharp
var parameters = new List<KeyValuePair<string, string>>() { ... };

OLAuthorData authorData = await OLAuthorLoader.GetDataAsync("OL7715527A");
OLWorkData[] works = await OLAuthorLoader.GetWorksAsync("OL7715527A", parameters);
```

For valid parameters, see the link above.
Alternatively, see [OpenLibraryUtility's Maps](https://github.com/Luca3317/OpenLibrary.NET/blob/main/docs/Utilities.md#Maps).