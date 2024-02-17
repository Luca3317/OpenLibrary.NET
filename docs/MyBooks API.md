# MyBooks API 
### [Documentation on the OpenLibrary MyBooks API](https://openlibrary.org/dev/docs/api/mybooks)

The MyBooks API serves to get information about a user's various reading logs (Want-To-Read, Currently-Reading and Already-Read).
***

You may get the reading logs of a public user using their username.
To make a request to the MyBooks API, use the OpenLibraryClient:
```csharp
OpenLibraryClient client = new OpenLibraryClient();
OLMyBooksData myBooksData = await client.MyBooks.GetCurrentlyReadingAsync("username-here");
OLMyBooksData myBooksData = await client.MyBooks.GetAlreadReadAsync("username-here");
OLMyBooksData myBooksData = await client.MyBooks.GetWantToReadAsync("username-here");
```

or the static OLMyBooksLoader methods:
```csharp
HttpClient httpClient = new HttpClient();
OLMyBooksData myBooksData = await OLMyBooksLoader.GetCurrentlyReadingAsync("username-here");
```

If you want to get the reading logs of your own account, you may do that using the OpenLibraryClient as well:
```csharp
OpenLibraryClient client = new OpenLibraryClient();
await client.TryLoginAsync("username", "password");
if (client.LoggedIn)
{
    OLMyBooksData myBooksData = await client.MyBooks.GetCurrentlyReadingAsync();
}
```
This will work whether your account's reading logs are set to private or not.
