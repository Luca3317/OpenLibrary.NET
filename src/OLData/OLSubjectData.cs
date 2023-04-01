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
    /// Represents an OpenLibrary Subjects request.
    /// </summary>
    public sealed record OLSubjectData : OLContainer
    {
        [JsonProperty("name")]
        public string Name { get; init; } = "";
        [JsonProperty("work_count")]
        public int WorkCount { get; init; } = -1;

        [JsonIgnore]
        public ReadOnlyCollection<OLWorkData> Works => works.AsReadOnly();

        [JsonProperty("works")]
        private List<OLWorkData> works { get; init; } = new List<OLWorkData>();

        public bool Equals(OLSubjectData? data)
        {
            return
                data != null &&
                CompareExtensionData(data.extensionData) &&
                SequenceEqual(this.extensionData, data.extensionData) &&
                SequenceEqual(this.Works, data.Works) &&
                this.Name == data.Name &&
                this.WorkCount == data.WorkCount;
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}
