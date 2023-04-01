using Newtonsoft.Json;
using OpenLibrary;
using Xunit.Abstractions;
using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;
using System.IO;
using OpenLibrary.NET;

#pragma warning disable 8604, 8602
namespace Tests
{
    public class OLObjectsTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public OLObjectsTests(ITestOutputHelper helper)
        {
            _testOutputHelper = helper;
        }

        public static string[] WorksIDs = new string[]
        {
            "OL45804W",
            "OL18020194W",
            "OL17860744W",
            "OL498411W",
            "OL509183W",
            "OL262421W",
            "OL483391W",
            "OL257943W",
            "OL20090688W",
            "OL17356524W"
        };

        public static string[] EditionsIDs = new string[]
        {
            "OL35136778M",
            "OL8188748M",
            "OL28283468M",
            "OL31815823M",
            "OL15544923M",
            "OL27361392M",
            "OL32784436M",
            "OL27910148M",
            "OL26386599M",
            "OL1743891M"
        };

        public static string[] AuthorIDs = new string[]
        {
            "OL767553A",
            "OL234664A",
            "OL161167A",
            "OL33825A",
            "OL7115219A",
            "OL22012A",
            "OL34184A",
            "OL104867A",
            "OL1392722A",
            "OL23919A"
        };

        public static string[] Subjects = new string[]
        {
            "love",
            "action",
            "adventure",
            "dinosaurs",
            "computers",
            "magic",
            "fantasy",
            "music",
            "movies",
            "programming"
        };

        public static string[] CoverIDs = new string[]
        {
            "240727-S"
        };

        public static string[] AuthorPhotoIDs = new string[]
        {
            "OL229501A-S"
        };

        public static string[] SearchQueries = new string[]
        {
            "Bible",
            "Koran",
            "The Road",
            "What remains of the day",
            "The lord of the rings",
            "Giovannis room",
            "three stigmata of palmer eldritch",
            "World war 2",
            "Pale fire",
            "Lolita"
        };

        public static string[] AuthorSearchQueries = new string[]
        {
            "Mark Twain",
            "Haruki Murakami",
            "Cormac McCarthy",
            "Kobo Abe",
            "Juli Zeh",
            "Goethe",
            "Dax Flame",
            "H.P. Lovecraft",
            "Kazuo Ishiguro",
        };

        public static string[] SubjectSearchQueries = new string[]
        {
            "ships",
            "shorts",
            "mushrooms",
            "knots",
            "construction",
            "programming",
            "literature",
            "alaska",
            "sopranos",
            "iceberg"
        };

        public static string[] ListSearchQueries = new string[]
        {
            "love",
            "romance",
            "action",
            "programming",
            "boats"
        };

        [Fact]
        public async Task OLWorkTests()
        {
            foreach (var workID in WorksIDs)
            {
                OLWork work = new OLWork(workID)
                {
                    Data = await OLWorkLoader.GetDataAsync(workID),
                    Ratings = await OLWorkLoader.GetRatingsAsync(workID),
                    Bookshelves = await OLWorkLoader.GetBookshelvesAsync(workID),
                    Editions = new ReadOnlyCollection<OLEditionData>
                    (
                        await OLWorkLoader.GetEditionsAsync(workID)!
                    )
                };

                Assert.NotNull(work);
                Assert.NotNull(work.Data);
                Assert.NotNull(work.Data.Subjects);
                Assert.NotNull(work.Ratings);
                Assert.NotNull(work.Bookshelves);
                Assert.NotNull(work.Editions);

                // Assert Data has been set
                Assert.NotEqual("", work.Data.Key);
                Assert.True(work.Data.CoverKeys.Count > 0);
                Assert.True(work.Data.Subjects.Count > 0);

                _testOutputHelper.WriteLine(work.Data.CoverKeys[0].ToString());

                // Assert Ratings has been set
                Assert.NotEqual(-1, work.Ratings.Count);
                Assert.NotNull(work.Ratings.Average);

                // Assert Bookshelves has been set
                Assert.NotEqual(-1, work.Bookshelves.WantToRead);
                Assert.NotEqual(-1, work.Bookshelves.CurrentlyReading);
                Assert.NotEqual(-1, work.Bookshelves.AlreadyRead);

                // Assert equality operator
                OLWork copy = new OLWork(workID)
                {
                    Data = await OLWorkLoader.GetDataAsync(workID),
                    Ratings = await OLWorkLoader.GetRatingsAsync(workID),
                    Bookshelves = await OLWorkLoader.GetBookshelvesAsync(workID),
                    Editions = new ReadOnlyCollection<OLEditionData>
                    (
                        await OLWorkLoader.GetEditionsAsync(workID)
                    )
                };
                Assert.True(copy.Data.Subjects.Count > 0);
                Assert.Equal(work, copy);
            }
        }

