using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace OpenLibraryNET.Data
{
    /// <summary>
    /// Holds data about a user-created list.
    /// </summary>
    public sealed record OLListData : OLContainer
    {
        /// <summary>
        /// The list's ID.
        /// </summary>
        public string ID => Regex.Match(URL, "(?<=/)[^/]+$").ToString();

        /// <summary>
        /// The list's name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; init; } = "";
        /// <summary>
        /// The URL pointing to the list.
        /// </summary>
        [JsonProperty("url")]
        public string URL { get; init; } = "";
        /// <summary>
        /// The timestamp of the last update made to the list.
        /// </summary>
        [JsonProperty("last_update")]
        public string LastUpdate { get; init; } = "";

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.<br/>
        /// Does NOT consider extension data.
        /// </summary>
        /// <param name="data">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the other parameter; otherwise, false.</returns>
        public bool Equals(OLListData? data)
        {
            return data != null &&
                this.Name == data.Name &&
                this.URL == data.URL &&
                this.LastUpdate == data.LastUpdate;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode() => base.GetHashCode();
    }
}
