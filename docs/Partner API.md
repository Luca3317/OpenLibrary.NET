# Partner API 
### [Documentation on the OpenLibrary partner API](https://openlibrary.org/dev/docs/api/read)

The Partner API (previously Read API) serves to get online-readable or borrowable books from OpenLibrary. Also returns data about the work itself.

For more usage examples of this API, see [MiscellaneousExamples](https://github.com/Luca3317/OpenLibrary.NET/blob/main/examples/MiscellaneousExamples.cs).
***

To make a request to the Partner API, use the OpenLibraryClient
```csharp
OpenLibraryClient client = new OpenLibraryClient();
OLPartnerData partnerData = await client.Partner.GetDataAsync(PartnerIdType.ISBN, "0123456789");
```

or the static OLPartnerLoader methods:
```csharp
HttpClient httpClient = new HttpClient();
OLPartnerData partnerData = await OLPartnerLoader.GetDataAsync(PartnerIdType.OLID, "OL15601629M");
```

To request data for multiple books at once, use GetMultiDataAsync:
```csharp
OLPartnerData partnerData = await client.GetMultiDataAsync("isbn:0123456789", "olid:OL15601629M");
```
