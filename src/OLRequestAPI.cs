﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLibrary.NET
{
    public enum OLRequestAPI
    {
        Books,          // TODO
        Books_Works,    // DONE
        Books_Editions, // DONE
        Books_ISBN,     // DONE
        Authors,        // DONE
        Subjects,       // DONE
        Search,         // DONE
        SearchInside,   // TODO; Probably wont
        Partner,        // TODO; Probably wont
        Covers,         // DONE
        AuthorPhotos,   // DONE
        RecentChanges   // TODO; Probably wont
    }
}