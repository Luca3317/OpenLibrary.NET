using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLibrary.NET.examples
{
    internal class MiscellaneousExamples
    {
        public async void SubjectExamples()
        {
            string subject = "horror";

            // Get subject data for this subject
            OLSubjectData? subjectData = await OLSubjectLoader.GetDataAsync(subject);

            // Get more detailled subject data for this subject
            // This data will be (as all data without fields in
            // the corresponding OL...Data record) accessible
            // through the ExtensionData property.
            OLSubjectData? subjectData2 = await OLSubjectLoader.GetDataAsync
                (
                    subject,
                    new KeyValuePair<string, string>("detail", "true")
                );

            // Get only 10 works for this subject
            // Also, only consider works that have an ebook variant
            OLSubjectData? subjectData3 = await OLSubjectLoader.GetDataAsync
                (
                    subject,
                    new KeyValuePair<string, string>("limit", "10"),
                    new KeyValuePair<string, string>("ebooks", "true")
                );

            // You can also get subjects from, for example, works, like so
            OLWork work = new OLWork("OL675783W");
            IReadOnlyList<string> workSubjects = (await work.GetDataAsync())!.Subjects;
        }

        public async void SearchExamples()
        {
            string query = "my query";

            // You can search for works,
            OLWorkData[]? workData = await OLSearchLoader.GetSearchResultsAsync(query);

            // for authors,
            OLAuthorData[]? authorData = await OLSearchLoader.GetAuthorSearchResultsAsync(query);

            // for subjects
            OLSubjectData[]? subjectData = await OLSearchLoader.GetSubjectSearchResultsAsync(query);

            // and for user created lists
            OLListData[]? listData = await OLSearchLoader.GetListSearchResultsAsync(query);


            // Search for Lolita, by Vladimir Nabokov
            // Limit the search to 3 works
            OLWorkData[]? workData2 = await OLSearchLoader.GetSearchResultsAsync
                (
                    "",
                    new KeyValuePair<string, string>("title", "lolita"),
                    new KeyValuePair<string, string>("author", "vladimir nabokov"),
                    new KeyValuePair<string, string>("limit", "3")
                );

            // Alternatively, you could search like this; they are equivalent
            OLWorkData[]? workData3 = await OLSearchLoader.GetSearchResultsAsync
                (
                    "title:lolita author:vladimir nabokov limit:3"
                );

            // Search for random authors with "Max" in their name
            // Limit the search to three, and offset the results by 5
            // (therefore results 6-8 will be returned)
            OLAuthorData[]? authorData2 = await OLSearchLoader.GetAuthorSearchResultsAsync
                (
                    "Max",
                    new KeyValuePair<string, string>("sort", "random"),
                    new KeyValuePair<string, string>("limit", "3"),
                    new KeyValuePair<string, string>("offset", "5")
                );
        }

        public async void CoverExamples()
        {
            // From an OLWork, get the covers of all editions

            OLWork work = new OLWork("OL675783W");

            foreach (OLEditionData editionData in (await work.RequestEditionsAsync(await work.GetTotalEditionCountAsync()))!)
            {
                byte[] cover = await OLImageLoader.GetCoverAsync(editionData.ID);
            }
        }
    }
}
