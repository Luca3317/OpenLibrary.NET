# RecentChanges API 
### [Documentation on the OpenLibrary RecentChanges API](https://openlibrary.org/dev/docs/api/recentchanges)

The RecentChanges API serves to get information about recent changes made to OpenLibrary's data.

For more usage examples of this API, see [MiscellaneousExamples](https://github.com/Luca3317/OpenLibrary.NET/blob/main/examples/MiscellaneousExamples.cs).
***

To make a request to the RecentChanges API, use the OpenLibraryClient
```csharp
OpenLibraryClient client = new OpenLibraryClient();
var parameters = new List<KeyValuePair<string,string>>() { ... };

OLRecentChangesData recentChanges = await client.RecentChanges.GetRecentChangesAsync(year:"2018", month:"08", kind:"merge-authors", parameters);
```

or the static OLRecentChangesLoader methods:
```csharp
HttpClient httpClient = new HttpClient();
OLRecentChangesData recentChanges = await OLRecentChangesLoader.RecentChanges.GetRecentChangesAsync(year:"2018", month:"08", day:"01", parameters);
```

For valid parameters, see the link above.
Alternatively, see [OpenLibraryUtility's Maps](https://github.com/Luca3317/OpenLibrary.NET/blob/main/docs/Utilities.md#Maps).