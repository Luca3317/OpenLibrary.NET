using Newtonsoft.Json;
using System.Collections.ObjectModel;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET.Data
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
        [JsonProperty("subject_type")]
        public string SubjectType { get; init; } = "subject";

        [JsonIgnore]
        public IReadOnlyList<OLWorkData> Works
        {
            get => new ReadOnlyCollection<OLWorkData>(_works);
            init => _works = value.ToArray();
        }

        [JsonProperty("works")]
        private OLWorkData[] _works { get; init; } = Array.Empty<OLWorkData>();

        public bool Equals(OLSubjectData? data)
        {
            return
                data != null &&
                CompareExtensionData(data.extensionData) &&
                GeneralUtility.SequenceEqual(this.extensionData, data.extensionData) &&
                GeneralUtility.SequenceEqual(this.Works, data.Works) &&
                this.Name == data.Name &&
                this.WorkCount == data.WorkCount;
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}
