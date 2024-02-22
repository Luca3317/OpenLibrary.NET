using Newtonsoft.Json;
using System.Collections.ObjectModel;
using OpenLibraryNET.Data;
using CodeGeneration_Attributes;

namespace OpenLibraryNET
{
    /// <summary>
    /// Composite storage of various data related to an edition.
    /// </summary>
    [CollectionValueEquality]
    public sealed partial record OLEdition
    {
        /// <summary>
        /// The ID of the edition.
        /// </summary>
        [JsonProperty("id")]
        public string ID { get; init; } = "";
        /// <summary>
        /// Data about the edition itself.
        /// </summary>
        [JsonProperty("data")]
        public OLEditionData? Data { get; init; } = null;

        /// <summary>
        /// The cover of the edition, in small resolution.
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<byte>? CoverS
        {
            get => _cover_S == null ? null : new ReadOnlyCollection<byte>(_cover_S);
            init { if (value == null) _cover_S = null; else _cover_S = value.ToArray(); }
        }
        /// <summary>
        /// The cover of the edition, in medium resolution.
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<byte>? CoverM
        {
            get => _cover_M == null ? null : new ReadOnlyCollection<byte>(_cover_M);
            init { if (value == null) _cover_M = null; else _cover_M = value.ToArray(); }
        }
        /// <summary>
        /// The cover of the edition, in large resolution.
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<byte>? CoverL
        {
            get => _cover_L == null ? null : new ReadOnlyCollection<byte>(_cover_L);
            init { if (value == null) _cover_L = null; else _cover_L = value.ToArray(); }
        }

        [JsonProperty("cover_s")]
        private byte[]? _cover_S { get; init; } = null;
        [JsonProperty("cover_m")]
        private byte[]? _cover_M { get; init; } = null;
        [JsonProperty("cover_l")]
        private byte[]? _cover_L { get; init; } = null;
    }
}