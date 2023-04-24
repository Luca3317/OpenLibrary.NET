namespace OpenLibraryNET.Utility
{
    /// <summary>
    /// Valid ID types for cover IDs.
    /// </summary>
    public enum CoverIdType
    {
        /// <summary>
        /// ISBN (10 and 13)
        /// </summary>
        ISBN,
        /// <summary>
        /// OpenLibrary ID
        /// </summary>
        OLID,
        /// <summary>
        /// Library of Congress Control Number
        /// </summary>
        LCCN,
        /// <summary>
        /// OCLC (Online Computer Library Center)
        /// </summary>
        OCLC,
        /// <summary>
        /// Generic ID
        /// </summary>
        ID
    }
}
