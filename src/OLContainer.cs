using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLibrary.NET
{
    /// <summary>
    /// Base record for OLData and OLObject records.
    /// Implements ExtensionData.
    /// </summary>
    public abstract record OLContainer
    {
        [JsonIgnore]
        public ReadOnlyDictionary<string, JToken>? ExtensionData =>
            extensionData == null ? null : new ReadOnlyDictionary<string, JToken>(extensionData);

        [JsonExtensionData]
        [JsonProperty("extension_data")]
        protected Dictionary<string, JToken>? extensionData = null;

        protected bool CompareExtensionData(Dictionary<string, JToken>? data)
        {
            if (extensionData == null && data == null) return true;
            if (extensionData == null || data == null) return false;
            return Enumerable.SequenceEqual(extensionData.Keys, data.Keys);
        }
    }
}
