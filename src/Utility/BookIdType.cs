namespace OpenLibraryNET.Utility
{
    /// <summary>
    /// Valid ID types for edition IDs.
    /// </summary>
    public enum BookIdType
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
        OCLC
    }
}
