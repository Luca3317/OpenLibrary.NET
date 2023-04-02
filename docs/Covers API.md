# Covers API 
### [Documentation on the OpenLibrary covers API (Note the warnings about rate limiting and bulk downloads)](https://openlibrary.org/dev/docs/api/covers)

The Covers API serves to get book covers and author photos.
***

### Book Covers
To get a book cover by its ID, use the static OLImageLoader class:
```csharp
string key = ..., value = ..., size = ...;
byte[] cover = await OLImageLoader.GetCoverAsync(key, value, size);

// Examples
byte[] cover = await OLImageLoader.GetCoverAsync("olid", "OL7440033M", "S");
byte[] cover = await OLImageLoader.GetCoverAsync("isbn", "0385472579", "M");
byte[] cover = await OLImageLoader.GetCoverAsync("librarything", "192819", "L");
```
According to the OpenLibrary API documentation:
> key can be any one of ISBN, OCLC, LCCN, OLID and ID (case-insensitive)
> 
> value is the value of the chosen key
> 
> size can be one of S, M and L for small, medium and large respectively.

If you want to, you can also prepare a full ID string yourself:
```csharp
byte[] cover = await OLImageLoader.GetCoverAsync(id);

// Examples
byte[] cover = await OLImageLoader.GetCoverAsync("olid/OL7440033M-S");
byte[] cover = await OLImageLoader.GetCoverAsync("isbn/0385472579-M");
byte[] cover = await OLImageLoader.GetCoverAsync("librarything/192819-L");
```
>The URL pattern to access book covers is:
>https://covers.openlibrary.org/b/$key/$value-$size.jpg
***

### Author Photos
The process of getting an author photo by ID is entirely analogous (with the exception that the key may only be either OLID or ID):
```csharp
string key = ..., value = ..., size = ...;
byte[] cover = await OLImageLoader.GetAuthorPhotoAsync(key, value, size);

// Examples
byte[] cover = await OLImageLoader.GetAuthorPhotoAsync("olid", "OL229501A", "S");
```
or
```csharp
byte[] cover = await OLImageLoader.GetAuthorPhotoAsync(id);

// Examples
byte[] cover = await OLImageLoader.GetAuthorPhotoAsync("olid/OL229501A-S");
```
>The URL Pattern for accessing author photos is:
>https://covers.openlibrary.org/a/$key/$value-$size.jpg