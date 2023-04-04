using Newtonsoft.Json.Linq;
using OpenLibrary.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLibrary
{
    internal static class GeneralUtility
    {
        public static bool SequenceEqual<T>(IEnumerable<T>? first, IEnumerable<T>? second)
        {
            if (first == null && second == null) return true;
            if (first == null || second == null) return false;
            return Enumerable.SequenceEqual(first, second);
        }
    }
}

