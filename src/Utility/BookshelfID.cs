using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLibraryNET.Utility
{
    /// <summary>
    /// Valid Bookshelf IDs.
    /// </summary>
    public enum BookshelfID
    {
        /// <summary>
        /// ID corresponding to Want-To-Read reading log
        /// </summary>
        WantToRead = 1,
        /// <summary>
        /// ID corresponding to Currently-Reading reading log
        /// </summary>
        CurrentlyReading = 2,
        /// <summary>
        /// ID corresponding to Already-Read reading log
        /// </summary>
        AlreadyRead = 3
    }
}
