using Newtonsoft.Json;
using CodeGeneration_Attributes;

namespace OpenLibraryNET.Data
{
    /// <summary>
    /// Holds data about a <see cref="OLWorkData"/>'s bookshelves.
    /// </summary>
    [CollectionValueEquality]
    public sealed partial record OLBookshelvesData : OLContainer
    {
        /// <summary>
        /// The amount of accounts that marked the corresponding work as "Want to read".
        /// </summary>
        [JsonProperty("want_to_read")]
        public int WantToRead { get; init; } = -1;
        /// <summary>
        /// The amount of accounts that marked the corresponding work as "Currently reading".
        /// </summary>
        [JsonProperty("currently_reading")]
        public int CurrentlyReading { get; init; } = -1;
        /// <summary>
        /// The amount of accounts that marked the corresponding work as "Already read".
        /// </summary>
        [JsonProperty("already_read")]
        public int AlreadyRead { get; init; } = -1;
    }
}
