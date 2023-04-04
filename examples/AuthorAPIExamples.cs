using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLibrary.NET.examples
{
    internal class AuthorAPIExamples
    {
        /*
        * Given an author's OLID, get all their information
        */
        public async void GetAuthor()
        {
            // The author's OLID
            string id = "OL31827A";

            // Create author and load all their information
            // These values, once loaded, are stored in author
            OLAuthor author = new OLAuthor(id);
            await author.GetDataAsync();
            await author.GetTotalWorksCountAsync();
            await author.RequestWorksAsync(author.TotalWorks);
        }

        /*
         * Get an author without having a prior author OLID,
         * using the search function
         */
        public async void GetAuthorFromSearch()
        {
            // The search query
            string query = "James Joyce";

            // Request the first author returned by the search
            var results = await OLSearchLoader.GetAuthorSearchResultsAsync(query, new KeyValuePair<string, string>("limit", "1"));
           
            // Ensure there were enough search results
            if (results == null || results.Length == 0)
            {
                // Error handling code
                return;
            }

            OLAuthor author = new OLAuthor(results[0]);
        }

        /*
         * Given an author's OLID, get only their OLAuthorData
         */
        public async void GeAuthorData()
        {
            // The author's OLID
            string id = "OL31827A";

            OLAuthorData? data = await OLAuthorLoader.GetDataAsync(id);
        }
    }
}
