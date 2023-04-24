using Newtonsoft.Json;

namespace OpenLibraryNET.Data
{
    /// <summary>
    /// Holds data about a <see cref="OLWorkData"/>'s ratings.
    /// </summary>
    public sealed record OLRatingsData : OLContainer
    {
        /// <summary>
        /// The corresponding work's average rating. Null if there are no ratings.
        /// </summary>
        [JsonProperty("average")]
        public float? Average { get; init; } = null;
        /// <summary>
        /// The amount of ratings the corresponding work has received.
        /// </summary>
        [JsonProperty("count")]
        public int Count { get; init; } = -1;

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.<br/>
        /// Does NOT consider extension data.
        /// </summary>
        /// <param name="data">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the other parameter; otherwise, false.</returns>
        public bool Equals(OLRatingsData? data)
        {
            return data != null &&
                this.Average == data.Average &&
                this.Count == data.Count;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode() => base.GetHashCode();
    }
}
