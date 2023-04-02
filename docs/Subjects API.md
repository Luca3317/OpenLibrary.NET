# Subjects API 
### [Documentation on the OpenLibrary editions API](https://openlibrary.org/dev/docs/api/subjects)

The Subjects API serves to get information related to a specific subject, primarily related works.
***

To get a subject's related data, use the static OLSubjectLoader class:
```csharp
var parameters = new List<KeyValuePair<string,string>>() { ... };
OLSubjectData subjectData = await OLSubjectData.GetDataAsync("fantasy", parameters);
```

For valid parameters, see the link above.
Alternatively, see [OpenLibraryUtility](https://github.com/Luca3317/OpenLibrary.NET/blob/main/src/Utility/OpenLibraryUtility.cs).