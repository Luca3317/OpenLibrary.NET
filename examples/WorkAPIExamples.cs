using OpenLibraryNET.Loader;
using OpenLibraryNET.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLibraryNET.examples
{ 
    public class WorkAPIExamples
    {
        /*
        * Given a work's id, get all its information
        */
        public async Task LoadWork()
        {
            OpenLibraryClient client = new OpenLibraryClient();
            string workOLID = "";

            // Get all of the work's info and 10 of its editions
            OLWork work = await client.GetWorkAsync(workOLID, 10);

            // Equivalent
            work = new OLWork()
            {
                ID = workOLID,
                Data = await client.Work.GetDataAsync(workOLID),
                Bookshelves = await client.Work.GetBookshelvesAsync(workOLID),
                Ratings = await client.Work.GetRatingsAsync(workOLID),
                Editions = await client.Work.GetEditionsAsync(workOLID, new KeyValuePair<string, string>("limit", "10"))
            };

            // Equivalent, using static methods
            HttpClient httpClient = new HttpClient();
            work = new OLWork()
            {
                ID = workOLID,
                Data = await OLWorkLoader.GetDataAsync(httpClient, workOLID),
                Bookshelves = await OLWorkLoader.GetBookshelvesAsync(httpClient, workOLID),
                Ratings = await OLWorkLoader.GetRatingsAsync(httpClient, workOLID),
                Editions = await OLWorkLoader.GetEditionsAsync(httpClient, workOLID, new KeyValuePair<string, string>("limit", "10"))
            };
        }

        /*
         * Get a work without having a prior work OLID,
         * using the search function
         */
        public async Task GetWorkFromSearch()
        {
            HttpClient client = new HttpClient();

            // The search query
            string query = "The picture of dorian gray";

            // Request the first author returned by the search
            var results = await OLSearchLoader.GetSearchResultsAsync(client, query, new KeyValuePair<string, string>("limit", "1"));

            // Ensure there were enough search results
            if (results == null || results.Length == 0)
            {
                // Error handling code
                return;
            }

            OLWork work = new OLWork()
            {
                ID = results[0].ID,
                Data = results[0]
            };
        }
    }
}
