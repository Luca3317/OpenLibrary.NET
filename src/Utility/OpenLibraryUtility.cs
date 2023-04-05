using System.Collections.ObjectModel;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace OpenLibrary.NET
{
    public static class OpenLibraryUtility
    {
        #region Constants
        public const string BaseURL = "https://openlibrary.org/";
        public const string BaseURL_Covers = "https://covers.openlibrary.org/";
        #endregion

        // TODO Make more robust; rn will just remove /xyz/
        public static string ExtractIdFromKey(string key)
        {
            return Regex.Replace(key, "[/[a-zA-Z]*/]*", "");
        }

        /* Helper functions for correctly formatting OpenLibrary request URLs. 
         */
        #region URL Builders Functions

        public static string BuildURL(OLRequest request)
        {
            // TODO: perhaps instead dont make request.API nullable?
            if (request.API == null) throw new InvalidOperationException();
            return BuildURL((OLRequestAPI)request.API, request.ID, request.Path, request.Params.ToArray());
        }

        public static string BuildURL(OLRequestAPI api, string? id = null, string? path = null, params KeyValuePair<string, string>[] parameters)
        {
            StringBuilder stringBuilder = new StringBuilder().Clear();

            switch (api)
            {
                case OLRequestAPI.Covers:
                case OLRequestAPI.AuthorPhotos:
                    stringBuilder.Append(BaseURL_Covers + RequestTypePrefixMap[api] + "/" + id + ".jpg");
                    break;

                case OLRequestAPI.Search:
                    stringBuilder.Append(BaseURL + RequestTypePrefixMap[api]);

                    if (!string.IsNullOrWhiteSpace(path))
                        stringBuilder.Append("/" + path);

                    stringBuilder.Append(".json");
                    if (parameters != null && parameters.Length > 0)
                    {
                        stringBuilder.Append("?");
                        foreach (var param in parameters)
                        {
                            stringBuilder.Append(param.Key + "=" + param.Value + "&");
                        }
                    }
                    break;

                // Should work for /authors, /works, /editions, /isbn, /subjects ...?
                default:
                    stringBuilder.Append(BaseURL + RequestTypePrefixMap[api]);

                    if (!string.IsNullOrWhiteSpace(id))
                        stringBuilder.Append("/" + id);

                    if (!string.IsNullOrWhiteSpace(path))
                        stringBuilder.Append("/" + path);

                    stringBuilder.Append(".json");
                    if (parameters != null && parameters.Length > 0)
                    {
                        stringBuilder.Append("?");
                        foreach (var parameter in parameters)
                        {
                            stringBuilder.Append(parameter.Key + "=" + parameter.Value + "&");
                        }

                        // Remove the trailing &
                        stringBuilder.Remove(stringBuilder.Length - 1, 1);
                    }
                    break;
            }

            return stringBuilder.ToString();
        }
        #endregion

        /* Helper functions for requesting (and deserializing) data.
         */
        #region Requests
        public async static Task<string> RequestAsync(string url)
        {
            if (client == null) client = GetClient();
            return await client.GetStringAsync(url);
        }

        public async static Task<T?> LoadAsync<T>(string url, string path = "")
        {
            string response = await RequestAsync(url);
            if (string.IsNullOrWhiteSpace(path))
                return JsonConvert.DeserializeObject<T>(response);
            else
            {
                JToken token = JToken.Parse(response);
                return token[path]!.ToObject<T>();
            }
        }

        private static HttpClient? client = null;

        private static HttpClient GetClient()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            client = new HttpClient(handler);
            return client;
        }
        #endregion

        /* Json Converters for the deserialization of data requested from openlibrary.org;
         * multiple data points come as various types (i.e., sometimes as KeyValuePair, sometimes as array)
         */
        #region Json Converters
        public static class Serialization
        {
            public class BioConverter : JsonConverter<string>
            {
                public override string? ReadJson(JsonReader reader, Type objectType, string? existingValue, bool hasExistingValue, JsonSerializer serializer)
                {
                    if (reader.TokenType == JsonToken.String) return reader.Value!.ToString();
                    else if (reader.TokenType == JsonToken.StartObject)
                    {
                        JToken? value = JToken.ReadFrom(reader)["value"];
                        if (value != null && value.Type == JTokenType.String) return value.ToString();
                    }

                    throw new JsonException();
                }

                public override void WriteJson(JsonWriter writer, string? value, JsonSerializer serializer)
                {
                    serializer.Serialize(writer, value);
                }
            }

            public class DescriptionConverter : JsonConverter<string>
            {
                public override string? ReadJson(JsonReader reader, Type objectType, string? existingValue, bool hasExistingValue, JsonSerializer serializer)
                {
                    if (reader.TokenType == JsonToken.String) return reader.Value!.ToString();
                    else if (reader.TokenType == JsonToken.StartObject)
                    {
                        JToken? value = JToken.ReadFrom(reader)["value"];

                        if (value != null && value.Type == JTokenType.String) return value.ToString();
                    }

                    throw new JsonException();
                }

                public override void WriteJson(JsonWriter writer, string? value, JsonSerializer serializer)
                {
                    serializer.Serialize(writer, value);
                }
            }

            public class WorksKeysConverter : JsonConverter<string[]>
            {
                public override string[]? ReadJson(JsonReader reader, Type objectType, string[]? existingValue, bool hasExistingValue, JsonSerializer serializer)
                {
                    if (reader.TokenType == JsonToken.StartArray)
                    {
                        JArray array = JArray.Load(reader);
                        if (array.Count == 0) return existingValue;

                        if (array[0].Type == JTokenType.String)
                        {
                            return array.ToObject<string[]>();
                        }
                        else if (array[0].Type == JTokenType.Object)
                        {
                            List<string> strings = new List<string>();
                            foreach (JToken value in array)
                            {
                                JToken? authorToken = value["key"];
                                if (authorToken == null) throw new JsonException();
                                strings.Add(authorToken.ToString());
                            }

                            return strings.ToArray();
                        }
                    }

                    throw new JsonException();
                }

                public override void WriteJson(JsonWriter writer, string[]? value, JsonSerializer serializer)
                {
                    serializer.Serialize(writer, value);
                }
            }

            public class AuthorsKeysConverter : JsonConverter<string[]>
            {
                public override string[]? ReadJson(JsonReader reader, Type objectType, string[]? existingValue, bool hasExistingValue, JsonSerializer serializer)
                {
                    if (reader.TokenType == JsonToken.String)
                    {
                        return new string[1] { reader.Value!.ToString()! };
                    }
                    else if (reader.TokenType == JsonToken.StartArray)
                    {
                        JArray array = JArray.Load(reader);
                        if (array[0].Type == JTokenType.String)
                        {
                            return array.ToObject<string[]>();
                        }
                        else if (array[0].Type == JTokenType.Object)
                        {
                            List<string> strings = new List<string>();
                            foreach (JToken value in array)
                            {
                                JToken? authorToken = value["author"];
                                if (authorToken == null)
                                {
                                    authorToken = value["key"];
                                    if (authorToken == null) throw new JsonException();
                                }
                                strings.Add(authorToken.ToString());
                            }

                            return strings.ToArray();
                        }
                    }

                    throw new JsonException();
                }

                public override void WriteJson(JsonWriter writer, string[]? value, JsonSerializer serializer)
                {
                    serializer.Serialize(writer, value);
                }
            }

            public class EditionsAuthorsKeysConverter : JsonConverter<string[]>
            {
                public override string[]? ReadJson(JsonReader reader, Type objectType, string[]? existingValue, bool hasExistingValue, JsonSerializer serializer)
                {
                    if (reader.TokenType == JsonToken.StartArray)
                    {
                        JArray array = JArray.Load(reader);
                        if (array.Count == 0) return new string[0];

                        if (array[0].Type == JTokenType.String)
                        {
                            return array.ToObject<string[]>();
                        }
                        else if (array[0].Type == JTokenType.Object)
                        {
                            List<string> strings = new List<string>();
                            foreach (JToken value in array)
                            {
                                JToken? authorToken = value["key"];
                                if (authorToken == null) if (authorToken == null) throw new JsonException();
                                strings.Add(authorToken.ToString());
                            }

                            return strings.ToArray();
                        }
                    }

                    throw new JsonException();
                }

                public override void WriteJson(JsonWriter writer, string[]? value, JsonSerializer serializer)
                {
                    serializer.Serialize(writer, value);
                }
            }
        }
        #endregion

        /* These dictionaries map the various APIs to their valid "paths" and parameters.
         * Helper data not used anywhere within the library itself.
         */
        #region Maps
        public static readonly ReadOnlyDictionary<OLRequestAPI, string> RequestTypePrefixMap = new ReadOnlyDictionary<OLRequestAPI, string>
        (
            new Dictionary<OLRequestAPI, string>()
            {
            {OLRequestAPI.Books, "api"},
            {OLRequestAPI.Books_Works, "works"},
            {OLRequestAPI.Books_Editions, "books"},
            {OLRequestAPI.Books_ISBN, "isbn"},
            {OLRequestAPI.Authors, "authors"},
            {OLRequestAPI.Subjects, "subjects"},
            {OLRequestAPI.Search, "search"},
            {OLRequestAPI.SearchInside, ""},
            {OLRequestAPI.Partner, ""},
            {OLRequestAPI.Covers, "b"},
            {OLRequestAPI.AuthorPhotos, "a"},
            {OLRequestAPI.RecentChanges, ""},
            }
        );

        public static ReadOnlyDictionary<string, List<string>> GetPathParametersMap(OLRequestAPI api)
        {
            switch (api)
            {
                case OLRequestAPI.Books: return AuthorsSubfileFiltersMap;
                case OLRequestAPI.Books_Works: return WorksPathParametersMap;
                case OLRequestAPI.Books_Editions: return EditionsPathParametersMap;
                case OLRequestAPI.Books_ISBN: return ISBNPathParametersMap;
                case OLRequestAPI.Authors: return AuthorsSubfileFiltersMap;
                case OLRequestAPI.Subjects: return SubjectsSubfileFiltersMap;
                case OLRequestAPI.Search: return SearchSubfileFiltersMap;
                case OLRequestAPI.SearchInside: return AuthorsSubfileFiltersMap;
                case OLRequestAPI.Partner: return AuthorsSubfileFiltersMap;
                case OLRequestAPI.Covers: return CoversSubfileFiltersMap;
                case OLRequestAPI.AuthorPhotos: return AuthorPhotosSubfileFiltersMap;
                case OLRequestAPI.RecentChanges: return AuthorsSubfileFiltersMap;
                default: throw new System.Exception();
            }
        }

        #region Books Maps
        public static readonly ReadOnlyDictionary<string, List<string>> BooksPathParametersMap = new ReadOnlyDictionary<string, List<string>>
        (
            new Dictionary<string, List<string>>
            {
                {string.Empty, new List<string>() { "bibkeys", "format", "callback", "jscmd" } }
            }
        );
        #endregion

        #region Works Maps
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

        #endregion

        #region Editions Maps
        public static readonly ReadOnlyDictionary<string, List<string>> EditionsPathParametersMap = new ReadOnlyDictionary<string, List<string>>
        (
            new Dictionary<string, List<string>>
            {
                {string.Empty, new List<string>() },
            }
        );

        #endregion


        #region ISBN Maps
        public static readonly ReadOnlyDictionary<string, List<string>> ISBNPathParametersMap = new ReadOnlyDictionary<string, List<string>>
        (
            new Dictionary<string, List<string>>
            {
                {string.Empty, new List<string>() },
            }
        );

        #endregion

        #region Authors Maps
        public static readonly ReadOnlyDictionary<string, List<string>> AuthorsSubfileFiltersMap = new ReadOnlyDictionary<string, List<string>>
        (
            new Dictionary<string, List<string>>
            {
                {string.Empty, new List<string>() },
                {"works", new List<string>() { "offset", "limit" } },
            }
        );
        #endregion

        #region Subjects Maps
        public static readonly ReadOnlyDictionary<string, List<string>> SubjectsSubfileFiltersMap = new ReadOnlyDictionary<string, List<string>>
        (
            new Dictionary<string, List<string>>
            {
                {string.Empty, new List<string>() { "details", "ebooks", "published_in", "limit", "offset", "sort" } }
            }
        );
        #endregion

        #region Search Maps
        public static readonly ReadOnlyDictionary<string, List<string>> SearchSubfileFiltersMap = new ReadOnlyDictionary<string, List<string>>
        (
            new Dictionary<string, List<string>>
            {
                {string.Empty, new List<string>() { "q", "title", "author", "subject", "place", "person", "language", "publisher", "sort" } },
                {"authors", new List<string>() { "q", "sort", "limit", "offset" } },
                {"subjects", new List<string>() { "q", "limit", "offset" } },
                {"lists", new List<string>() { "q", "limit", "offset" } },
            }
        );
        #endregion

        #region Covers Maps
        public static readonly ReadOnlyDictionary<string, List<string>> CoversSubfileFiltersMap = new ReadOnlyDictionary<string, List<string>>
        (
            new Dictionary<string, List<string>>
            {
                {string.Empty, new List<string>(){ } }
            }
        );
        #endregion

        #region AuthorPhotos Maps
        public static readonly ReadOnlyDictionary<string, List<string>> AuthorPhotosSubfileFiltersMap = new ReadOnlyDictionary<string, List<string>>
        (
            new Dictionary<string, List<string>>
            {
                {string.Empty, new List<string>() }
            }
        );
        #endregion

        // subjects, books, works, editions, isbn, partner, search, searchinside, and more
        #endregion

    }
}
