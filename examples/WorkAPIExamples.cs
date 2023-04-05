using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLibrary.NET.examples
{
    public class WorkAPIExamples
    {
        /*
         * Given a work's OLID, get all its information
         */
        public async Task GetWork()
        {
            // The work's OLID
            string id = "OL29583061W";

            // Create work and load all its data
            // These values, once loaded, are stored in work
            OLWork work = new OLWork(id);
            await work.GetDataAsync();
            await work.GetRatingsAsync();
            await work.GetBookshelvesAsync();
            await work.GetTotalEditionCountAsync();
            await work.RequestEditionsAsync(work.TotalEditions);
        }

        /*
         * Get a work without having a prior work OLID,
         * using the search function
         */
        public async Task GetWorkFromSearch()
        {
            // The search query
            string query = "Tom sawyer";

            // Request the first work returned by the search
            var results = await OLSearchLoader.GetSearchResultsAsync(query, new KeyValuePair<string, string>("limit", "1"));
            
            // Ensure there were enough search results
            if (results == null || results.Length == 0)
            {
                // Error handling code
                return;
            }

            OLWork workData = new OLWork(results[0]);
        }

        /*
         * Given a work's OLID, get only its ratings
         */
        public async Task GetRatings()
        {
            // The work's OLID
            string id = "OL29583061W";

            OLRatingsData? ratings = await OLWorkLoader.GetRatingsAsync(id);
        }
    }
}
