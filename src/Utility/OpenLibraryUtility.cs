using System.Collections.ObjectModel;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Collections.Immutable;

namespace OpenLibraryNET.Utility
{
    /// <summary>
    /// OpenLibraryNET specific utility classes and methods.
    /// </summary>
    public static class OpenLibraryUtility
    {
        #region Constants
        /// <summary>
        /// The base URL for OpenLibrary requests.
        /// </summary>
        public const string BaseURL = "openlibrary.org";
        /// <summary>
        /// The base URL for OpenLibrary image requests.
        /// </summary>
        public const string BaseURL_Covers = "covers.openlibrary.org";

        /// <summary>
        /// The base Uri for OpenLibrary requests.
        /// </summary>
        public static readonly Uri BaseUri = new Uri("https://" + BaseURL);
        /// <summary>
        /// The base Uri for OpenLibrary image requests.
        /// </summary>
        public static readonly Uri BaseUri_Covers = new Uri("https://" + BaseURL_Covers);
        #endregion

        #region ID formatting helpers
        /// <summary>
        /// Extracts the ID from a valid key.<br/>
        /// i.e. /works/OL8037381W => OL8037381W
        /// </summary>
        /// <param name="key">The key to extract ID from.</param>
        /// <returns>Extracted ID, assuming the input key was valid.</returns>
        public static string ExtractIdFromKey(string key)
        {
            return Regex.Match(key, "[^/]+(?=/$|$)").Value;
        }

        /// <summary>
        /// Return the raw ID from a prefixed bibkey.<br/>
        /// i.e. ISBN:0123456789 => 0123456789
        /// </summary>
        /// <param name="bibkey">The bibkey to extract raw ID from.</param>
        /// <returns>Extracted ID, assuming input bibkey was valid.</returns>
        public static string GetRawBibkey(string bibkey) => Regex.Match(bibkey, "(?<=:)[^:]*$|^[^:]*$").ToString();
        /// <summary>
        /// Return the prefix from a prefixed bibkey.<br/>
        /// i.e. ISBN:0123456789 => ISBN
        /// </summary>
        /// <param name="bibkey">The bibkey to extract raw ID from.</param>
        /// <returns>Extracted prefix, assuming input bibkey was valid.</returns>
        public static string GetBibkeyPrefix(string bibkey) => Regex.Match(bibkey, ".*(?=:)").ToString();

        /// <summary>
        /// Tries to ensure the bibkey prefix is set correctly.<br/>
        /// Will work on raw keys and incorrectly set prefixes.<br/>
        /// </summary>
        /// <param name="idType">Type of the ID.</param>
        /// <param name="id">The ID to prefix.</param>
        /// <returns>Prefixed bibkey, assuming the input ID was valid.</returns>
        public static string SetBibkeyPrefix(BookIdType idType, string id)
            => SetBibkeyPrefix(idType.GetString(), id);
        /// <summary>
        /// Tries to ensure the bibkey prefix is set correctly.<br/>
        /// Will work on raw keys and incorrectly set prefixes.<br/>
        /// </summary>
        /// <param name="idType">Type of the ID.</param>
        /// <param name="id">The ID to prefix.</param>
        /// <returns>Prefixed bibkey, assuming the input ID was valid.</returns>
        public static string SetBibkeyPrefix(PartnerIdType idType, string id)
            => SetBibkeyPrefix(idType.GetString(), id);
        /// <summary>
        /// Tries to ensure the bibkey prefix is set correctly.<br/>
        /// Will work on raw keys and incorrectly set prefixes.<br/>
        /// </summary>
        /// <param name="idType">Type of the ID.</param>
        /// <param name="id">The ID to prefix.</param>
        /// <returns>Prefixed bibkey, assuming the input ID was valid.</returns>
        public static string SetBibkeyPrefix(string idType, string id)
        {
            id = Regex.Replace(id, ".*:", "");
            return idType + ":" + id;
        }
        #endregion

