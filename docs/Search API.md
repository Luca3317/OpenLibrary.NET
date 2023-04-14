# Search API 
### [Documentation on the OpenLibrary search API](https://openlibrary.org/dev/docs/api/search)

The Search API allows to search OpenLibrary for books, authors, subjects and lists.

For more usage examples of this API, see [MiscellaneousExamples](https://github.com/Luca3317/OpenLibrary.NET/blob/main/examples/MiscellaneousExamples.cs).
***

To get a search query's result, either use the OpenLibraryClient
```csharp
OpenLibraryClient client = new OpenLibraryClient();
var parameters = new List<KeyValuePair<string,string>>() { ... };

OLWorkData[] works = await client.Search.GetSearchResultsAsync("the remains of the day", parameters);
OLAuthorData[] authors = await client.Search.GetAuthorsSearchResultsAsync("Kazuo ishiguro", parameters);
OLSubjectData[] subjects = await client.Search.GetSubjectsSearchResultsAsync("butlers", parameters);
OLListData[] lists = await client.Search.GetListSearchResultsAsync("second world war", parameters);
```

or use the static OLSearchLoader class:
```csharp
HttpClient httpClient = new HttpClient();
var parameters = new List<KeyValuePair<string,string>>() { ... };

OLWorkData[] works = await OLSearchLoader.GetSearchResultsAsync(httpClient, "the remains of the day", parameters);
OLAuthorData[] authors = await OLSearchLoader.GetAuthorsSearchResultsAsync(httpClient, "Kazuo ishiguro", parameters);
OLSubjectData[] subjects = await OLSearchLoader.GetSubjectsSearchResultsAsync(httpClient, "butlers", parameters);
OLListData[] lists = await OLSearchLoader.GetListSearchResultsAsync(httpClient, "second world war", parameters);
```

For valid parameters, see the link above.
Alternatively, see [OpenLibraryUtility's Maps](https://github.com/Luca3317/OpenLibrary.NET/blob/main/docs/Utilities.md#Maps).