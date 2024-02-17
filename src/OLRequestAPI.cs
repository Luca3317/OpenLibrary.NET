namespace OpenLibraryNET
{
    /// <summary>
    /// The APIs of OpenLibrary currently supported by OpenLibrary.NET
    /// </summary>
    public enum OLRequestAPI : int
    {
        /// <summary>
        /// The Books API. Get data about individual books by bibkey (see <see cref="Utility.BookIdType"/>).
        /// </summary>
        Books = 0,
        /// <summary>
        /// The Works API. Get data about works by OLID.
        /// </summary>
        Books_Works = 5,
        /// <summary>
        /// The Editions API. Get data about editions by OLID.
        /// </summary>
        Books_Editions = 10,
        /// <summary>
        /// The ISBN API. Get data about editions by ISBN.
        /// </summary>
        Books_ISBN = 15,
        /// <summary>
        /// The Authors API. Get data about authors by OLID.
        /// </summary>
        Authors = 20,
        /// <summary>
        /// The Subjects API. Get data about subjects.
        /// </summary>
        Subjects = 25,
        /// <summary>
        /// The Search API. Search for authors, work, subjects and lists or search inside books.
        /// </summary>
        Search = 30,
        /// <summary>
        /// The Partner API. Get data about online-readable or borrowable books by bibkey (see <see cref="Utility.PartnerIdType"/>).
        /// </summary>
        Partner = 35,
        /// <summary>
        /// The Covers API. Get covers of books by ID (see <see cref="Utility.CoverIdType"/>).
        /// </summary>
        Covers = 40,
        /// <summary>
        /// The AuthorPhotos API. Get photos of authors by ID (see <see cref="Utility.AuthorPhotoIdType"/>).
        /// </summary>
        AuthorPhotos = 45,
        /// <summary>
        /// The RecentChanges API. Get recent changes made to OpenLibrary's data records.
        /// </summary>
        RecentChanges = 50,
        /// <summary>
        /// The Lists API. Get, create or delete user-created lists.
        /// </summary>
        Lists = 55,
        /// <summary>
        /// The MyBooks API. Get reading logs of a public user (or of your own account, if logged in).
        /// </summary>
        MyBooks = 60
    }
}
