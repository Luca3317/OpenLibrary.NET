using Newtonsoft.Json;
using System.Collections.ObjectModel;
using OpenLibraryNET.Utility;
using CodeGeneration_Attributes;

namespace OpenLibraryNET.Data
{
    /// <summary>
    /// Holds data about a subject.
    /// </summary>
    [CollectionValueEquality]
    public sealed partial record OLSubjectData : OLContainer
    {
        /// <summary>
        /// The name of this subject.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; init; } = "";
        /// <summary>
        /// The amount of works associated with this subject.
        /// </summary>
        [JsonProperty("work_count")]
        public int WorkCount { get; init; } = -1;
        /// <summary>
        /// The type of this subject.
        /// </summary>
        [JsonProperty("subject_type")]
        public string SubjectType { get; init; } = "subject";

        /// <summary>
        /// The works associated with this subject.
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<OLWorkData> Works
        {
            get => new ReadOnlyCollection<OLWorkData>(_works);
            init => _works = value.ToArray();
        }

        [JsonProperty("works")]
        private OLWorkData[] _works { get; init; } = Array.Empty<OLWorkData>();
    }
}
