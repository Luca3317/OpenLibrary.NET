namespace OpenLibraryNET.Utility
{
    public static class ExtensionMethods
    {
        public static string GetString(this ImageSize size)
        {
            return size switch
            {
                ImageSize.Small => "S",
                ImageSize.Medium => "M",
                ImageSize.Large => "L",
                _ => throw new System.NotImplementedException(),
            };
        }

        public static string GetString(this CoverIdType idType)
        {
            switch (idType)
            {
                case CoverIdType.ISBN: return "ISBN";
                case CoverIdType.OLID: return "OLID";
                case CoverIdType.OCLC: return "OCLC";
                case CoverIdType.LCCN: return "LCCN";
                case CoverIdType.ID: return "ID";
            }

            throw new System.NotImplementedException();
        }

        public static string GetString(this EditionIdType idType)
        {
            switch (idType)
            {
                case EditionIdType.ISBN: return "ISBN";
                case EditionIdType.OLID: return "OLID";
                case EditionIdType.OCLC: return "OCLC";
                case EditionIdType.LCCN: return "LCCN";
            }

            throw new System.Exception();
        }

        public static string GetString(this AuthorPhotoIdType idType)
        {
            switch (idType)
            {
                case AuthorPhotoIdType.OLID: return "OLID";
                case AuthorPhotoIdType.ID: return "ID";
            }

            throw new System.NotImplementedException();
        }
    }
}
