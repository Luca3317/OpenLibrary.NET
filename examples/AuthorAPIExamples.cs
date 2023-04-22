using OpenLibraryNET.Loader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLibraryNET.examples
{
    public class AuthorAPIExamples
    {
        /*
         * Given an author's id, get all their information.
         */
        public async Task LoadExamples()
        {
            OpenLibraryClient client = new OpenLibraryClient();
            string authorOLID = "";

            // Get all of the author's info and 10 of their works
            OLAuthor author = await client.GetAuthorAsync(authorOLID, 10);

            // Equivalent
            author = new OLAuthor()
            {
                ID = authorOLID,
                Data = await client.Author.GetDataAsync(authorOLID),
                Works = await client.Author.GetWorksAsync(authorOLID, new KeyValuePair<string, string>("limit", "10"))
            };

            // Equivalent, using static methods
            HttpClient httpClient = new HttpClient();
            author = new OLAuthor()
            {
                ID = authorOLID,
                Data = await OLAuthorLoader.GetDataAsync(httpClient, authorOLID),
                Works = await OLAuthorLoader.GetWorksAsync(httpClient, authorOLID, new KeyValuePair<string, string>("limit", "10"))
            };
        }

        /*
         * Get an author without having a prior author OLID,
         * using the search function
         */
        public async Task GetAuthorFromSearch()
        {
            HttpClient client = new HttpClient();

            // The search query
            string query = "James Joyce";

            // Request the first author returned by the search
            var results = await OLSearchLoader.GetAuthorSearchResultsAsync(client, query, new KeyValuePair<string, string>("limit", "1"));

            // Ensure there were enough search results
            if (results == null || results.Length == 0)
            {
                // Error handling code
                return;
            }

            OLAuthor author = new OLAuthor()
            {
                ID = results[0].ID,
                Data = results[0]
            };
        }
    }
}