        /* Helper functions for correctly formatting OpenLibrary request URLs. 
         */
        #region URL Builders Functions
        /// <summary>
        /// Build a Works API Uri.
        /// </summary>
        /// <param name="olid">OLID of the work.</param>
        /// <param name="path">Path of the Uri.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>A Works API Uri.</returns>
        public static Uri BuildWorksUri(string olid, string path = "", params KeyValuePair<string, string>[] parameters)
        {
            return BuildUri
            (
                BaseURL,
                "works/" + olid + (string.IsNullOrWhiteSpace(path) ? "" : "/" + path) + ".json",
                parameters
            );
        }

        /// <summary>
        /// Build an Editions API Uri.
        /// </summary>
        /// <param name="olid">OLID of the edition.</param>
        /// <param name="path">Path of the Uri.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>An Editions API Uri.</returns>
        public static Uri BuildEditionsUri(string olid, string path = "", params KeyValuePair<string, string>[] parameters)
        {
            return BuildUri
            (
                BaseURL,
                "books/" + olid + (string.IsNullOrWhiteSpace(path) ? "" : "/" + path) + ".json",
                parameters
            );
        }

        /// <summary>
        /// Build an ISBN API Uri.
        /// </summary>
        /// <param name="isbn">ISBN of the edition.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>An ISBN API Uri.</returns>
        public static Uri BuildISBNUri(string isbn, params KeyValuePair<string, string>[] parameters)
        {
            return BuildUri
            (
                BaseURL,
                "isbn/" + isbn + ".json",
                parameters
            );
        }

        /// <summary>
        /// Build a Books API Uri.
        /// </summary>
        /// <param name="bibkeys">Bibkeys of the editions.</param>
        /// <param name="path">Path of the Uri.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>A Books API Uri.</returns>
        public static Uri BuildBooksUri(string bibkeys = "", string path = "", params KeyValuePair<string, string>[] parameters)
            => BuildUri
                (
                    BaseURL,
                    "api/books" + (string.IsNullOrWhiteSpace(path) ? "" : "/" + path) + ".json",
                    string.IsNullOrWhiteSpace(bibkeys) ?
                    parameters :
                    new List<KeyValuePair<string, string>>(parameters) { new KeyValuePair<string, string>("bibkeys", bibkeys) }.ToArray()

                );

        /// <summary>
        /// Build a Books API Uri.
        /// </summary>
        /// <param name="bibkeys">Bibkeys of the editions.</param>
        /// <param name="path">Path of the Uri.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>A Books API Uri.</returns>
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

        /// <summary>
        /// Build an Authors API Uri.
        /// </summary>
        /// <param name="olid">OLID of the author.</param>
        /// <param name="path">Path of the Uri.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>An Authors API Uri.</returns>
        public static Uri BuildAuthorsUri(string olid, string path = "", params KeyValuePair<string, string>[] parameters)
        {
            return BuildUri
            (
                BaseURL,
                "authors/" + olid + (string.IsNullOrWhiteSpace(path) ? "" : "/" + path) + ".json",
                parameters
            );
        }

        /// <summary>
        /// Build a Subjects API Uri.
        /// </summary>
        /// <param name="subject">Name of the subject.</param>
        /// <param name="path">Path of the Uri.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>A Subjects API Uri.</returns>
        public static Uri BuildSubjectsUri(string subject, string path = "", params KeyValuePair<string, string>[] parameters)
        {
            return BuildUri
            (
                BaseURL,
                "subjects/" + subject + (string.IsNullOrWhiteSpace(path) ? "" : "/" + path) + ".json",
                parameters
            );
        }

        /// <summary>
        /// Build a Search API Uri.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <param name="path">Path of the Uri.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>A Search API Uri.</returns>
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

