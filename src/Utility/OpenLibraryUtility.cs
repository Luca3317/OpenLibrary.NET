using System.Collections.ObjectModel;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace OpenLibraryNET.Utility
{
    public static class OpenLibraryUtility
    {
        #region Constants
        public const string BaseURL = "openlibrary.org";
        public const string BaseURL_Covers = "covers.openlibrary.org";

        public static readonly Uri BaseUri = new Uri("https://" + BaseURL);
        public static readonly Uri BaseUri_Covers = new Uri("https://" + BaseURL_Covers);
        #endregion

        // TODO Make more robust; rn will just remove /xyz/
        public static string ExtractIdFromKey(string key)
        {
            return Regex.Replace(key, "[/[a-zA-Z]*/]*", "");
        }

        #region Bibkey Helpers
        public static string GetRawBibkey(string id) => Regex.Match(id, "(?<=:)[^:]*$|^[^:]*$").ToString();
        public static string GetBibkeyPrefix(string id) => Regex.Match(id, ".*(?=:)").ToString();

        public static EditionIdType? InferEditionIdType(string id)
        {
            string pureID = GetRawBibkey(id);
            string idPrefix = GetBibkeyPrefix(id);

            if (idPrefix != null && idPrefix != "")
            {
                switch (idPrefix)
                {
                    case "ISBN": return EditionIdType.ISBN;
                    case "LCCN": return EditionIdType.LCCN;
                    case "OCLC": return EditionIdType.OCLC;
                    case "OLID": return EditionIdType.OLID;
                }
            }
            if (Regex.Match(pureID, "^OL[0-9]*[A-Z]").Success)
                return EditionIdType.OLID;

            return null;
        }

        /// <summary>
        /// Tries to ensure the bibkey prefix is set correctly.<br/>
        /// Will work on raw keys and incorrectly set prefixes.
        /// <para>
        /// Examples:<br/>
        /// idType = ISBN, key = "012345" => "ISBN:012345"<br/>
        /// idType = LCCN, key = "OCLC:12312" => "LCCN:12312"<br/>
        /// idType = ISBN, key = "OCLC90000" => "ISBN:OCLC90000" (missing colon)<br/>
        /// </para>
        /// </summary>
        /// <param name="idType"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string SetBibkeyPrefix(EditionIdType idType, string key)
        {
            key = Regex.Replace(key, ".*:", "");
            return idType.GetString() + ":"+ key;
        }
        #endregion

        /* Helper functions for correctly formatting OpenLibrary request URLs. 
         */
        #region URL Builders Functions
        public static Uri BuildWorksUri(string olid, string path = "", params KeyValuePair<string, string>[] parameters)
        {
            return BuildUri
            (
                BaseURL,
                "works/" + olid + (string.IsNullOrWhiteSpace(path) ? "" : "/" + path) + ".json",
                parameters
            );
        }

        public static Uri BuildEditionsUri(string olid, string path = "", params KeyValuePair<string, string>[] parameters)
        {
            return BuildUri
            (
                BaseURL,
                "books/" + olid + (string.IsNullOrWhiteSpace(path) ? "" : "/" + path) + ".json",
                parameters
            );
        }

        public static Uri BuildISBNUri(string isbn)
        {
            return BuildUri
            (
                BaseURL,
                "isbn/" + isbn + ".json"
            );
        }

        public static Uri BuildBooksUri(string bibkeys = "", string path = "", params KeyValuePair<string, string>[] parameters)
            => BuildUri
                (
                    BaseURL,
                    "api/books" + (string.IsNullOrWhiteSpace(path) ? "" : "/" + path) + ".json",
                    string.IsNullOrWhiteSpace(bibkeys) ?
                    parameters :
                    new List<KeyValuePair<string, string>>(parameters) { new KeyValuePair<string, string>("bibkeys", bibkeys) }.ToArray()

                );

        public static Uri BuildBooksUri(string[] bibkeys, string path = "", params KeyValuePair<string, string>[] parameters)
        {
            string bibkeysConcat = "";
            foreach (string bibkey in bibkeys)
            {
                bibkeysConcat += bibkey + ",";
            }

            return BuildUri
            (
                BaseURL,
                "api/books" + (string.IsNullOrWhiteSpace(path) ? "" : "/" + path) + ".json",
                bibkeys == null || bibkeys.Length == 0 ?
                parameters :
                new List<KeyValuePair<string, string>>(parameters) { new KeyValuePair<string, string>("bibkeys", bibkeysConcat) }.ToArray()
            );
        }

        public static Uri BuildAuthorsUri(string olid, string path = "", params KeyValuePair<string, string>[] parameters)
        {
            return BuildUri
            (
                BaseURL,
                "authors/" + olid + (string.IsNullOrWhiteSpace(path) ? "" : "/" + path) + ".json",
                parameters
            );
        }

        public static Uri BuildSubjectsUri(string subject, string path = "", params KeyValuePair<string, string>[] parameters)
        {
            return BuildUri
            (
                BaseURL,
                "subjects/" + subject + (string.IsNullOrWhiteSpace(path) ? "" : "/" + path) + ".json",
                parameters
            );
        }

        public static Uri BuildSearchUri(string query = "", string path = "", params KeyValuePair<string, string>[] parameters)
        {
            return BuildUri
            (
                BaseURL,
                "search" + (string.IsNullOrWhiteSpace(path) ? "" : "/" + path) + ".json",
                string.IsNullOrWhiteSpace(query) ?
                    parameters :
                    new List<KeyValuePair<string, string>>(parameters) { new KeyValuePair<string, string>("q", query) }.ToArray()
            );
        }

        public static Uri BuildRecentChangesUri(string year, string month, string day, string kind = "", params KeyValuePair<string, string>[] parameters)
            => BuildRecentChangesUri(year + "/" + month + "/" + day + (string.IsNullOrWhiteSpace(kind) ? "" : "/" + kind), parameters);
        public static Uri BuildRecentChangesUri(string year, string month, string kind = "", params KeyValuePair<string, string>[] parameters)
            => BuildRecentChangesUri(year + "/" + month + (string.IsNullOrWhiteSpace(kind) ? "" : "/" + kind), parameters);
        public static Uri BuildRecentChangesUri(string year, string kind = "", params KeyValuePair<string, string>[] parameters)
            => BuildRecentChangesUri(year + (string.IsNullOrWhiteSpace(kind) ? "" : "/" + kind), parameters);
        public static Uri BuildRecentChangesUri(string kind = "", params KeyValuePair<string, string>[] parameters)
        {
            return BuildUri
            (
                BaseURL,
                "recentchanges" +
                (string.IsNullOrWhiteSpace(kind) ? "" : "/" + kind) + ".json",
                parameters
            );
        }

        public static Uri BuildListsUri(string username, string? id = null, string? path = null, params KeyValuePair<string, string>[] parameters)
        {
            return BuildUri
            (
                BaseURL,
                "people/" + username + "/lists" +
                (string.IsNullOrWhiteSpace(id) ? "" : "/" + id) +
                (string.IsNullOrWhiteSpace(path) ? "" : "/" + path) +
                ".json",
                parameters
            );
        }

        public static Uri BuildCoversUri(CoverIdType idType, string id, ImageSize size, params KeyValuePair<string, string>[] parameters)
            => BuildCoversUri(idType.GetString(), id, size.GetString(), parameters);
        public static Uri BuildCoversUri(string idType, string id, string size, params KeyValuePair<string, string>[] parameters)
            => BuildCoversUri(idType + "/" + id + "-" + size, parameters);
        public static Uri BuildCoversUri(string key, params KeyValuePair<string, string>[] parameters)
        {
            return BuildUri
            (
                BaseURL_Covers,
                "b/" + key + ".jpg",
                parameters
            );
        }

        public static Uri BuildAuthorPhotosUri(AuthorPhotoIdType idType, string id, ImageSize size, params KeyValuePair<string, string>[] parameters)
            => BuildAuthorPhotosUri(idType.GetString(), id, size.GetString(), parameters);
        public static Uri BuildAuthorPhotosUri(string idType, string id, string size, params KeyValuePair<string, string>[] parameters)
            => BuildAuthorPhotosUri(idType + "/" + id + "-" + size, parameters);
        public static Uri BuildAuthorPhotosUri(string key, params KeyValuePair<string, string>[] parameters)
        {
            return BuildUri
            (
                BaseURL_Covers,
                "a/" + key + ".jpg",
                parameters
            );
        }

        // TODO maybe add only path + parameters version for each builder; could call this here then instead of using requesttypeprefixmap
        public static Uri BuildUri(OLRequestAPI api, string path, params KeyValuePair<string, string>[] parameters)
        {
            // Remove leading prefix if it is there, to prevent duplication
            path = SetPrefix(api, path);
            path = SetFileExtension(api, path);

            switch (api)
            {
                case OLRequestAPI.Covers:
                case OLRequestAPI.AuthorPhotos: return BuildUri(BaseURL_Covers, path, KeyValuePairToString(parameters));

                default: return BuildUri(BaseURL, path, KeyValuePairToString(parameters));
            }
        }

        /// <summary>
        /// Builds an Uri from the given arguments.
        /// None of the arguments are modified in any way, therefore
        /// Host: The base address; since this function is private, this is ensured to always be BaseURL or BaseURL_Covers.
        /// Path: The absolute path; must already include prefix and file extension.
        /// Parameters: Form the component of the Uri; either pass in array or use KeyValuePairToString.
        /// </summary>
        /// <param name="host">The base address; since this function is private, this is ensured to always be BaseURL or BaseURL_Covers.</param>
        /// <param name="path">The absolute path; must already include prefix and file extension.</param>
        /// <param name="fileExt"></param>
        /// <param name="parameters">Form the component of the Uri; either pass in array or use KeyValuePairToString.</param>
        /// <returns></returns>
        private static Uri BuildUri(string host, string path = "", params KeyValuePair<string, string>[] parameters)
            => BuildUri(host, path, KeyValuePairToString(parameters));
        private static Uri BuildUri(string host, string path = "", string query = "")
        {
            UriBuilder builder = new UriBuilder() { Scheme = Uri.UriSchemeHttps };
            builder.Host = host;
            builder.Path = path;
            builder.Query = query;
            return builder.Uri;
        }

        /// <summary>
        /// Ensures path will have the correct prefix prepended.
        /// Passing in paths that already have a prefix is fine.
        /// </summary>
        /// <param name="api"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string SetPrefix(OLRequestAPI api, string path)
        {
            string prefix = RequestTypePrefixMap[api];

            if (api == OLRequestAPI.Books)
            {
                path = Regex.Replace(path, "^/?" + RequestTypePrefixMap[api] + "/?", "");
                return prefix + path;
            }

            path = Regex.Replace(path, "^/?" + RequestTypePrefixMap[api] + "/", "");
            return prefix + "/" + path;
        }

        /// <summary>
        /// Ensures path will have the correct file extension.
        /// Passing in paths that already have a file extension is fine.
        /// </summary>
        /// <param name="api"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string SetFileExtension(OLRequestAPI api, string path)
        {
            path = Regex.Replace(path, "\\.([^.]*\\.?)$", "");
            if (api == OLRequestAPI.Covers || api == OLRequestAPI.AuthorPhotos)
                return path + ".jpg";
            else
                return path + ".json";
        }

        private static string KeyValuePairToString(params KeyValuePair<string, string>[] parameters)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (parameters != null && parameters.Length > 0)
            {
                foreach (var parameter in parameters)
                {
                    stringBuilder.Append(parameter.Key + "=" + parameter.Value + "&");
                }

                // Remove the trailing &
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
            }

            return stringBuilder.ToString();
        }
        #endregion

        /* Helper functions for requesting (and deserializing) data.
         */
        #region Requests
        internal async static Task<string> RequestAsync(Uri uri, HttpClient? client = null)
        {
            if (client == null) client = GetClient();
            return await client.GetStringAsync(uri);
        }

        internal static T? Parse<T>(string serialized, string path = "")
        {
            if (string.IsNullOrWhiteSpace(path))
                return JsonConvert.DeserializeObject<T>(serialized);
            else
            {
                JToken token = JToken.Parse(serialized);
                return token[path]!.ToObject<T>();
            }
        }

        internal async static Task<T?> LoadAsync<T>(Uri uri, string path = "", HttpClient? client = null)
        {
            string response = await RequestAsync(uri, client);
            if (string.IsNullOrWhiteSpace(path))
                return JsonConvert.DeserializeObject<T>(response);
            else
            {
                JToken token = JToken.Parse(response);
                return token[path]!.ToObject<T>();
            }
        }

        public static HttpClient GetClient()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            HttpClient client = new HttpClient(handler);
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
                        if (value != null && value.Type == JTokenType.String) return value.ToObject<string>();
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

                        if (value != null && value.Type == JTokenType.String) return value.ToObject<string>();
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
                                strings.Add(authorToken.ToObject<string>()!);
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
                                else if (authorToken.Type == JTokenType.Object)
                                {
                                    authorToken = authorToken["key"];
                                    if (authorToken == null) throw new JsonException();
                                }
                                strings.Add(authorToken.ToObject<string>()!);
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
                                if (authorToken == null)
                                {
                                    authorToken = value["url"];
                                    if (authorToken == null) throw new JsonException();
                                }
                                strings.Add(authorToken.ToObject<string>()!);
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

            public class EditionPublishersConverter : JsonConverter<string[]>
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
                                JToken? authorToken = value["name"];
                                strings.Add(authorToken!.ToObject<string>()!);
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

            public class EditionSubjectsConverter : JsonConverter<string[]>
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
                                JToken? authorToken = value["name"];
                                strings.Add(authorToken!.ToObject<string>()!);
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

            public class RecentChangesAuthorConverter : JsonConverter<string>
            {
                public override string? ReadJson(JsonReader reader, Type objectType, string? existingValue, bool hasExistingValue, JsonSerializer serializer)
                {
                    if (reader.TokenType == JsonToken.String) return reader.Value!.ToString();
                    else if (reader.TokenType == JsonToken.StartObject)
                    {
                        JToken? value = JToken.ReadFrom(reader)["key"];

                        if (value != null && value.Type == JTokenType.String) return value.ToObject<string>();
                    }

                    throw new JsonException();
                }

                public override void WriteJson(JsonWriter writer, string? value, JsonSerializer serializer)
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
            {OLRequestAPI.Books, "api/books"},
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
            {OLRequestAPI.RecentChanges, "recentchanges"},
            {OLRequestAPI.Lists, "people"}
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

        #region Lists Maps
        public static readonly ReadOnlyDictionary<string, List<string>> ListsSubfileFiltersMap = new ReadOnlyDictionary<string, List<string>>
        (
            new Dictionary<string, List<string>>
            {
                {string.Empty, new List<string>() { } },
                {"editions", new List<string>() { "limit", "offset" } },
                {"subjects", new List<string>() { "limit", "offset" } }
            }
        );
        #endregion

        #region RecentChanges Maps
        public static readonly ReadOnlyDictionary<string, List<string>> RecentChangesSubfileFiltersMap = new ReadOnlyDictionary<string, List<string>>
        (
            new Dictionary<string, List<string>>
            {
                {string.Empty, new List<string>() { "limit", "offset", "bot" } }
            }
        );
        #endregion
        #endregion

    }
}
