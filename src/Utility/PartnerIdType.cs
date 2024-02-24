namespace OpenLibraryNET.Utility
{
    /// <summary>
    /// Valid ID types for partner API requests.
    /// </summary>
    public enum PartnerIdType
    {
        /// <summary>
        /// ISBN (10 and 13)
        /// </summary>
        ISBN = 10,
        /// <summary>
        /// OCLC (Online Computer Library Center)
        /// </summary>
        OCLC = 30,
        /// <summary>
        /// Library of Congress Control Number
        /// </summary>
        LCCN = 20,
        /// <summary>
        /// OpenLibrary ID
        /// </summary>
        OLID = 0
    }
}
