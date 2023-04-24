using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace OpenLibraryNET.Data
{
    /// <summary>
    /// Base record for OLData records.<br/>
    /// Implements ExtensionData.
    /// </summary>
    public record OLContainer
    {
        /// <summary>
        /// This record's extension data.
        /// </summary>
        [JsonIgnore]
        public ReadOnlyDictionary<string, JToken>? ExtensionData =>
            extensionData == null ? null : new ReadOnlyDictionary<string, JToken>(extensionData);

        /// <summary>
        /// This record's extension data.
        /// </summary>
        [JsonExtensionData]
        [JsonProperty("extension_data")]
        protected Dictionary<string, JToken>? extensionData = null;
    }
}
