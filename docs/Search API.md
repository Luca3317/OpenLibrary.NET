# Search API 
### [Documentation on the OpenLibrary search API](https://openlibrary.org/dev/docs/api/search)

The Search API allows to search OpenLibrary for books, authors, subjects and lists.

For more usage examples of this API, see [MiscellaneousExamples](https://github.com/Luca3317/OpenLibrary.NET/blob/main/examples/MiscellaneousExamples.cs)
***

To get a search query's result, use the static OLSearchLoader class:
```csharp
var parameters = new List<KeyValuePair<string,string>>() { ... };

OLWorkData[] works = await OLSearchLoader.GetSearchResultsAsync("the remains of the day", parameters);
OLAuthorData[] authors = await OLSearchLoader.GetAuthorsSearchResult("Kazuo ishiguro", parameters);
OLSubjectData[] subjects = await OLSearchLoader.GetSubjectsSearchResult("butlers", parameters);
OLListData[] lists = await OLSearchLoader.GetListSearchResults("second world war", parameters);
```

For valid parameters, see the link above.
Alternatively, see [OpenLibraryUtility's Maps](https://github.com/Luca3317/OpenLibrary.NET/blob/main/docs/Utilities.md#Maps).