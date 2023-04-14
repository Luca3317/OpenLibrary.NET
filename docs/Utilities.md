# Utilities

There are various important utility classes.

### [OLLoader](https://github.com/Luca3317/OpenLibrary.NET/tree/main/src/OLLoader)

For each of OpenLibrary's APIs there is a static Loader class. These supply a simple interface for requesting and parsing specific data from OpenLibrary.

```csharp
OLAuthorData authorData = await OLAuthorLoader.GetDataAsync("OL28104A");
OLWorkData workData = await OLWorkLoader.GetDataAsync("OL21019691W");
OLRatingsData workRatings = await OLWorkLoader.GetRatingsAsync("OL21019691W");
```
For examples for each Loader class, see the respective API's documentations.

### [OpenLibraryUtility](https://github.com/Luca3317/OpenLibrary.NET/blob/main/src/Utility/OpenLibraryUtility.cs)
The main utility class. Provides various helpful methods and variables.

#### Constants
Provides the base URLs for the various APIs.
```csharp
public const string BaseURL = "https://openlibrary.org/";
public const string BaseURL_Covers = "https://covers.openlibrary.org/";
```

#### Maps
#### PathParametersMaps
For each API, there is a readonly dictionary defined that maps the APIs' requests paths to the parameters that are valid for this path. 

For example:
```csharp
public static readonly ReadOnlyDictionary<string, List<string>> WorksPathParametersMap = new ReadOnlyDictionary<string, List<string>>
(
    new Dictionary<string, List<string>>
    {
        {string.Empty, new List<string>() },
        {"editions", new List<string>(){ "limit", "offset" } },
        {"bookshelves", new List<string>() },
        {"ratings", new List<string>() },
    }
);
```
Note that these are not guaranteed to be complete. To maintain flexibility in case of API changes, these are not enforced anywhere within the other classes.

#### RequestTypePrefixMap
The RequestTypePrefixMap
```csharp
public static readonly ReadOnlyDictionary<OLRequestAPI, string> RequestTypePrefixMap = ...
```
maps the various APIs to their url prefix (i.e., OLRequestAPI.Authors => "authors", OLRequestAPI.Covers => "b" etc.)

#### BuildURL
Simple helper method to correctly format an OpenLibrary url.
```csharp
public static string BuildURL(OLRequestAPI api, string? id = null, string? path = null, params KeyValuePair<string, string>[] parameters);

// Examples
OpenLibraryUtility.BuildURL(OLRequestAPI.Authors, "OL274606A", "works", new KeyValuePair<string, string>("limit", "10"));
```

#### Requests
Provides basic methods for making web requests. Uses private static HttpClient.

Also provides method to request and then parse data.

```csharp
public async static Task<string> RequestAsync(string url);
public async static Task<T> LoadAsAsync<T>(string url, string path = "");

// Examples
string response = OpenLibraryUtility.RequestAsync("https://openlibrary.org/authors/OL274606A.json");
OLWorkData[] workData = OpenLibraryUtility.LoadAs<OlWorkData>("https://openlibrary.org/authors/OL274606A/works.json", "entries");
// Note: The work collection is the "entries" variable in the json response of the given url.
// The path parameter in LoadAsAsync is the path within the json structure.
```

