using OpenLibraryNET.Data;
using OpenLibraryNET.Loader;
using OpenLibraryNET.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLibraryNET.examples
{
    public class MiscellaneousExamples
    {
        HttpClient client = new HttpClient();
        OpenLibraryClient olClient = new OpenLibraryClient();

        public async Task SubjectExamples()
        {
            string subject = "World war 2";

            // Get subject data for this subject
            OLSubjectData? subjectData = await OLSubjectLoader.GetDataAsync(client, subject);

            // Get more detailed subject data for this subject
            // This data will be accessible through the ExtensionData property
            // (as is all data without fields in the corresponding OL...Data record)
            OLSubjectData? subjectData2 = await OLSubjectLoader.GetDataAsync
            (
                client,
                subject,
                new KeyValuePair<string, string>("detail", "true")
            );

            // Get only 10 works for this subject and offset by 5
            // Therefore get work 5 through 14
            OLSubjectData? subjectData3 = await OLSubjectLoader.GetDataAsync
            (
                client,
                subject,
                new KeyValuePair<string, string>("limit", "10")
            );

            // You can also get subjects from, for example, works, like so
            OLWorkData work = (await OLWorkLoader.GetDataAsync(client, "OL675783W"))!;
            IReadOnlyList<string> workSubjects = work.Subjects;
        }

        public async Task SearchExamples()
        {
            string query = "my query";

            // You can search for works,
            OLWorkData[]? workData = await OLSearchLoader.GetSearchResultsAsync(client, query);

            // for authors,
            OLAuthorData[]? authorData = await OLSearchLoader.GetAuthorSearchResultsAsync(client, query);

            // for subjects
            OLSubjectData[]? subjectData = await OLSearchLoader.GetSubjectSearchResultsAsync(client, query);

            // and for user created lists
            OLListData[]? listData = await OLSearchLoader.GetListSearchResultsAsync(client, query);


            // Search for Lolita, by Vladimir Nabokov
            OLWorkData[]? workData2 = await OLSearchLoader.GetSearchResultsAsync
            (
                client,
                "",
                new KeyValuePair<string, string>("title", "lolita"),
                new KeyValuePair<string, string>("author", "vladimir nabokov")
            );

            // Alternatively, you could search like this; they are equivalent
            OLWorkData[]? workData3 = await OLSearchLoader.GetSearchResultsAsync
            (
                client,
                "title:lolita author:vladimir nabokov"
            );

            // Search for random authors with "Max" in their name
            // Limit the search to three, and offset the results by 5
            // (therefore results 6-8 will be returned)
            OLAuthorData[]? authorData2 = await OLSearchLoader.GetAuthorSearchResultsAsync
            (
                client,
                "Max",
                new KeyValuePair<string, string>("sort", "random"),
                new KeyValuePair<string, string>("limit", "3"),
                new KeyValuePair<string, string>("offset", "5")
            );
        }

        public async Task RecentChangesExamples()
        {
            // Get recent changes
            OLRecentChangesData[]? data = await OLRecentChangesLoader.GetRecentChangesAsync(client);

            // Get changes made on January 1st 1970
            OLRecentChangesData[]? data1 = await OLRecentChangesLoader.GetRecentChangesAsync(client, "1970", "01", "01");

            // Get most recent "add-book" changes
            OLRecentChangesData[]? data2 = await OLRecentChangesLoader.GetRecentChangesAsync(client, kind: "add-book");

            // Get last 5 changes made by a bot
            KeyValuePair<string, string>[] parameters = new KeyValuePair<string, string>[]
            {
                new ("limit", "5"),
                new ("bot", "true")
            };
            OLRecentChangesData[]? data3 = await OLRecentChangesLoader.GetRecentChangesAsync(client, parameters: parameters);

            // Get (up to) 5 "merge-authors" changes made by a bot in May 2018
            OLRecentChangesData[]? data4 = await OLRecentChangesLoader.GetRecentChangesAsync
            (
                client, 
                kind: "merge-authors", 
                year: "2018", 
                month: "05", 
                parameters: parameters
            );
        }

        public async Task ListsExamples()
        {
            // Get list with id "012345" belonging to user "luca3317"
            OLListData? data = await OLListLoader.GetListAsync(client, "luca3317", "012345");

            // Get all lists belonging to user "luca3317"
            OLListData[]? data1 = await OLListLoader.GetUserListsAsync(client, "luca3317");

            // Get all editions in list "012345" by user "luca3317"
            OLEditionData[]? data2 = await OLListLoader.GetListEditionsAsync(client, "luca3317", "012345");

            // Get first 5 editions in list "012345" by user "luca3317"
            OLEditionData[]? data3 = await OLListLoader.GetListEditionsAsync
            (
                client, 
                "luca3317", 
                "012345",
                new KeyValuePair<string, string>("limit", "5")
            );


            // You can also get lists from other APIs
            string editionOLID = "";
            OLListData[]? data4 = await OLEditionLoader.GetListsAsync(client, editionOLID);

            string workOLID = "";
            OLListData[]? data5 = await OLWorkLoader.GetListsAsync(client, workOLID);

            string authorOLID = "";
            OLListData[]? data6 = await OLAuthorLoader.GetListsAsync(client, authorOLID);

            string subject = "";
            OLListData[]? data7 = await OLSubjectLoader.GetListsAsync(client, subject);
        }

        public async Task PartnerExamples()
        {
            // Get partner data by ISBN
            OLPartnerData? data = await OLPartnerLoader.GetDataAsync(client, PartnerIdType.ISBN, "0123456789");

            // Get multiple partner data records at once
            OLPartnerData[]? data1 = await OLPartnerLoader.GetMultiDataAsync(client, "isbn:0865474613", "olid:OL35738141M");

            // Equivalent
            OLPartnerData[]? data2 = await OLPartnerLoader.GetMultiDataAsync
            (
                client,
                OpenLibraryUtility.SetBibkeyPrefix(PartnerIdType.ISBN, "0865474613"),
                OpenLibraryUtility.SetBibkeyPrefix(PartnerIdType.OLID, "OL35738141M")
            );
        }

        public async Task CoverExamples()
        {
            // From a work id, get the cover of the first edition
            string workOLID = "OL675783W";

            OLEditionData data = (await OLWorkLoader.GetEditionsAsync
            (
                client,
                workOLID,
                new KeyValuePair<string, string>("limit", "1")
            ))![0];

            byte[] cover = await OLImageLoader.GetCoverAsync(client, CoverIdType.OLID, data.ID, ImageSize.Medium);
        }

        public async Task AuthorPhotoExamples()
        {
            // From an author id, get one of the author's photos
            string authorID = "OL234664A";
            byte[] authorPhoto = await OLImageLoader.GetAuthorPhotoAsync(client, AuthorPhotoIdType.OLID, authorID, ImageSize.Small);
        }
    }
}
