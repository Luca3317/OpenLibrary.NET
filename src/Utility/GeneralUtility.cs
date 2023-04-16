namespace OpenLibraryNET.Utility
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

