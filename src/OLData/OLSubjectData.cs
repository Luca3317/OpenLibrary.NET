using Newtonsoft.Json;
using System.Collections.ObjectModel;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET.Data
{
    /// <summary>
    /// Holds data about a subject.
    /// </summary>
    public sealed record OLSubjectData : OLContainer
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


        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.<br/>
        /// Compares purely by value, including collections.<br/>
        /// Does NOT consider extension data.
        /// </summary>
        /// <param name="data">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the other parameter; otherwise, false.</returns>
        public bool Equals(OLSubjectData? data)
        {
            return
                data != null &&
                GeneralUtility.SequenceEqual(this.extensionData, data.extensionData) &&
                GeneralUtility.SequenceEqual(this.Works, data.Works) &&
                this.Name == data.Name &&
                this.WorkCount == data.WorkCount;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode() => base.GetHashCode();
    }
}
