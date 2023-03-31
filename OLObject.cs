using Newtonsoft.Json;
using OpenLibrary.NET;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
    /// Represents an (OpenLibrary) work.
    /// </summary>
    public sealed record OLWork : OLContainer
    {
        [JsonProperty("ID")]
        public string ID { get; private set; }
        [JsonProperty("Data")]
        public OLWorkData? Data { get; init; } = null;
        [JsonProperty("Ratings")]
        public OLRatingsData? Ratings { get; init; } = null;
        [JsonProperty("Bookshelves")]
        public OLBookshelvesData? Bookshelves { get; init; } = null;
        [JsonProperty("Editions")]
        public ReadOnlyCollection<OLEditionData>? Editions { get; init; } = null;

        public OLWork(string id) => ID = id;

        public bool Equals(OLWork? work)
        {
            return work != null &&
                work.ID == ID &&
                work.Data == Data &&
                work.Ratings == Ratings &&
                work.Bookshelves == Bookshelves &&
                Enumerable.SequenceEqual(work.Editions!, Editions!);
        }
        public override int GetHashCode() => base.GetHashCode();
    }

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

    /// <summary>
    /// Represents an (OpenLibrary) author.
    /// </summary>
    public sealed record OLAuthor : OLContainer
    {
        [JsonProperty("ID")]
        public string ID { get; private set; }
        [JsonProperty("Data")]
        public OLAuthorData? Data { get; init; } = null;
        [JsonProperty("Works")]
        public ReadOnlyCollection<OLWorkData>? Works { get; init; } = null;

        public OLAuthor(string id) => ID = id;

        public bool Equals(OLAuthor? author)
        {
            return author != null &&
                author.ID == ID &&
                author.Data == Data &&
                Enumerable.SequenceEqual(author.Works!, Works!);
        }
        public override int GetHashCode() => base.GetHashCode();
    }
}