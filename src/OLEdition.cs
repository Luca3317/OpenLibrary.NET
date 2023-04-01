using Newtonsoft.Json;

/*
 * Represent OpenLibrary objects, being aggregates of multiple requests.
 * 
 * TODO
 * tryget in loaders DONE
 * Add builders to these
 * Make loaders sub classes of the objects? ie OLWork.Loader.getratingsasync
 */

namespace OpenLibrary.NET
{
    /// <summary>
    /// Represents an (OpenLibrary) edition.
    /// </summary>
    public sealed record OLEdition : OLContainer
    {
        [JsonProperty("ID")]
        public string ID { get; private set; }
        [JsonProperty("Data")]
        public OLEditionData? Data { get; init; } = null;

        public OLEdition(string id) => ID = id;

        public bool Equals(OLEdition? edition)
        {
            return edition != null &&
                edition.ID == ID &&
                edition.Data == Data;
        }
        public override int GetHashCode() => base.GetHashCode();
    }
}