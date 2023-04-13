using Newtonsoft.Json;
using OpenLibraryNET;
using Xunit.Abstractions;
using OpenLibraryNET.Loader;
using OpenLibraryNET.Data;
using OpenLibraryNET.Utility;

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

        public static string[] LCCNs = new string[]
        {
            "2015042138",
            "96026698",
            "95043936",
            "2009278071",
            "90010247"
        };

        public static string[] OCLCs = new string[]
        {
            "156946426",
            "70004186",
            "825551135",
            "818345011",
            "22308330"
        };

        public static Dictionary<string, string> ListIDs = new Dictionary<string, string>()
        {
            { "anu2020", "OL189403L" },
            { "wcmem", "OL137419L" },
            { "Rondunn", "OL133394L" },
            { "Suitesez", "OL80072L" }
        };

        public static string[] Usernames = new string[]
        {
            "luca3317",
            "Reader7",
            "madcybrarian",
            "jinxiee3",
            "33engyjoy",
            "halliryh"
        };

        [Fact]
        public async Task OpenLibraryClientLoadTests()
        {
            OpenLibraryClient client = new OpenLibraryClient();

            CheckOLWorkData(await client.Work.GetDataAsync(WorksIDs[0]));
            CheckOLRatingsData(await client.Work.GetRatingsAsync(WorksIDs[0]));
            CheckOLBookshelvesData(await client.Work.GetBookshelvesAsync(WorksIDs[0]));
            CheckOLEditionData((await client.Work.GetEditionsAsync(WorksIDs[0]))[0]);

            CheckOLEditionData(await client.Edition.GetDataAsync(EditionsIDs[0]));
            CheckOLAuthorData(await client.Author.GetDataAsync(AuthorIDs[0]));
            CheckOLSubjectData(await client.Subject.GetDataAsync(Subjects[0]));
            CheckOLRecentChangesData((await client.RecentChanges.GetRecentChangesAsync())[0]);
        }

        [Fact]
        public async Task OpenLibraryClientAccountTests()
        {
            OpenLibraryClient client = new OpenLibraryClient();
            
            await client.Login("", ""); // Email and password
            await client.CreateListAsync("Test list", "Created this list as part of OpenLibrary.NET tests");
            OLListData[]? lists = await client.List.GetUserListsAsync(client.Username);
            foreach (var list in lists)
            {
                if (list.Name == "Test list")
                {
                    await client.DeleteListAsync(list.ID);
                }
            }
        }

        [Fact]
        public async Task OLRecentChangesLoaderTests()
        {
            HttpClient client = OpenLibraryUtility.GetClient();

            OLRecentChangesData[]? changesData =
                await OLRecentChangesLoader.GetRecentChangesAsync(client, parameters: new KeyValuePair<string, string>("limit", "10"));
            OLRecentChangesData[]? changesData2 =
                await OLRecentChangesLoader.GetRecentChangesAsync(client, year: "2018", parameters: new KeyValuePair<string, string>("limit", "10"));
            OLRecentChangesData[]? changesData3 =
                await OLRecentChangesLoader.GetRecentChangesAsync(client, year: "2018", month: "12", parameters: new KeyValuePair<string, string>("limit", "10"));

            Assert.NotEmpty(changesData);
            foreach (OLRecentChangesData change in changesData) CheckOLRecentChangesData(change);
            Assert.NotEmpty(changesData2);
            foreach (OLRecentChangesData change in changesData2) CheckOLRecentChangesData(change);
            Assert.NotEmpty(changesData3);
            foreach (OLRecentChangesData change in changesData3) CheckOLRecentChangesData(change);
        }

        [Fact]
        public async Task OLListLoaderTests()
        {
            HttpClient client = OpenLibraryUtility.GetClient();

            foreach (string username in Usernames)
            {
                OLListData[]? listData = await OLListLoader.GetUserListsAsync(client, username);

                Assert.NotNull(listData);
                foreach (OLListData data in listData) CheckOLListData(data);
            }

            foreach (string username in ListIDs.Keys)
            {
                OLEditionData[]? editions = await OLListLoader.GetListEditionsAsync(client, username, ListIDs[username]);
                Assert.NotEmpty(editions);
                foreach (OLEditionData data in editions) CheckOLEditionData(data);
            }

            foreach (string username in ListIDs.Keys)
            {
                OLListData? listData = await OLListLoader.GetListAsync(client, username, ListIDs[username]);
                CheckOLListData(listData);
            }
        }

        [Fact]
        public async Task OLWorkLoaderTests()
        {
            HttpClient client = OpenLibraryUtility.GetClient();

            foreach (string id in WorksIDs)
            {
                OLWorkData? data = await OLWorkLoader.GetDataAsync(client, id);
                OLRatingsData? ratings = await OLWorkLoader.GetRatingsAsync(client, id);
                OLBookshelvesData? bookshelves = await OLWorkLoader.GetBookshelvesAsync(client, id);
                OLEditionData[]? editions = await OLWorkLoader.GetEditionsAsync(client, id, new KeyValuePair<string, string>("limit", "10"));
                OLListData[]? lists = await OLWorkLoader.GetListsAsync(client, id, new KeyValuePair<string, string>("limit", "10"));

                CheckOLWorkData(data);
                CheckOLRatingsData(ratings);
                CheckOLBookshelvesData(bookshelves);
                Assert.NotEmpty(editions);
                foreach (OLEditionData edition in editions) CheckOLEditionData(edition);
                foreach (OLListData list in lists) CheckOLListData(list);
            }
        }

        [Fact]
        public async Task OLEditionLoaderOLIDTests()
        {
            HttpClient client = OpenLibraryUtility.GetClient();

            foreach (string id in EditionsIDs)
            {
                OLEditionData? data = await OLEditionLoader.GetDataByOLIDAsync(client, id);
                OLEditionData? data2 = await OLEditionLoader.GetDataByBibkeyAsync(client, id, EditionIdType.OLID);
                OLBookViewAPI? viewapi = await OLEditionLoader.GetViewAPIAsync(client, id, EditionIdType.OLID);

                CheckOLEditionData(data);
                CheckOLEditionData(data2);
                CheckOLEditionViewAPI(viewapi);
            }
        }

        [Fact]
        public async Task OLEditionLoaderISBNTests()
        {
            HttpClient client = OpenLibraryUtility.GetClient();

            foreach (string isbn in ISBNs)
            {
                _testOutputHelper.WriteLine(isbn);
                OLEditionData? data = await OLEditionLoader.GetDataByISBNAsync(client, isbn);
                OLEditionData? data2 = await OLEditionLoader.GetDataByBibkeyAsync(client, isbn, EditionIdType.ISBN);
                OLBookViewAPI? viewapi = await OLEditionLoader.GetViewAPIAsync(client, isbn, EditionIdType.ISBN);

                CheckOLEditionData(data);
                CheckOLEditionData(data2);
                CheckOLEditionViewAPI(viewapi);
            }
        }

        [Fact]
        public async Task OLEditionLoaderLCCNTests()
        {
            HttpClient client = OpenLibraryUtility.GetClient();

            foreach (string lccn in LCCNs)
            {
                OLEditionData? data = await OLEditionLoader.GetDataByBibkeyAsync(client, lccn, EditionIdType.LCCN);
                OLBookViewAPI? viewapi = await OLEditionLoader.GetViewAPIAsync(client, lccn, EditionIdType.LCCN);

                CheckOLEditionData(data);
                CheckOLEditionViewAPI(viewapi);
            }
        }

        [Fact]
        public async Task OLEditionLoaderOCLCTests()
        {
            HttpClient client = OpenLibraryUtility.GetClient();

            foreach (string oclc in OCLCs)
            {
                OLEditionData? data = await OLEditionLoader.GetDataByBibkeyAsync(client, oclc, EditionIdType.OCLC);
                OLBookViewAPI? viewapi = await OLEditionLoader.GetViewAPIAsync(client, oclc, EditionIdType.OCLC);

                CheckOLEditionData(data);
                CheckOLEditionViewAPI(viewapi);
            }
        }

        [Fact]
        public async Task OLAuthorLoaderTests()
        {
            HttpClient client = OpenLibraryUtility.GetClient();

            foreach (string id in AuthorIDs)
            {
                OLAuthorData? data = await OLAuthorLoader.GetDataAsync(client, id);
                int worksCount = await OLAuthorLoader.GetWorksCountAsync(client, id);
                OLWorkData[]? works = await OLAuthorLoader.GetWorksAsync(client, id, new KeyValuePair<string, string>("limit", "10"));
                OLListData[]? lists = await OLAuthorLoader.GetListsAsync(client, id, new KeyValuePair<string, string>("limit", "10"));

                Assert.True(worksCount > 0);
                CheckOLAuthorData(data);
                Assert.NotEmpty(works);
                foreach (OLWorkData work in works) CheckOLWorkData(work);
                foreach (OLListData list in lists) CheckOLListData(list);
            }
        }

        [Fact]
        public async Task OLSubjectLoaderTests()
        {
            HttpClient client = OpenLibraryUtility.GetClient();

            foreach (string id in Subjects)
            {
                OLSubjectData? data = await OLSubjectLoader.GetDataAsync(client, id);
                OLListData[]? lists = await OLSubjectLoader.GetListsAsync(client, id, new KeyValuePair<string, string>("limit", "10"));

                CheckOLSubjectData(data);
                _testOutputHelper.WriteLine(id);
                foreach (OLListData list in lists) CheckOLListData(list);
            }
        }

        [Fact]
        public async Task OLImageLoaderTests()
        {
            HttpClient client = OpenLibraryUtility.GetClient();

            foreach (string id in CoverIDs)
                Assert.NotEmpty(await OLImageLoader.GetCoverAsync(client, "id", id, "S"));

            foreach (string id in AuthorPhotoIDs)
                Assert.NotEmpty(await OLImageLoader.GetAuthorPhotoAsync(client, "id", id, "S"));
        }

        [Fact]
        public async Task OLSearchLoaderrTests()
        {
            HttpClient client = OpenLibraryUtility.GetClient();

            foreach (string query in SearchQueries)
            {
                OLWorkData[]? data = await OLSearchLoader.GetSearchResultsAsync(client, query);
                _testOutputHelper.WriteLine(query);
                Assert.NotEmpty(data);
                foreach (OLWorkData work in data)
                    CheckOLWorkData(work);
            }

            foreach (string query in AuthorSearchQueries)
            {
                OLAuthorData[]? data = await OLSearchLoader.GetAuthorSearchResultsAsync(client, query);
                Assert.NotEmpty(data);
                foreach (OLAuthorData author in data)
                    CheckOLAuthorData(author);
            }

            foreach (string query in SubjectSearchQueries)
            {
                OLSubjectData[]? data = await OLSearchLoader.GetSubjectSearchResultsAsync(client, query);
                Assert.NotEmpty(data);
                foreach (OLSubjectData subject in data)
                    CheckOLSubjectData(subject);
            }

            foreach (string query in ListSearchQueries)
            {
                OLListData[]? data = await OLSearchLoader.GetListSearchResultsAsync(client, query);
                Assert.NotEmpty(data);
                foreach (OLListData list in data)
                    CheckOLListData(list);
            }
        }

        [Fact]
        public async Task OLWorkTests()
        {
            HttpClient client = OpenLibraryUtility.GetClient();

            foreach (string id in WorksIDs)
            {
                OLWork work = new OLWork()
                {
                    ID = id,
                    Data = await OLWorkLoader.GetDataAsync(client, id),
                    Bookshelves = await OLWorkLoader.GetBookshelvesAsync(client, id),
                    Ratings = await OLWorkLoader.GetRatingsAsync(client, id),
                    Editions = await OLWorkLoader.GetEditionsAsync(client, id, new KeyValuePair<string, string>("limit", "10"))
                };

                CheckOLWorkData(work.Data);
                CheckOLRatingsData(work.Ratings);
                CheckOLBookshelvesData(work.Bookshelves);
                Assert.NotEmpty(work.Editions);
                foreach (OLEditionData edition in work.Editions)
                    CheckOLEditionData(edition);
            }
        }

        [Fact]
        public async Task OLAuthorTests()
        {
            HttpClient client = OpenLibraryUtility.GetClient();

            foreach (string id in AuthorIDs)
            {
                OLAuthor author = new OLAuthor()
                {
                    ID = id,
                    Data = await OLAuthorLoader.GetDataAsync(client, id),
                    Works = await OLAuthorLoader.GetWorksAsync(client, id, new KeyValuePair<string, string>("limit", "10"))
                };

                CheckOLAuthorData(author.Data);
                Assert.NotEmpty(author.Works);
                foreach (OLWorkData work in author.Works)
                    CheckOLWorkData(work);
            }
        }

        [Fact]
        public async Task OLEditionTests()
        {
            HttpClient client = OpenLibraryUtility.GetClient();

            foreach (string id in EditionsIDs)
            {
                OLEdition edition = new OLEdition()
                {
                    ID = id,
                    Data = await OLEditionLoader.GetDataAsync(client, id),
                    CoverS = await OLImageLoader.GetCoverAsync(client, "OLID", id, "s")
                };
                CheckOLEditionData(edition.Data);
                Assert.NotEmpty(edition.CoverS);
            }
        }

        
        [Fact]
        public async Task OLWorkDataSerializationTest()
        {
            HttpClient client = OpenLibraryUtility.GetClient();

            foreach (string id in WorksIDs)
            {
                OLWorkData? data = await OLWorkLoader.GetDataAsync(client, id);
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
            HttpClient client = OpenLibraryUtility.GetClient();

            foreach (string id in EditionsIDs)
            {
                OLEditionData? data = await OLEditionLoader.GetDataByOLIDAsync(client, id);
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
            HttpClient client = OpenLibraryUtility.GetClient();

            foreach (string id in AuthorIDs)
            {
                OLAuthorData? data = await OLAuthorLoader.GetDataAsync(client, id);
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
            HttpClient client = OpenLibraryUtility.GetClient();

            foreach (string id in WorksIDs)
            {
                OLRatingsData? data = await OLWorkLoader.GetRatingsAsync(client, id);
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
            HttpClient client = OpenLibraryUtility.GetClient();

            foreach (string id in WorksIDs)
            {
                OLBookshelvesData? data = await OLWorkLoader.GetBookshelvesAsync(client, id);
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
            HttpClient client = OpenLibraryUtility.GetClient();

            foreach (string id in Subjects)
            {
                OLSubjectData? data = await OLSubjectLoader.GetDataAsync(client, id);
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
            HttpClient client = OpenLibraryUtility.GetClient();

            foreach (string id in WorksIDs)
            {
                OLWork work = new OLWork()
                {
                    ID = id,
                    Data = await OLWorkLoader.GetDataAsync(client, id),
                    Bookshelves = await OLWorkLoader.GetBookshelvesAsync(client, id),
                    Ratings = await OLWorkLoader.GetRatingsAsync(client, id),
                    Editions = await OLWorkLoader.GetEditionsAsync(client, id, new KeyValuePair<string, string>("limit", "10"))
                };

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
            HttpClient client = OpenLibraryUtility.GetClient();

            foreach (string id in AuthorIDs)
            {
                OLAuthor author = new OLAuthor()
                {
                    ID = id,
                    Data = await OLAuthorLoader.GetDataAsync(client, id),
                    Works = await OLAuthorLoader.GetWorksAsync(client, id, new KeyValuePair<string, string>("limit", "10"))
                };

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
            HttpClient client = OpenLibraryUtility.GetClient();

            foreach (string id in EditionsIDs)
            {
                OLEdition edition = new OLEdition()
                {
                    ID = id,
                    Data = await OLEditionLoader.GetDataAsync(client, id),
                    CoverS = await OLImageLoader.GetCoverAsync(client, "OLID", id, "s")
                };

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
            //Assert.NotEmpty(data.WorkKeys); Not in api/books/ data
        }

        void CheckOLEditionViewAPI(OLBookViewAPI? viewapi)
        {
            Assert.NotNull(viewapi);
            Assert.NotEqual("", viewapi.Preview);
            Assert.NotEqual("", viewapi.Bibkey);
            Assert.NotEqual("", viewapi.InfoURL);
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
            //Assert.NotEqual("", data.URL);
            //Assert.NotEqual("", data.LastUpdate);
        }

        void CheckOLRecentChangesData(OLRecentChangesData? data)
        {
            Assert.NotNull(data);
            Assert.NotEqual("", data.ID);
            Assert.NotEqual("", data.Timestamp);
            Assert.NotEqual("", data.Author);
            Assert.NotEqual("", data.IP);
            Assert.NotEqual("", data.Kind);
            Assert.NotEmpty(data.Changes);
            foreach (var change in data.Changes)
            {
                Assert.NotNull(change);
                Assert.NotEqual(-1, change.Revision);
                Assert.NotEqual("", change.Key);
            }
        }
    }
}
#pragma warning restore 8604, 8602