        /// <summary>
        /// Build a RecentChanges API Uri.
        /// </summary>
        /// <param name="year">The year the change was made in.</param>
        /// <param name="month">The month the change was made in.</param>
        /// <param name="day">The day the change was made in.</param>
        /// <param name="kind">The kind of change.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>A RecentChanges API Uri.</returns>
        public static Uri BuildRecentChangesUri(string year, string month, string day, string kind = "", params KeyValuePair<string, string>[] parameters)
            => BuildRecentChangesUri(year + "/" + month + "/" + day + (string.IsNullOrWhiteSpace(kind) ? "" : "/" + kind), parameters);
        /// <summary>
        /// Build a RecentChanges API Uri.
        /// </summary>
        /// <param name="year">The year the change was made in.</param>
        /// <param name="month">The month the change was made in.</param>
        /// <param name="kind">The kind of change.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>A RecentChanges API Uri.</returns>
        public static Uri BuildRecentChangesUri(string year, string month, string kind = "", params KeyValuePair<string, string>[] parameters)
            => BuildRecentChangesUri(year + "/" + month + (string.IsNullOrWhiteSpace(kind) ? "" : "/" + kind), parameters);
        /// <summary>
        /// Build a RecentChanges API Uri.
        /// </summary>
        /// <param name="year">The year the change was made in.</param>
        /// <param name="kind">The kind of change.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>A RecentChanges API Uri.</returns>
        public static Uri BuildRecentChangesUri(string year, string kind = "", params KeyValuePair<string, string>[] parameters)
            => BuildRecentChangesUri(year + (string.IsNullOrWhiteSpace(kind) ? "" : "/" + kind), parameters);
        /// <summary>
        /// Build a RecentChanges API Uri.
        /// </summary>
        /// <param name="kind">The kind of change.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>A RecentChanges API Uri.</returns>
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

        /// <summary>
        /// Build a Lists API Uri.
        /// </summary>
        /// <param name="username">The user the list belongs to.</param>
        /// <param name="id">The ID of the list.</param>
        /// <param name="path">The path of the Uri.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>A Lists API Uri.</returns>
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

        /// <summary>
        /// Build a MyBooks API Uri.
        /// </summary>
        /// <param name="username">The user whose reading logs you want to get.</param>
        /// <param name="path">The path of the Uri. Should be "want-to-read", "currently-reading" or "already-read".</param>
        /// <returns>A MyBooks API Uri.</returns>
        public static Uri BuildMyBooksUri(string username, string path)
        {
            return BuildUri
            (
                BaseURL,
                "people/" + username + "/books/" + path + ".json"
            );
        }

        /// <summary>
        /// Build a Covers API Uri.
        /// </summary>
        /// <param name="idType">The type of the ID.</param>
        /// <param name="id">The ID of the cover.</param>
        /// <param name="size">The size of the cover.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>A Covers API Uri.</returns>
        public static Uri BuildCoversUri(CoverIdType idType, string id, ImageSize size, params KeyValuePair<string, string>[] parameters)
            => BuildCoversUri(idType.GetString(), id, size.GetString(), parameters);
        /// <summary>
        /// Build a Covers API Uri.
        /// </summary>
        /// <param name="idType">The type of the ID.</param>
        /// <param name="id">The ID of the cover.</param>
        /// <param name="size">The size of the cover.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>A Covers API Uri.</returns>
        public static Uri BuildCoversUri(string idType, string id, string size, params KeyValuePair<string, string>[] parameters)
            => BuildCoversUri(idType + "/" + id + "-" + size, parameters);
        /// <summary>
        /// Build a Covers API Uri.
        /// </summary>
        /// <param name="key">The key of the cover.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>A Covers API Uri.</returns>
        public static Uri BuildCoversUri(string key, params KeyValuePair<string, string>[] parameters)
        {
            return BuildUri
            (
                BaseURL_Covers,
                "b/" + key + ".jpg",
                parameters
            );
        }

        /// <summary>
        /// Build an AuthorPhotos API Uri.
        /// </summary>
        /// <param name="idType">The type of the ID.</param>
        /// <param name="id">The ID of the AuthorPhoto.</param>
        /// <param name="size">The size of the AuthorPhoto.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>An AuthorPhotos API Uri.</returns>
        public static Uri BuildAuthorPhotosUri(AuthorPhotoIdType idType, string id, ImageSize size, params KeyValuePair<string, string>[] parameters)
            => BuildAuthorPhotosUri(idType.GetString(), id, size.GetString(), parameters);
        /// <summary>
        /// Build an AuthorPhotos API Uri.
        /// </summary>
        /// <param name="idType">The type of the ID.</param>
        /// <param name="id">The ID of the AuthorPhoto.</param>
        /// <param name="size">The size of the AuthorPhoto.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>An AuthorPhotos API Uri.</returns>
        public static Uri BuildAuthorPhotosUri(string idType, string id, string size, params KeyValuePair<string, string>[] parameters)
            => BuildAuthorPhotosUri(idType + "/" + id + "-" + size, parameters);
        /// <summary>
        /// Build an AuthorPhotos API Uri.
        /// </summary>
        /// <param name="key">The key of the AuthorPhoto.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>An AuthorPhotos API Uri.</returns>
        public static Uri BuildAuthorPhotosUri(string key, params KeyValuePair<string, string>[] parameters)
        {
            return BuildUri
            (
                BaseURL_Covers,
                "a/" + key + ".jpg",
                parameters
            );
        }

