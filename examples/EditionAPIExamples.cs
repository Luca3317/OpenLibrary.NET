using OpenLibraryNET.Loader;
using OpenLibraryNET.Data;
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
        * Given an edition's id, get all its information
        */
        public async Task LoadEdition()
        {
            OpenLibraryClient client = new OpenLibraryClient();
            string editionOLID = "";

            // Get the edition's data, as well as its cover in small resolution
            OLEdition edition = await client.GetEditionAsync(editionOLID, EditionIdType.OLID, ImageSize.Small);

            // Equivalent
            edition = new OLEdition()
            {
                ID = editionOLID,
                Data = await client.Edition.GetDataAsync(editionOLID),
                CoverS = await client.Image.GetCoverAsync(CoverIdType.OLID, editionOLID, ImageSize.Small)
            };

            // Equivalent, using static methods
            HttpClient httpClient = new HttpClient();
            edition = new OLEdition()
            {
                ID = editionOLID,
                Data = await OLEditionLoader.GetDataAsync(httpClient, editionOLID),
                CoverS = await OLImageLoader.GetCoverAsync(httpClient, CoverIdType.OLID, editionOLID, ImageSize.Small),
            };

            // Alternatives
            // Get an edition by ISBN
            edition = await client.GetEditionAsync("0123456789", EditionIdType.ISBN, ImageSize.Small);
            // Get an edition by OCLC
            edition = await client.GetEditionAsync("30647889", EditionIdType.OCLC, ImageSize.Small);
            // Get an edition using a prepared bibkey; no cover
            edition = await client.GetEditionAsync("ISBN:0123456789");
        }

        /*
         * Get an edition without having a prior edition OLID,
         * using the search function
         */
        public async Task GetEditionFromSearch()
        {
            HttpClient client = new HttpClient();

            // The search query
            string query = "the lord of the rings two towers";

            // Request the first author returned by the search
            var results = await OLSearchLoader.GetSearchResultsAsync(client, query, new KeyValuePair<string, string>("limit", "1"));

            // Ensure there were enough search results
            if (results == null || results.Length == 0)
            {
                // Error handling code
                return;
            }

            OLEditionData editionData = (await OLWorkLoader.GetEditionsAsync(client, results[0].ID))![0];

            OLEdition edition = new OLEdition()
            {
                ID = editionData.ID,
                Data = editionData
            };


            // Alternatively, you can try to get the edition ID from OLWorkData's extensiondata
            string editionID = results[0].ExtensionData["cover_edition_key"].ToObject<string>();
            OLEdition editionAlt = new OLEdition()
            {
                ID = editionID,
                Data = await OLEditionLoader.GetDataAsync(client, editionID, EditionIdType.OLID)
            };
        }
    }
}
