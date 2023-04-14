# Editions API 
### [Documentation on the OpenLibrary editions API](https://openlibrary.org/dev/docs/api/books)

The Editions API serves to get information pertaining to a specific edition of a work.

For more usage examples of this API, see [EditionAPIExamples](https://github.com/Luca3317/OpenLibrary.NET/blob/main/examples/EditionAPIExamples.cs).
***

Get a populated instance of OLEdition using OpenLibraryClient and a valid edition's ID (be aware this will make multiple requests):
```csharp
OpenLibraryClient client = new OpenLibraryClient();
OLEdition edition = await client.GetEditionAsync("OL7826547M");
```
You can get editions by OLID, ISBN, OCLC or LCCN.
```csharp
// These are all logically equivalent, though some might populate more of 
// edition's fields than others, depending on the data returned by OpenLibrary
OLEdition edition = await client.GetEditionAsync("OL7826547M", EditionIdType.OLID);
OLEdition edition = await client.GetEditionAsync("OL7826547M", EditionIdType.ISBN);
OLEdition edition = await client.GetEditionAsync("551650731", EditionIdType.OCLC);
OLEdition edition = await client.GetEditionAsync("95043936", EditionIdType.LCCN);
```
If you specify either S(mall), M(edium) or L(arge) as optional parameter, GetEditionAsync will also load the cover in the respective size.
```csharp
OLEdition edition = await client.GetEditionAsync("95043936", EditionIdType.LCCN, "s");
```
***
Alternatively, you can make individual requests using the OpenLibraryClient 
```csharp
OLEditionData editionData = await client.Edition.GetDataAsync("95043936", EditionIdType.LCCN);
byte[] coverSmall = await client.Image.GetCoverAsync("LCCN", "95043936", "s");
```
or using the static OLEditionLoader class.
```csharp
HttpClient httpClient = new HttpClient();
OLEditionData editionData = await OLEditionLoader.GetDataAsync(httpClient, "95043936", EditionIdType.LCCN);
byte[] coverSmall = await OLImageLoader.GetCoverAsync(httpClient, "LCCN", "95043936", "s");
```
If the GetCoverAsync call is not clear to you, see the [Covers API]((docs/Covers%20API.md)).
***
In addition to its data, you can also get an edition's [viewapi](https://openlibrary.org/dev/docs/api/books#):
```csharp

OLViewAPIData viewAPI = await client.Edition.GetViewAPIAsync(httpClient, "OL7826547M", EditionIdType.LCCN);

// To get a more detailled response
OLViewAPIData viewAPI = await client.Edition.GetViewAPIAsync(httpClient, "OL7826547M", details: true, EditionIdType.LCCN);
```