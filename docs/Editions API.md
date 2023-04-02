# Editions API 
### [Documentation on the OpenLibrary editions API](https://openlibrary.org/dev/docs/api/books)

The Editions API serves to get information pertaining to a specific edition of a work.
***

To get an edition by its OLID, use the static OLEditionLoader class:
```csharp
OLEditionData editionData = await OLEditionLoader.GetDataAsync();
```

Alternatively, you can also get an edition's data by its ISBN:
```csharp
OLEditionData editionData = await OLEditionLoader.GetDataByISBNAsync("OL35738141M");
```