namespace OpenLibraryNET.Utility
{
    /// <summary>
    /// Extension methods for OpenLibrary.NET.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Get <see cref="ImageSize"/> as string.
        /// </summary>
        /// <param name="size">The image size.</param>
        /// <returns>The image size as string.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static string GetString(this ImageSize size)
        {
            return size switch
            {
                ImageSize.Small => "S",
                ImageSize.Medium => "M",
                ImageSize.Large => "L",
                _ => throw new System.ArgumentException(),
            };
        }

        /// <summary>
        /// Get <see cref="CoverIdType"/> as string.
        /// </summary>
        /// <param name="idType">The ID type.</param>
        /// <returns>The ID type as string.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static string GetString(this CoverIdType idType)
        {
            return idType switch
            {
                CoverIdType.ISBN => "ISBN",
                CoverIdType.OLID => "OLID",
                CoverIdType.OCLC => "OCLC",
                CoverIdType.LCCN => "LCCN",
                CoverIdType.ID => "ID",
                _ => throw new System.ArgumentException(),
            };
        }

        /// <summary>
        /// Get <see cref="BookIdType"/> as string.
        /// </summary>
        /// <param name="idType">The ID type.</param>
        /// <returns>The ID type as string.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static string GetString(this BookIdType idType)
        {
            return idType switch
            {
                BookIdType.ISBN => "ISBN",
                BookIdType.OLID => "OLID",
                BookIdType.OCLC => "OCLC",
                BookIdType.LCCN => "LCCN",
                _ => throw new System.ArgumentException(),
            };
        }

        /// <summary>
        /// Get <see cref="AuthorPhotoIdType"/> as string.
        /// </summary>
        /// <param name="idType">The ID type.</param>
        /// <returns>The ID type as string.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static string GetString(this AuthorPhotoIdType idType)
        {
            return idType switch
            {
                AuthorPhotoIdType.OLID => "OLID",
                AuthorPhotoIdType.ID => "ID",
                _ => throw new System.ArgumentException(),
            };
        }

        /// <summary>
        /// Get <see cref="PartnerIdType"/> as string.
        /// </summary>
        /// <param name="idType">The ID type.</param>
        /// <returns>The ID type as string.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static string GetString(this PartnerIdType idType)
        {
            return idType switch
            {
                PartnerIdType.OLID => "olid",
                PartnerIdType.ISBN => "isbn",
                PartnerIdType.OCLC => "oclc",
                PartnerIdType.LCCN => "lccn",
                _ => throw new System.ArgumentException(),
            };
        }
    }
}
