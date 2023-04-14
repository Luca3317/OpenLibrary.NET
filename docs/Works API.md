# Works API 
### [Documentation on the OpenLibrary works API](https://openlibrary.org/dev/docs/api/books)

The Works API serves to get information pertaining to a work and its editions.

For more usage examples of this API, see [WorkAPIExamples](https://github.com/Luca3317/OpenLibrary.NET/blob/main/examples/WorkAPIExamples.cs).
***

OLWork serves to combine storage of the various requests supported by the works API.

It contains [data about the work](https://github.com/Luca3317/OpenLibrary.NET/blob/main/src/OLData/OLAuthorData.cs) as well as its [ratings](https://github.com/Luca3317/OpenLibrary.NET/blob/main/src/OLData/OLRatingsData.cs), [bookshelves](https://github.com/Luca3317/OpenLibrary.NET/blob/main/src/OLData/OLBookshelvesData.cs) and a collection of [its editions](https://github.com/Luca3317/OpenLibrary.NET/blob/main/src/OLData/OLEditionData.cs).


Get a populated instance of OLWork using OpenLibraryClient and a valid works's OLID (be aware this will make multiple requests):
```csharp
OpenLibraryClient client = new OpenLibraryClient();
OLWork work = await client.GetWorkAsync("OL257943W", 10);
```
***
Alternatively, you can make individual requests using the OpenLibraryClient 
```csharp
var parameters = new List<KeyValuePair<string, string>>() { ... };

OLWorkData workData = await client.Work.GetDataAsync("OL257943W");
OLEditionData[] editions = await client.Work.GetEditionsAsync("OL257943W", parameters);
OLRatingsData ratings = await client.Work.GetRatingsAsync("OL257943W");
OLBookshelvesData bookshelves = await client.Work.GetBookshelvesAsync("OL257943W");
```

or using the static OLWorkLoader class.
```csharp
HttpClient httpClient = new HttpClient();

OLWorkData workData = await OLWorkLoader.GetDataAsync(httpClient, "OL257943W");
OLEditionData[] editions = await OLWorkLoader.GetEditionsAsync(httpClient, "OL257943W", parameters);
OLRatingsData ratings = await OLWorkLoader.GetRatingsAsync(httpClient, "OL257943W");
OLBookshelvesData bookshelves = await OLWorkLoader.GetBookshelvesAsync(httpClient, "OL257943W");
```
For valid parameters, see the link above.
Alternatively, see [OpenLibraryUtility's Maps](https://github.com/Luca3317/OpenLibrary.NET/blob/main/docs/Utilities.md#Maps).