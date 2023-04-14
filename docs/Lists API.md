# Lists API 
### [Documentation on the OpenLibrary lists API](https://openlibrary.org/dev/docs/api/lists)

The Lists API serves to get user-created lists.

For more usage examples of this API, see [MiscellaneousExamples](https://github.com/Luca3317/OpenLibrary.NET/blob/main/examples/MiscellaneousExamples.cs).
***

To make requests to the list API, either use the OpenLibraryClient
```csharp
OpenLibraryClient client = new OpenLibraryClient();
OLListData listData = await client.List.GetListAsync("username", "listID");
```

or the static OLListLoader class:
```csharp
HttpClient httpClient = new HttpClient();
OLListData listData = await OLListLoader.GetListAsync(httpClient, "username", "listID");
```

You can also get all lists of a user
```csharp
await client.List.GetUserListsAsync("username");
```
or get the editions contained in a list
```csharp
var parameters = new List<KeyValuePair<string,string>>() { ... };
await client.List.GetListEditionsAsync("username", "listID", parameters);
```
For valid parameters, see the link above.
Alternatively, see [OpenLibraryUtility's Maps](https://github.com/Luca3317/OpenLibrary.NET/blob/main/docs/Utilities.md#Maps).
***
When using the OpenLibraryClient, you may also create or delete lists (after logging in).
```csharp
await client.CreateListAsync("listName", "listDescription");
await client.DeleteListAsync("listID");
```
You can of course only delete lists that belong to your account.