        /// <summary>
        /// Build a Partner API Uri.
        /// </summary>
        /// <param name="idType">The type of the ID.</param>
        /// <param name="id">The ID of the edition.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>A Partner API Uri.</returns>
        public static Uri BuildPartnerUri(PartnerIdType idType, string id, params KeyValuePair<string, string>[] parameters)
            => BuildPartnerUri(idType.GetString(), id, parameters);
        /// <summary>
        /// Build a Partner API Uri.
        /// </summary>
        /// <param name="idType">The type of the ID.</param>
        /// <param name="id">The ID of the edition.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>A Partner API Uri.</returns>
        public static Uri BuildPartnerUri(string idType, string id, params KeyValuePair<string, string>[] parameters)
            => BuildPartnerUri(idType + "/" + id, parameters);
        /// <summary>
        /// Build a Partner API Uri.
        /// </summary>
        /// <param name="bibkey">The bibkey of the edition.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>A Partner API Uri.</returns>
        public static Uri BuildPartnerUri(string bibkey, params KeyValuePair<string, string>[] parameters)
        {
            return BuildUri
            (
                BaseURL,
                RequestTypePrefixMap[OLRequestAPI.Partner] + "/" + bibkey + ".json",
                parameters
            );
        }

        /// <summary>
        /// Build a Partner API Uri.
        /// </summary>
        /// <param name="bibkeys">The bibkeys of the editions.</param>
        /// <returns>A Partner API Uri.</returns>
        public static Uri BuildPartnerMultiUri(params string[] bibkeys)
            => BuildPartnerMultiUri(bibkeys, Array.Empty<KeyValuePair<string, string>>());
        /// <summary>
        /// Build a Partner API Uri.
        /// </summary>
        /// <param name="bibkeys">The bibkeys of the editions.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>A Partner API Uri.</returns>
        public static Uri BuildPartnerMultiUri(string[] bibkeys, params KeyValuePair<string, string>[] parameters)
        {
            string concat;
            if (bibkeys.Length > 1)
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (string id in bibkeys)
                    stringBuilder.Append(id + "|");

                // Remove the trailing &
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
                concat = stringBuilder.ToString();
            }
            else concat = bibkeys[0];

            return BuildUri
            (
                BaseURL,
                RequestTypePrefixMap[OLRequestAPI.Partner] + "/json/" + concat,
                parameters
            );
        }

        /// <summary>
        /// Build an OpenLibrary Uri.
        /// </summary>
        /// <param name="api">The OpenLibrary API the Uri will make a request to..</param>
        /// <param name="path">The path of the Uri.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The constructed OpenLibrary uri.</returns>
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

        private static string SetPrefix(OLRequestAPI api, string path)
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

        private static string SetFileExtension(OLRequestAPI api, string path)
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
        /// <summary>
        /// Make a GET request and get the response as a string.<br/>
        /// For now, this is simply a wrapper around <see cref="HttpClient.GetStringAsync(Uri?)"/>.
        /// </summary>
        /// <param name="client">The HTTPClient to make the request with.</param>
        /// <param name="uri">The Uri to send the GET request to.</param>
        /// <returns></returns>
        public async static Task<string> RequestAsync(HttpClient client, Uri uri)
        {
            if (client == null) throw new System.ArgumentNullException(nameof(client));
            return await client.GetStringAsync(uri);
        }