        [Fact]
        public async Task OLEditionTests()
        {
            foreach (var editionID in EditionsIDs)
            {
                OLEdition edition = new OLEdition(editionID)
                {
                    Data = await OLEditionLoader.GetDataAsync(editionID)
                };

                Assert.NotNull(edition.Data);
                Assert.NotEqual("", edition.Data.Key);

                OLEdition copy = new OLEdition(editionID)
                {
                    Data = await OLEditionLoader.GetDataAsync(editionID)
                };
                Assert.Equal(edition, copy);
            }
        }

        [Fact]
        public async Task OLAuthorTests()
        {
            foreach (var authorID in AuthorIDs)
            {
                OLAuthor author = new OLAuthor(authorID)
                {
                    Data = await OLAuthorLoader.GetDataAsync(authorID),
                    Works = new ReadOnlyCollection<OLWorkData>
                    (
                        await OLAuthorLoader.GetWorksAsync(authorID)
                    )
                };

                Assert.NotNull(author.Data);
                Assert.NotNull(author.Works);
                Assert.NotNull(author.Works[0].Subjects);

                Assert.NotEqual("", author.Data.Key);

                OLAuthor copy = new OLAuthor(authorID)
                {
                    Data = await OLAuthorLoader.GetDataAsync(authorID),
                    Works = new ReadOnlyCollection<OLWorkData>
                    (
                        await OLAuthorLoader.GetWorksAsync(authorID)
                    )
                };
                Assert.NotNull(copy.Works[0].Subjects);
                Assert.Equal(author, copy);
            }
        }

        [Fact]
        public async Task OLSubjectTests()
        {
            foreach (var subjectID in Subjects)
            {
                OLSubjectData? subject = await OLSubjectLoader.GetDataAsync(subjectID);

                Assert.NotNull(subject);
                Assert.NotNull(subject.Works);

                Assert.NotEqual("", subject.Name);
                Assert.NotEqual(-1, subject.WorkCount);
                Assert.True(subject.Works.Count > 0);

                OLSubjectData? copy = await OLSubjectLoader.GetDataAsync(subjectID);
                Assert.Equal(subject, copy);
            }
        }

        [Fact]
        public async void OLSearchTests()
        {
            foreach (var query in SearchQueries)
            {
                var obj = await OLSearchLoader.GetSearchResultsAsync(query);

                Assert.NotNull(obj);
                Assert.True(obj.Length > 0);
            }

            foreach (var query in AuthorSearchQueries)
            {
                var obj = await OLSearchLoader.GetAuthorSearchResultsAsync(query);

                Assert.NotNull(obj);
                Assert.True(obj.Length > 0);
            }

            foreach (var query in SubjectSearchQueries)
            {
                var obj = await OLSearchLoader.GetSubjectSearchResultsAsync(query);

                Assert.NotNull(obj);
                Assert.True(obj.Length > 0);
            }

            foreach (var query in ListSearchQueries)
            {
                var obj = await OLSearchLoader.GetListSearchResultsAsync(query);
                Assert.NotNull(obj);
                Assert.True(obj.Length > 0);
            }
        }
    }
}
#pragma warning restore 8604, 8602