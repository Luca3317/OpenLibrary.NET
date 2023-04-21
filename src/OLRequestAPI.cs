namespace OpenLibraryNET
{
    public enum OLRequestAPI
    {
        Books,          // DONE
        Books_Works,    // DONE
        Books_Editions, // DONE
        Books_ISBN,     // DONE
        Authors,        // DONE
        Subjects,       // DONE
        Search,         // DONE
        Partner,        // DONE
        Covers,         // DONE
        AuthorPhotos,   // DONE
        RecentChanges,  // DONE
        Lists           // DONE; The way this api currently work seems strange, more like one of the aspects of the /people/ api.
            // This is reflected in the RequestTypePrefixMap for this api (prefix: people).
            // This is a necessary workaround for now.
    }
}
