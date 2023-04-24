using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace OpenLibraryNET.Data
{
    /// <summary>
    /// Base record for OLData and OLObject records.
    /// Implements ExtensionData.
    /// </summary>
    public record OLContainer
    {
        [JsonIgnore]
        public ReadOnlyDictionary<string, JToken>? ExtensionData =>
            extensionData == null ? null : new ReadOnlyDictionary<string, JToken>(extensionData);

        [JsonExtensionData]
        [JsonProperty("extension_data")]
        protected Dictionary<string, JToken>? extensionData = null;
    }
}
