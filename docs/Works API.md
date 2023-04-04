# Works API 
### [Documentation on the OpenLibrary works API](https://openlibrary.org/dev/docs/api/books)

The Works API serves to get information pertaining to a work and its editions.

For more usage examples of this API, see [WorkAPIExamples](https://github.com/Luca3317/OpenLibrary.NET/blob/main/examples/WorkAPIExamples.cs).
***

OLWork serves to combine storage of the various requests supported by the works API.

It contains [data about the work](https://github.com/Luca3317/OpenLibrary.NET/blob/main/src/OLData/OLAuthorData.cs) as well as its [ratings](https://github.com/Luca3317/OpenLibrary.NET/blob/main/src/OLData/OLRatingsData.cs), [bookshelves](https://github.com/Luca3317/OpenLibrary.NET/blob/main/src/OLData/OLBookshelvesData.cs) and a collection of [its editions](https://github.com/Luca3317/OpenLibrary.NET/blob/main/src/OLData/OLEditionData.cs).

Create a new instance of OLWork using a valid work's OLID:
```csharp
OLWork work = new OLWork("OL8037381W");
```

You can then make requests and populate the object asynchronously.
The requested data will be cached within OLWork and returned.
```csharp
OLWorkData workData = await work.GetDataAsync();  // Request data and returns it
authorData = author.GetDataAsync();               // Returns cached data
authorData = author.Data;                         // Returns cached data

OLRatingsData ratingsData = await work.GetRatingsAsync(); // Request data and returns it
ratingsData = await work.GetRatingsAsync();               // Returns cached data
ratingsData = work.Ratings;                               // Returns cached data

OLBookshelvesData bookshelvesData = await work.GetBookshelvesAsync(); // Request data and returns it
bookshelvesData = await work.GetBookshelvesAsync();                   // Returns cached data
bookshelvesData = work.Bookshelves;                                   // Returns cached data

ReadOnlyCollection<OLEditionData> editions = await author.GetEditionsAsync(10); // Requests 10 works and returns the collection
works = await author.GetWorksAsync(25);                                         // Requests 15 works and returns the collection
works = await author.GetWorksAsync(10);                                         // Returns the cached collection, count 25
works = await author.Works;                                                     // Returns the cached collection, count 25
```
***
Alternatively, you can use the static OLWorkLoader class to make individual requests.
```csharp
var parameters = new List<KeyValuePair<string,string>>() { ... };

OLWorkData workData = await OLWorkLoader.GetDataAsync("OL8037381W");
OLRatingsData ratingsData = await OLWorkLoader.GetRatingsAsync("OL8037381W");
OLBookshelvesData bookshelvesData = await OLWorkLoader.GetBookshelvesAsync("OL8037381W");
OLEditionData[] editionData = await OLWorkLoader.GetEditionsAsync("OL8037381W", parameters);
```

For valid parameters, see the link above.
Alternatively, see [OpenLibraryUtility's Maps](https://github.com/Luca3317/OpenLibrary.NET/blob/main/docs/Utilities.md#Maps).