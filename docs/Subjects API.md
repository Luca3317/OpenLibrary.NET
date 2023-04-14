# Subjects API 
### [Documentation on the OpenLibrary subjects API](https://openlibrary.org/dev/docs/api/subjects)

The Subjects API serves to get information related to a specific subject, primarily related works.

For more usage examples of this API, see [MiscellaneousExamples](https://github.com/Luca3317/OpenLibrary.NET/blob/main/examples/MiscellaneousExamples.cs).
***

To get a subject's related data, use the OpenLibraryClient
```csharp
OpenLibraryClient client = new OpenLibraryClient();
var parameters = new List<KeyValuePair<string,string>>() { ... };

OLSubjectData subjectData = await client.Subject.GetDataAsync("fantasy", parameters);
```

or the static OLSubjectLoader class:
```csharp
HttpClient httpClient = new HttpClient();
OLSubjectData subjectData = await OLSubjectLoader.GetDataAsync(httpClient, "fantasy", parameters);
```

For valid parameters, see the link above.
Alternatively, see [OpenLibraryUtility's Maps](https://github.com/Luca3317/OpenLibrary.NET/blob/main/docs/Utilities.md#Maps).