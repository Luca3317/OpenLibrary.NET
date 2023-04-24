namespace OpenLibraryNET
{
    /// <summary>
    /// Valid ID types for partner API requests.
    /// </summary>
    public enum PartnerIdType
    {
        /// <summary>
        /// ISBN (10 and 13)
        /// </summary>
        ISBN,
        /// <summary>
        /// OCLC (Online Computer Library Center)
        /// </summary>
        OCLC,
        /// <summary>
        /// Library of Congress Control Number
        /// </summary>
        LCCN,
        /// <summary>
        /// OpenLibrary ID
        /// </summary>
        OLID
    }
}
