using Newtonsoft.Json;

namespace OpenLibraryNET.Data
{
    /// <summary>
    /// Holds data about a <see cref="OLWorkData"/>'s bookshelves.
    /// </summary>
    public sealed record OLBookshelvesData : OLContainer
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

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.<br/>
        /// Compares purely by value, including collections.
        /// </summary>
        /// <param name="data">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the other parameter; otherwise, false.</returns>
        public bool Equals(OLBookshelvesData? data)
        {
            return data != null &&
                this.WantToRead == data.WantToRead &&
                this.AlreadyRead == data.AlreadyRead &&
                this.CurrentlyReading == data.CurrentlyReading;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode() => base.GetHashCode();
    }
}
