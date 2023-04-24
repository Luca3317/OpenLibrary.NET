namespace OpenLibraryNET.Utility
{
    /// <summary>
    /// The APIs of OpenLibrary currently supported by OpenLibrary.NET
    /// </summary>
    public enum OLRequestAPI
    {
        /// <summary>
        /// The Books API. Get data about individual books by bibkey (see <see cref="BookIdType"/>).
        /// </summary>
        Books,
        /// <summary>
        /// The Works API. Get data about works by OLID.
        /// </summary>
        Books_Works,
        /// <summary>
        /// The Editions API. Get data about editions by OLID.
        /// </summary>
        Books_Editions,
        /// <summary>
        /// The ISBN API. Get data about editions by ISBN.
        /// </summary>
        Books_ISBN,
        /// <summary>
        /// The Authors API. Get data about authors by OLID.
        /// </summary>
        Authors,
        /// <summary>
        /// The Subjects API. Get data about subjects.
        /// </summary>
        Subjects,
        /// <summary>
        /// The Search API. Search for authors, work, subjects and lists or search inside books.
        /// </summary>
        Search,
        /// <summary>
        /// The Partner API. Get data about online-readable or borrowable books by bibkey (see <see cref="PartnerIdType"/>).
        /// </summary>
        Partner,
        /// <summary>
        /// The Covers API. Get covers of books by ID (see <see cref="CoverIdType"/>).
        /// </summary>
        Covers,
        /// <summary>
        /// The AuthorPhotos API. Get photos of authors by ID (see <see cref="AuthorPhotoIdType"/>).
        /// </summary>
        AuthorPhotos,
        /// <summary>
        /// The RecentChanges API. Get recent changes made to OpenLibrary's data records.
        /// </summary>
        RecentChanges,
        /// <summary>
        /// The Lists API. Get, create or delete user-created lists.
        /// </summary>
        Lists
    }
}
