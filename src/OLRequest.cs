using System.Collections.ObjectModel;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET
{
    public record OLRequest
    {
        public OLRequestAPI? API { get; init; }
        public string? ID { get; init; }
        public string? Path { get; init; }
        public ReadOnlyDictionary<string, string> Params { get; init; }
            = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>());
    }
}