using Newtonsoft.Json;
using OpenLibraryNET;
using Xunit.Abstractions;
using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;

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
            "240727"
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

        public static string[] ISBNs = new string[]
        {
            "1784878006",
            "1784877980",
            "1846276845"
        };

        [Fact]
        public async Task Tesst()
        {
            OLWork work = new OLWork("OL45804W");
            await work.GetBookshelvesAsync();
            await work.GetRatingsAsync();
            await work.GetDataAsync();
            await work.GetTotalEditionCountAsync();
            await work.RequestEditionsAsync(1);

            _testOutputHelper.WriteLine(work.Data.AuthorKeys[0]);
            string aid = OpenLibraryUtility.ExtractIdFromKey(work.Data.AuthorKeys[0]);
            _testOutputHelper.WriteLine(aid);

            _testOutputHelper.WriteLine
            (
                "Title: " + work.Data.Title +
                "\nDescription: " + work.Data.Description +
                "\nAuthor: " + (await OLAuthorLoader.GetDataAsync(aid))!.Name +
                "\nRatings: " + work.Ratings.Count +
                "\nAverage: " + work.Ratings.Average +
                "\nRead by: " + work.Bookshelves.AlreadyRead + " people"
            );

            OLAuthor author = new OLAuthor(aid);
            await author.GetDataAsync();
            await author.GetTotalWorksCountAsync();

            await OLImageLoader.GetCoverAsync("id", work.Editions[0].CoverKeys[0].ToString(), "s");
            await OLImageLoader.GetAuthorPhotoAsync("id", author.Data.PhotosIDs[0].ToString(), "s");
        }

        [Fact]
        public async Task ExamplesTest()
        {
            // Work examples
            await new OpenLibraryNET.examples.WorkAPIExamples().GetWork();
            await new OpenLibraryNET.examples.WorkAPIExamples().GetWorkFromSearch();
            await new OpenLibraryNET.examples.WorkAPIExamples().GetRatings();

            // Editon examples
            await new OpenLibraryNET.examples.EditionAPIExamples().GetEditionFromSearch();
            await new OpenLibraryNET.examples.EditionAPIExamples().GetEdition();
            await new OpenLibraryNET.examples.EditionAPIExamples().GetCover();

            // Author examples
            await new OpenLibraryNET.examples.AuthorAPIExamples().GetAuthorFromSearch();
            await new OpenLibraryNET.examples.AuthorAPIExamples().GetAuthor();
            await new OpenLibraryNET.examples.AuthorAPIExamples().GeAuthorData();

            // Miscellaneous examples
            await new OpenLibraryNET.examples.MiscellaneousExamples().CoverExamples();
            await new OpenLibraryNET.examples.MiscellaneousExamples().SubjectExamples();
            await new OpenLibraryNET.examples.MiscellaneousExamples().SearchExamples();
        }

        [Fact]
        public async Task OLWorkLoaderTests()
        {
            foreach (string id in WorksIDs)
            {
                OLWorkData? data = await OLWorkLoader.GetDataAsync(id);
                OLRatingsData? ratings = await OLWorkLoader.GetRatingsAsync(id);
                OLBookshelvesData? bookshelves = await OLWorkLoader.GetBookshelvesAsync(id);
                OLEditionData[]? editions = await OLWorkLoader.GetEditionsAsync(id, new KeyValuePair<string, string>("limit", "10"));

                CheckOLWorkData(data);
                CheckOLRatingsData(ratings);
                CheckOLBookshelvesData(bookshelves);
                Assert.NotEmpty(editions);
                foreach (OLEditionData edition in editions) CheckOLEditionData(edition);
            }
        }

        [Fact]
        public async Task OLEditionLoaderTests()
        {
            foreach (string id in EditionsIDs)
            {
                OLEditionData? data = await OLEditionLoader.GetDataByOLIDAsync(id);
                CheckOLEditionData(data);
            }

            foreach (string isbn in ISBNs)
            {
                OLEditionData? data = await OLEditionLoader.GetDataByISBNAsync(isbn);
                CheckOLEditionData(data);
            }
        }

        [Fact]
        public async Task OLAuthorLoaderTests()
        {
            foreach (string id in AuthorIDs)
            {
                OLAuthorData? data = await OLAuthorLoader.GetDataAsync(id);
                int worksCount = await OLAuthorLoader.GetWorksCountAsync(id);
                OLWorkData[]? works = await OLAuthorLoader.GetWorksAsync(id, new KeyValuePair<string, string>("limit", "10"));

                Assert.True(worksCount > 0);
                CheckOLAuthorData(data);
                Assert.NotEmpty(works);
                foreach (OLWorkData work in works) CheckOLWorkData(work);
            }
        }

        [Fact]
        public async Task OLSubjectLoaderTests()
        {
            foreach (string id in Subjects)
            {
                OLSubjectData? data = await OLSubjectLoader.GetDataAsync(id);
                CheckOLSubjectData(data);
            }
        }

        [Fact]
        public async Task OLImageLoaderTests()
        {
            foreach (string id in CoverIDs)
                Assert.NotEmpty(await OLImageLoader.GetCoverAsync("id", id, "S"));

            foreach (string id in AuthorPhotoIDs)
                Assert.NotEmpty(await OLImageLoader.GetAuthorPhotoAsync("id", id, "S"));
        }

        [Fact]
        public async Task OLSearchLoaderrTests()
        {
            foreach (string query in SearchQueries)
            {
                OLWorkData[]? data = await OLSearchLoader.GetSearchResultsAsync(query);
                _testOutputHelper.WriteLine(query);
                Assert.NotEmpty(data);
                foreach (OLWorkData work in data)
                    CheckOLWorkData(work);
            }

            foreach (string query in AuthorSearchQueries)
            {
                OLAuthorData[]? data = await OLSearchLoader.GetAuthorSearchResultsAsync(query);
                Assert.NotEmpty(data);
                foreach (OLAuthorData author in data)
                    CheckOLAuthorData(author);
            }

            foreach (string query in SubjectSearchQueries)
            {
                OLSubjectData[]? data = await OLSearchLoader.GetSubjectSearchResultsAsync(query);
                Assert.NotEmpty(data);
                foreach (OLSubjectData subject in data)
                    CheckOLSubjectData(subject);
            }

            foreach (string query in ListSearchQueries)
            {
                OLListData[]? data = await OLSearchLoader.GetListSearchResultsAsync(query);
                Assert.NotEmpty(data);
                foreach (OLListData list in data)
                    CheckOLListData(list);
            }
        }

        [Fact]
        public async Task OLWorkTests()
        {
            foreach (string id in WorksIDs)
            {
                OLWork work = new OLWork(id);
                CheckOLWorkData(await work.GetDataAsync());
                CheckOLRatingsData(await work.GetRatingsAsync());
                CheckOLBookshelvesData(await work.GetBookshelvesAsync());
                CheckOLRatingsData(await work.GetRatingsAsync());
                var editions = await work.RequestEditionsAsync(10);
                Assert.NotEmpty(editions);
                foreach (OLEditionData edition in editions)
                    CheckOLEditionData(edition);
            }
        }

        [Fact]
        public async Task OLAuthorTests()
        {
            foreach (string id in AuthorIDs)
            {
                OLAuthor author = new OLAuthor(id);
                CheckOLAuthorData(await author.GetDataAsync());
                var works = author.RequestWorksAsync(10);
                foreach (OLWorkData work in await works)
                    CheckOLWorkData(work);
            }
        }

        [Fact]
        public async Task OLEditionTests()
        {
            foreach (string id in EditionsIDs)
            {
                OLEdition edition = new OLEdition(id);
                CheckOLEditionData(await edition.GetDataAsync());
                Assert.NotNull(edition.GetCoverAsync("s"));
            }
        }

        [Fact]
        public async Task OLWorkDataSerializationTest()
        {
            foreach (string id in WorksIDs)
            {
                OLWorkData? data = await OLWorkLoader.GetDataAsync(id);
                string serialized = JsonConvert.SerializeObject(data);
                var deserialized = JsonConvert.DeserializeObject<OLWorkData>(serialized);
                string reserialized = JsonConvert.SerializeObject(deserialized);
                Assert.Equal(data, deserialized);
                Assert.Equal(serialized, reserialized);
            }
        }

        [Fact]
        public async Task OLEditionDataSerializationTest()
        {
            foreach (string id in EditionsIDs)
            {
                OLEditionData? data = await OLEditionLoader.GetDataByOLIDAsync(id);
                string serialized = JsonConvert.SerializeObject(data);
                var deserialized = JsonConvert.DeserializeObject<OLEditionData>(serialized);
                string reserialized = JsonConvert.SerializeObject(deserialized);
                Assert.Equal(data, deserialized);
                Assert.Equal(serialized, reserialized);
            }
        }

        [Fact]
        public async Task OLAuthorDataSerializationTest()
        {
            foreach (string id in AuthorIDs)
            {
                OLAuthorData? data = await OLAuthorLoader.GetDataAsync(id);
                string serialized = JsonConvert.SerializeObject(data);
                var deserialized = JsonConvert.DeserializeObject<OLAuthorData>(serialized);
                string reserialized = JsonConvert.SerializeObject(deserialized);
                Assert.Equal(data, deserialized);
                Assert.Equal(serialized, reserialized);
            }
        }

        [Fact]
        public async Task OLRatingsDataSerializationTest()
        {
            foreach (string id in WorksIDs)
            {
                OLRatingsData? data = await OLWorkLoader.GetRatingsAsync(id);
                string serialized = JsonConvert.SerializeObject(data);
                var deserialized = JsonConvert.DeserializeObject<OLRatingsData>(serialized);
                string reserialized = JsonConvert.SerializeObject(deserialized);
                Assert.Equal(data, deserialized);
                Assert.Equal(serialized, reserialized);
            }
        }

        [Fact]
        public async Task OLBookshelvesDataSerializationTest()
        {
            foreach (string id in WorksIDs)
            {
                OLBookshelvesData? data = await OLWorkLoader.GetBookshelvesAsync(id);
                string serialized = JsonConvert.SerializeObject(data);
                var deserialized = JsonConvert.DeserializeObject<OLBookshelvesData>(serialized);
                string reserialized = JsonConvert.SerializeObject(deserialized);
                Assert.Equal(data, deserialized);
                Assert.Equal(serialized, reserialized);
            }
        }

        [Fact]
        public async Task OLSubjectDataSerializationTest()
        {
            foreach (string id in Subjects)
            {
                OLSubjectData? data = await OLSubjectLoader.GetDataAsync(id);
                string serialized = JsonConvert.SerializeObject(data);
                var deserialized = JsonConvert.DeserializeObject<OLSubjectData>(serialized);
                string reserialized = JsonConvert.SerializeObject(deserialized);
                Assert.Equal(data, deserialized);
                Assert.Equal(serialized, reserialized);
            }
        }

        [Fact]
        public async Task OLWorkSerializationTest()
        {
            foreach (string id in WorksIDs)
            {
                OLWork work = new OLWork(id);
                await work.GetDataAsync();
                await work.GetRatingsAsync();
                await work.GetBookshelvesAsync();
                await work.GetTotalEditionCountAsync();
                await work.RequestEditionsAsync(10);
                string serialized = JsonConvert.SerializeObject(work);
                var deserialized = JsonConvert.DeserializeObject<OLWork>(serialized);
                string reserialized = JsonConvert.SerializeObject(deserialized);
                Assert.Equal(work, deserialized);
                Assert.Equal(serialized, reserialized);
            }
        }

        [Fact]
        public async Task OLAuthorSerializationTest()
        {
            foreach (string id in AuthorIDs)
            {
                OLAuthor author = new OLAuthor(id);
                await author.GetDataAsync();
                await author.GetTotalWorksCountAsync();
                await author.RequestWorksAsync(10);
                string serialized = JsonConvert.SerializeObject(author);
                var deserialized = JsonConvert.DeserializeObject<OLAuthor>(serialized);
                string reserialized = JsonConvert.SerializeObject(deserialized);
                Assert.Equal(author, deserialized);
                Assert.Equal(serialized, reserialized);
            }
        }

        [Fact]
        public async Task OLEditionSerializationTest()
        {
            foreach (string id in EditionsIDs)
            {
                OLEdition edition = new OLEdition(id);
                await edition.GetDataAsync();
                await edition.GetCoverAsync("s");
                string serialized = JsonConvert.SerializeObject(edition);
                var deserialized = JsonConvert.DeserializeObject<OLEdition>(serialized);
                string reserialized = JsonConvert.SerializeObject(deserialized);
                Assert.Equal(edition, deserialized);
                Assert.Equal(serialized, reserialized);
            }
        }

        void CheckOLWorkData(OLWorkData? data)
        {
            Assert.NotNull(data);
            Assert.NotEqual("", data.Key);
            Assert.NotEqual("", data.Title);
            foreach (string author in data.AuthorKeys)
                Assert.Matches("/authors/OL[0-9]*A", author);
        }

        void CheckOLRatingsData(OLRatingsData? data)
        {
            Assert.NotNull(data);
            Assert.NotEqual(-1, data.Count);
            if (data.Count != 0) Assert.NotNull(data.Average);
            else Assert.Null(data.Average);
        }

        void CheckOLBookshelvesData(OLBookshelvesData? data)
        {
            Assert.NotNull(data);
            Assert.NotEqual(-1, data.WantToRead);
            Assert.NotEqual(-1, data.CurrentlyReading);
            Assert.NotEqual(-1, data.AlreadyRead);
        }

        void CheckOLEditionData(OLEditionData? data)
        {
            Assert.NotNull(data);
            Assert.NotEqual("", data.Key);
            Assert.NotEqual("", data.Title);
            Assert.NotEmpty(data.WorkKeys);
        }

        void CheckOLAuthorData(OLAuthorData? data)
        {
            Assert.NotNull(data);
            Assert.NotEqual("", data.Name);
            Assert.NotEqual("", data.Key);
        }

        void CheckOLSubjectData(OLSubjectData? data)
        {
            Assert.NotNull(data);
            Assert.NotEqual("", data.Name);
            Assert.NotEqual(-1, data.WorkCount);
        }

        void CheckOLListData(OLListData? data)
        {
            Assert.NotNull(data);
            Assert.NotEqual("", data.Name);
            Assert.NotEqual("", data.URL);
            Assert.NotEqual("", data.LastUpdate);
        }
    }
}
#pragma warning restore 8604, 8602