        /// <summary>
        /// Make a GET request and get the deserialized response as the generic type.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
        /// <param name="client">The HTTPClient to make the request with.</param>
        /// <param name="uri">The Uri to send the the GET request to.</param>
        /// <param name="path">The path of the desired object within the response body. Leave as default to get entire object.</param>
        /// <returns>The deserialized object from the JSON string at the given <paramref name="path"/>.</returns>
        public async static Task<T?> LoadAsync<T>(HttpClient client, Uri uri, string path = "")
        {
            string response = await RequestAsync(client, uri);
            if (string.IsNullOrWhiteSpace(path))
                return JsonConvert.DeserializeObject<T>(response);
            else
            {
                JToken token = JToken.Parse(response);
                return token[path]!.ToObject<T>();
            }
        }
        #endregion

        /* Json Converters for the deserialization of data requested from openlibrary.org;
         * multiple data points come as various types (i.e., sometimes as KeyValuePair, sometimes as array)
         */
        #region Json Converters
        internal static class Serialization
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
                        if (array.Count == 0) return Array.Empty<string>();

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
                        if (array.Count == 0) return Array.Empty<string>();

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
                        if (array.Count == 0) return Array.Empty<string>();

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
         * Purely helper data and not enforced anywhere within OpenLibraryNET itself.
         * Not exhaustive.
         */
        #region Maps
        /// <summary>
        /// Maps each <see cref="OLRequestAPI"/> to its corresponding Uri prefix.
        /// </summary>
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
            {OLRequestAPI.Partner, "api/volumes/brief"},
            {OLRequestAPI.Covers, "b"},
            {OLRequestAPI.AuthorPhotos, "a"},
            {OLRequestAPI.RecentChanges, "recentchanges"},
            {OLRequestAPI.Lists, "people"}
            }
        );

        /// <summary>
        /// Maps each <see cref="OLRequestAPI"/> to its corresponding path parameters map.
        /// </summary>
        public static ReadOnlyDictionary<string, ImmutableArray<string>> GetPathParametersMap(OLRequestAPI api)
        {
            return api switch
            {
                OLRequestAPI.Books => BooksPathParametersMap,
                OLRequestAPI.Books_Works => WorksPathParametersMap,
                OLRequestAPI.Authors => AuthorsPathParametersMap,
                OLRequestAPI.Subjects => SubjectsPathParametersMap,
                OLRequestAPI.Search => SearchPathParametersMap,
                OLRequestAPI.Lists => ListsPathParametersMap,
                OLRequestAPI.Covers => CoversPathParametersMap,
                OLRequestAPI.AuthorPhotos => AuthorPhotosPathParametersMap,
                OLRequestAPI.RecentChanges => RecentChangesPathParametersMap,
                _ => new ReadOnlyDictionary<string, ImmutableArray<string>>(new Dictionary<string, ImmutableArray<string>>()),
            };
        }

        #region Books Maps
        /// <summary>
        /// Maps each of the valid Books API paths to its valid parameters.<br/>
        /// Pure helper structure that is NOT exhaustive.
        /// </summary>
        public static readonly ReadOnlyDictionary<string, ImmutableArray<string>> BooksPathParametersMap = new ReadOnlyDictionary<string, ImmutableArray<string>>
        (
            new Dictionary<string, ImmutableArray<string>>
            {
                {string.Empty, ImmutableArray.Create("bibkeys", "format", "callback", "jscmd") }
            }
        );
        #endregion

        #region Works Maps
        /// <summary>
        /// Maps each of the valid Works API paths to its valid parameters.<br/>
        /// Pure helper structure that is NOT exhaustive.
        /// </summary>
        public static readonly ReadOnlyDictionary<string, ImmutableArray<string>> WorksPathParametersMap = new ReadOnlyDictionary<string, ImmutableArray<string>>
        (
            new Dictionary<string, ImmutableArray<string>>
            {
                {string.Empty, Array.Empty<string>().ToImmutableArray() },
                {"editions", ImmutableArray.Create( "limit", "offset") },
                {"bookshelves", Array.Empty<string>().ToImmutableArray() },
                {"ratings", Array.Empty<string>().ToImmutableArray() },
            }
        );

        #endregion

        #region Authors Maps
        /// <summary>
        /// Maps each of the valid Authors API paths to its valid parameters.<br/>
        /// Pure helper structure that is NOT exhaustive.
        /// </summary>
        public static readonly ReadOnlyDictionary<string, ImmutableArray<string>> AuthorsPathParametersMap = new ReadOnlyDictionary<string, ImmutableArray<string>>
        (
            new Dictionary<string, ImmutableArray<string>>
            {
                {string.Empty, Array.Empty<string>().ToImmutableArray() },
                {"works", ImmutableArray.Create("offset", "limit") },
            }
        );
        #endregion

        #region Subjects Maps
        /// <summary>
        /// Maps each of the valid Subjects API paths to its valid parameters.<br/>
        /// Pure helper structure that is NOT exhaustive.
        /// </summary>
        public static readonly ReadOnlyDictionary<string, ImmutableArray<string>> SubjectsPathParametersMap = new ReadOnlyDictionary<string, ImmutableArray<string>>
        (
            new Dictionary<string, ImmutableArray<string>>
            {
                {string.Empty, ImmutableArray.Create("details", "ebooks", "published_in", "limit", "offset", "sort") }
            }
        );
        #endregion

        #region Search Maps
        /// <summary>
        /// Maps each of the valid Search API paths to its valid parameters.<br/>
        /// Pure helper structure that is NOT exhaustive.
        /// </summary>
        public static readonly ReadOnlyDictionary<string, ImmutableArray<string>> SearchPathParametersMap = new ReadOnlyDictionary<string, ImmutableArray<string>>
        (
            new Dictionary<string, ImmutableArray<string>>
            {
                {string.Empty, ImmutableArray.Create("q", "title", "author", "subject", "place", "person", "language", "publisher", "sort") },
                {"authors", ImmutableArray.Create( "q", "sort", "limit", "offset") },
                {"subjects", ImmutableArray.Create("q", "limit", "offset") },
                {"lists", ImmutableArray.Create("q", "limit", "offset") },
            }
        );
        #endregion

        #region Covers Maps
        /// <summary>
        /// Maps each of the valid Covers API paths to its valid parameters.<br/>
        /// Pure helper structure that is NOT exhaustive.
        /// </summary>
        public static readonly ReadOnlyDictionary<string, ImmutableArray<string>> CoversPathParametersMap = new ReadOnlyDictionary<string, ImmutableArray<string>>
        (
            new Dictionary<string, ImmutableArray<string>>
            {
                {string.Empty, ImmutableArray.Create("default") }
            }
        );
        #endregion

        #region AuthorPhotos Maps
        /// <summary>
        /// Maps each of the valid AuthorPhotos API paths to its valid parameters.<br/>
        /// Pure helper structure that is NOT exhaustive.
        /// </summary>
        public static readonly ReadOnlyDictionary<string, ImmutableArray<string>> AuthorPhotosPathParametersMap = new ReadOnlyDictionary<string, ImmutableArray<string>>
        (
            new Dictionary<string, ImmutableArray<string>>
            {
                {string.Empty, ImmutableArray.Create("default") }
            }
        );
        #endregion

        #region Lists Maps
        /// <summary>
        /// Maps each of the valid Lists API paths to its valid parameters.<br/>
        /// Pure helper structure that is NOT exhaustive.
        /// </summary>
        public static readonly ReadOnlyDictionary<string, ImmutableArray<string>> ListsPathParametersMap = new ReadOnlyDictionary<string, ImmutableArray<string>>
        (
            new Dictionary<string, ImmutableArray<string>>
            {
                {string.Empty, Array.Empty<string>().ToImmutableArray() },
                {"editions", ImmutableArray.Create("limit", "offset") },
                {"subjects", ImmutableArray.Create("limit", "offset") }
            }
        );
        #endregion

        #region RecentChanges Maps
        /// <summary>
        /// Maps each of the valid RecentChanges API paths to its valid parameters.<br/>
        /// Pure helper structure that is NOT exhaustive.
        /// </summary>
        public static readonly ReadOnlyDictionary<string, ImmutableArray<string>> RecentChangesPathParametersMap = new ReadOnlyDictionary<string, ImmutableArray<string>>
        (
            new Dictionary<string, ImmutableArray<string>>
            {
                {string.Empty, ImmutableArray.Create("limit", "offset", "bot") }
            }
        );
        #endregion
        #endregion
    }
}
