# OpenLibrary.NET
[![NuGet](https://img.shields.io/nuget/v/WinSW?style=flat-square)](https://www.nuget.org/packages/OpenLibrary.NET/)

A C# library for OpenLibrary by the InternetArchive

See the [OpenLibray Developer Center](https://openlibrary.org/developers/api) for documentation on the various APIs offered by OpenLibrary.

See the [docs folder](https://github.com/Luca3317/OpenLibrary.NET/tree/main/docs) for documentation on this library.

## Installation
```console
dotnet add package OpenLibrary.NET --version 0.0.3
```
***
Currently, OpenLibrary.Net supports the following of OpenLibrary's APIs:
* Books API
  * [Works API](docs/Works%20API.md)
  * [Editions API](docs/Editions%20API.md)
  * [ISBN API](docs/Editions%20API.md)
  * [(Generic) Books API](docs/Editions%20API.md) - Partially supported using Extension Data
* [Authors API](docs/Authors%20API.md)
* [Subjects API](docs/Subjects%20API.md)
* [Search API](docs/Search%20API.md)
* Covers API
  * [Covers API](docs/Covers%20API.md)
  * [AuthorPhotos API](docs/Covers%20API.md)

For now, the SearchInside, Partner, RecentChanges and Lists APIs are unsupported.
***
Coming TODO:

* Add full support for the Books API
* Add support for the Lists API
* Add support for the SearchInside API
