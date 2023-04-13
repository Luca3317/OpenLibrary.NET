using Newtonsoft.Json;
using System.Collections.ObjectModel;
using OpenLibraryNET.Data;

namespace OpenLibraryNET
{
    /// <summary>
    /// Represents an (OpenLibrary) edition.
    /// </summary>
    public sealed record OLEdition
    {
        [JsonProperty("id")]
        public string ID { get; init; } = "";
        [JsonProperty("data")]
        public OLEditionData? Data { get; init; } = null;

        [JsonIgnore]
        public IReadOnlyList<byte>? CoverS
        {
            get => cover_S == null ? null : new ReadOnlyCollection<byte>(cover_S);
            init { if (value == null) cover_S = null; else cover_S = value.ToArray(); }
        }
        [JsonIgnore]
        public IReadOnlyList<byte>? CoverM
        {
            get => cover_M == null ? null : new ReadOnlyCollection<byte>(cover_M);
            init { if (value == null) cover_M = null; else cover_M = value.ToArray(); }
        }
        [JsonIgnore]
        public IReadOnlyList<byte>? CoverL
        {
            get => cover_L == null ? null : new ReadOnlyCollection<byte>(cover_L);
            init { if (value == null) cover_L = null; else cover_L = value.ToArray(); }
        }

        [JsonProperty("cover_s")]
        private byte[]? cover_S { get; init; } = null;
        [JsonProperty("cover_m")]
        private byte[]? cover_M { get; init; } = null;
        [JsonProperty("cover_l")]
        private byte[]? cover_L { get; init; } = null;

        public bool Equals(OLEdition? edition)
        {
            return edition != null &&
                edition.ID == ID &&
                edition.Data == Data;
        }
        public override int GetHashCode() => base.GetHashCode();
    }
}