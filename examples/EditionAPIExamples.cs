using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLibraryNET.examples
{
    public class EditionAPIExamples
    {
        /*
        * Given an edition's OLID, get all its information
        */
        public async Task GetEdition()
        {
            // The edition's OLID
            string id = "OL7826547M";

            // Create editon and load all its information
            // These values, once loaded, are stored in edition
            OLEdition edition = new OLEdition(id);
            await edition.GetDataAsync();
            await edition.GetCoverAsync("s"); // get cover in small resolution
            await edition.GetCoverAsync("m"); // get cover in medium resolution
            await edition.GetCoverAsync("l"); // get cover in large resolution
        }

        /*
         * Get an edition without having a prior edition OLID,
         * using the search function
         */
        public async Task GetEditionFromSearch()
        {
            // The search query
            string query = "Finnegans Wake";

            // Request the first work returned by the search
            OLWorkData[]? results =
                await OLSearchLoader.GetSearchResultsAsync(query, new KeyValuePair<string, string>("limit", "1"));
            
            // Ensure there were enough search results
            if (results == null || results.Length == 0)
            {
                // Error handling code
                return;
            }

            // Get the first of the works editions
            OLEditionData[]? editions = 
                await OLWorkLoader.GetEditionsAsync(results[0].ID, new KeyValuePair<string, string>("limit", "1"));
           
            // Ensure the work has enough editions
            if (editions == null || editions.Length == 0)
            {
                // Error handling code
                return;
            }

            OLEdition edition = new OLEdition(editions[0]);
        }

        /*
         * Given an editions's OLID, get only its cover
         */
        public async Task GetCover()
        {
            // The editions OLID
            string id = "OL31827A";

            byte[] cover = await OLImageLoader.GetCoverAsync("olid", id, "s"); // get cover in small resolution
        }
    }
}
