using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLibraryNET
{
    public enum EditionIdType
    {
        ISBN,
        OLID,
        LCCN,
        OCLC
    }

    static class EditionIdTypeMethods
    {
        public static string ToString(this EditionIdType idType)
        {
            switch (idType)
            {
                case EditionIdType.ISBN: return "ISBN";
                case EditionIdType.OLID: return "ISBN";
                case EditionIdType.OCLC: return "OCLC";
                case EditionIdType.LCCN: return "LCCN";
            }

            throw new System.Exception();
        }
    }
}
