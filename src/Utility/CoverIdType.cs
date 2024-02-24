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
        ISBN = 10,
        /// <summary>
        /// OpenLibrary ID
        /// </summary>
        OLID = 0,
        /// <summary>
        /// Library of Congress Control Number
        /// </summary>
        LCCN = 20,
        /// <summary>
        /// OCLC (Online Computer Library Center)
        /// </summary>
        OCLC = 30,
        /// <summary>
        /// Generic ID
        /// </summary>
        ID = 40
    }
}
