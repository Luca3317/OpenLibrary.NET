using Newtonsoft.Json;
using OpenLibraryNET;
using Xunit.Abstractions;
using OpenLibraryNET.Loader;
using OpenLibraryNET.Data;
using OpenLibraryNET.Utility;
using static OpenLibraryNET.Utility.OpenLibraryUtility;
using System.Text;
using Polly;
using System.Security.Cryptography;
using OpenLibraryNET.OLData;

#pragma warning disable 8604, 8602
namespace Tests
{
    public class OpenLibraryTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        private const string AccountEmail = "";
        private const string AccountPassword = "";

        public OpenLibraryTests(ITestOutputHelper helper)
        {
            _testOutputHelper = helper;
        }

        #region Constants
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
            "OL229501A"
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
        #endregion

        #region OLLoader Tests
        [Fact]
        [Trait("Category", "OLLoader")]
        public async Task OLPartnerLoaderTests()
        {
            string id1 = "id:1;lccn:50006784";
            string id2 = "olid:OL6179000M;lccn:55011330";

            OpenLibraryClient client = new OpenLibraryClient();
            var s = await client.Partner.GetMultiDataAsync(id1, id2);

            Assert.NotEmpty(s);
            foreach (var item in s)
            {
                Assert.NotNull(item.Details);
                Assert.NotNull(item.Data);
                Assert.NotNull(item.Items);
            }
        }

        [Fact]
        [Trait("Category", "OLLoader")]
        public async Task OLRecentChangesLoaderTests()
        {
            HttpClient client = new HttpClient();

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
        [Trait("Category", "OLLoader")]
        public async Task OLListLoaderTests()
        {
            HttpClient client = new HttpClient();

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

            foreach (string username in ListIDs.Keys)
            {
                OLSeedData[]? seedDatas = await OLListLoader.GetListSeedsAsync(client, username, ListIDs[username]);
                Assert.NotEmpty(seedDatas);
                foreach (OLSeedData data in seedDatas) CheckOLSeedData(data);
            }

            foreach (string username in ListIDs.Keys)
            {
                OLSubjectData[]? subjectDatas = await OLListLoader.GetListSubjectsAsync(client, username, ListIDs[username]);
                if (subjectDatas.Length == 0) _testOutputHelper.WriteLine("Empty for list " +  username + "/" + ListIDs[username]);
                foreach (OLSubjectData data in subjectDatas) CheckOLSubjectData(data);
            }
        }

        [Fact]
        [Trait("Category", "OLLoader")]
        public async Task OLWorkLoaderTests()
        {
            var policy = Policy
                .Handle<Exception>()
                .OrResult<object?>(o => o == null)
                .RetryAsync(3, (ex, retryCnt) =>
                {
                    _testOutputHelper.WriteLine($"Retry count {retryCnt}");
                });

            HttpClient client = new HttpClient();

            foreach (string id in WorksIDs)
            {
                OLWorkData? data = (OLWorkData?)await policy.ExecuteAsync(async () => await OLWorkLoader.GetDataAsync(client, id));
                OLRatingsData? ratings = (OLRatingsData?)await policy.ExecuteAsync(async () => await OLWorkLoader.GetRatingsAsync(client, id));
                OLBookshelvesData? bookshelves = (OLBookshelvesData?)await policy.ExecuteAsync(async () => await OLWorkLoader.GetBookshelvesAsync(client, id));
                OLEditionData[]? editions = (OLEditionData[]?)await policy.ExecuteAsync(async () => await OLWorkLoader.GetEditionsAsync(client, id, new KeyValuePair<string, string>("limit", "10")));
                OLListData[]? lists = (OLListData[]?)await policy.ExecuteAsync(async () => await OLWorkLoader.GetListsAsync(client, id, new KeyValuePair<string, string>("limit", "10")));

                CheckOLWorkData(data);
                CheckOLRatingsData(ratings);
                CheckOLBookshelvesData(bookshelves);
                Assert.NotEmpty(editions);
                foreach (OLEditionData edition in editions) CheckOLEditionData(edition);
                foreach (OLListData list in lists) CheckOLListData(list);
            }
        }

        [Fact]
        [Trait("Category", "OLLoader")]
        public async Task OLEditionLoaderOLIDTests()
        {
            HttpClient client = new HttpClient();

            foreach (string id in EditionsIDs)
            {
                OLEditionData? data = await OLEditionLoader.GetDataByOLIDAsync(client, id);
                OLEditionData? data2 = await OLEditionLoader.GetDataByBibkeyAsync(client, id, BookIdType.OLID);
                OLBookViewAPI? viewapi = await OLEditionLoader.GetViewAPIAsync(client, id, BookIdType.OLID);

                CheckOLEditionData(data);
                CheckOLEditionData(data2);
                CheckOLEditionViewAPI(viewapi);
            }
        }

        [Fact]
        [Trait("Category", "OLLoader")]
        public async Task OLEditionLoaderISBNTests()
        {
            HttpClient client = new HttpClient();

            foreach (string isbn in ISBNs)
            {
                _testOutputHelper.WriteLine(isbn);
                OLEditionData? data = await OLEditionLoader.GetDataByISBNAsync(client, isbn);
                OLEditionData? data2 = await OLEditionLoader.GetDataByBibkeyAsync(client, isbn, BookIdType.ISBN);
                OLBookViewAPI? viewapi = await OLEditionLoader.GetViewAPIAsync(client, isbn, BookIdType.ISBN);

                CheckOLEditionData(data);
                CheckOLEditionData(data2);
                CheckOLEditionViewAPI(viewapi);
            }
        }

        [Fact]
        [Trait("Category", "OLLoader")]
        public async Task OLEditionLoaderBibkeyTests()
        {
            HttpClient client = new HttpClient();

            foreach (string lccn in LCCNs)
            {
                OLEditionData? data = await OLEditionLoader.GetDataByBibkeyAsync(client, lccn, BookIdType.LCCN);
                OLBookViewAPI? viewapi = await OLEditionLoader.GetViewAPIAsync(client, lccn, BookIdType.LCCN);

                CheckOLEditionData(data);
                CheckOLEditionViewAPI(viewapi);
            }

            foreach (string oclc in OCLCs)
            {
                OLEditionData? data = await OLEditionLoader.GetDataByBibkeyAsync(client, oclc, BookIdType.OCLC);
                OLBookViewAPI? viewapi = await OLEditionLoader.GetViewAPIAsync(client, oclc, BookIdType.OCLC);

                CheckOLEditionData(data);
                CheckOLEditionViewAPI(viewapi);
            }

            List<string> bibkeys = new List<string>();
            foreach (string lccn in LCCNs) bibkeys.Add("LCCN:" + lccn);
            OLEditionData[]? editions = await OLEditionLoader.GetDataByBibkeyAsync(client, bibkeys.ToArray());
            OLBookViewAPI[]? viewapis = await OLEditionLoader.GetViewAPIAsync(client, bibkeys.ToArray());

            bibkeys.Clear();
            foreach (string oclc in OCLCs) bibkeys.Add("OCLC:" + oclc);
            OLEditionData[]? editions2 = await OLEditionLoader.GetDataByBibkeyAsync(client, bibkeys.ToArray());
            OLBookViewAPI[]? viewapis2 = await OLEditionLoader.GetViewAPIAsync(client, bibkeys.ToArray());

            Assert.NotEmpty(editions);
            Assert.NotEmpty(viewapis);
            Assert.NotEmpty(editions2);
            Assert.NotEmpty(viewapis2);
            foreach (var item in editions) CheckOLEditionData(item);
            foreach (var item in viewapis) CheckOLEditionViewAPI(item);
            foreach (var item in editions2) CheckOLEditionData(item);
            foreach (var item in viewapis2) CheckOLEditionViewAPI(item);
        }

        [Fact]
        [Trait("Category", "OLLoader")]
        public async Task OLAuthorLoaderTests()
        {
            HttpClient client = new HttpClient();

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
        [Trait("Category", "OLLoader")]
        public async Task OLSubjectLoaderTests()
        {
            HttpClient client = new HttpClient();

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
        [Trait("Category", "OLLoader")]
        public async Task OLImageLoaderTests()
        {
            HttpClient client = new HttpClient();

            foreach (string id in CoverIDs)
            {
                Assert.NotEmpty(await OLImageLoader.GetCoverAsync(client, "id", id, "S"));
            }

            foreach (string id in AuthorPhotoIDs)
                Assert.NotEmpty(await OLImageLoader.GetAuthorPhotoAsync(client, "olid", id, "S"));
        }

        [Fact]
        [Trait("Category", "OLLoader")]
        public async Task OLSearchLoaderrTests()
        {
            HttpClient client = new HttpClient();

            foreach (string query in SearchQueries)
            {
                OLWorkData[]? data = await OLSearchLoader.GetSearchResultsAsync(client, query);
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

            OLContainer? container = await OLSearchLoader.GetInsideSearchResultsAsync(client, "Hello");
            Assert.NotEmpty(container.ExtensionData);
        }

        [Fact]
        [Trait("Category", "OLLoader")]
        public async Task OLMyBooksLoaderTests()
        {
            HttpClient client = new HttpClient();

            OLMyBooksData? data = await OLMyBooksLoader.GetAlreadyReadAsync(client, "luca3317");
            CheckOLMyBooksData(data);

            OLMyBooksData? data1 = await OLMyBooksLoader.GetWantToReadAsync(client, "luca3317");
            CheckOLMyBooksData(data1);

            OLMyBooksData? data2 = await OLMyBooksLoader.GetCurrentlyReadingAsync(client, "luca3317");
            CheckOLMyBooksData(data2);
        }
        #endregion

        #region OpenLibraryClient Tests
        [Fact]
        [Trait("Category", "OpenLibaryClient")]
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

            var workO = await client.GetWorkAsync(WorksIDs[0], 10);
            var authorO = await client.GetAuthorAsync(AuthorIDs[0], 10);
            //var editionO = await client.GetEditionAsync(EditionsIDs[0], BookIdType.OLID, "S");
            var editionO = await client.GetEditionAsync(EditionsIDs[0], "OLID", "S");
            var editionO2 = await client.GetEditionAsync(EditionsIDs[0], BookIdType.OLID, ImageSize.Small);
            Assert.Equal(editionO, editionO2);

            CheckOLWorkData(workO.Data);
            CheckOLBookshelvesData(workO.Bookshelves);
            CheckOLRatingsData(workO.Ratings);
            foreach (var edition in workO.Editions) CheckOLEditionData(edition);

            CheckOLAuthorData(authorO.Data);
            foreach (var work in authorO.Works) CheckOLWorkData(work);

            CheckOLEditionData(editionO.Data);
            Assert.NotEmpty(editionO.CoverS);
        }

        [Fact]
        [Trait("Category", "OpenLibaryClient")]
        public async Task OpenLibraryClientAccountTests()
        {
            OpenLibraryClient client = new OpenLibraryClient();

            await client.LoginAsync(AccountEmail, AccountPassword);
            await client.CreateListAsync("Test list", "Created this list as part of OpenLibrary.NET tests");
            OLListData[]? lists = await client.List.GetUserListsAsync(client.Username);
            foreach (var list in lists)
            {
                if (list.Name == "Test list")
                {
                    await client.DeleteListAsync(list.ID);
                }
            }

            await client.LogoutAsync();
            await Assert.ThrowsAnyAsync<System.Exception>(async () => await client.CreateListAsync("This list should not be created"));
        }

        [Fact]
        [Trait("Category", "OpenLibaryClient")]
        public async Task OLWorkTests()
        {
            HttpClient client = new HttpClient();

            foreach (string id in WorksIDs)
            {
                var data = await OLWorkLoader.GetDataAsync(client, id);
                var bookshleves = await OLWorkLoader.GetBookshelvesAsync(client, id);
                var ratings = await OLWorkLoader.GetRatingsAsync(client, id);
                var editions = await OLWorkLoader.GetEditionsAsync(client, id, new KeyValuePair<string, string>("limit", "10"));
                OLWork work = new OLWork()
                {
                    ID = id,
                    Data = data,
                    Bookshelves = bookshleves,
                    Ratings = ratings,
                    Editions = editions
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
        [Trait("Category", "OpenLibaryClient")]
        public async Task OLAuthorTests()
        {
            HttpClient client = new HttpClient();

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
        [Trait("Category", "OpenLibaryClient")]
        public async Task OLEditionTests()
        {
            HttpClient client = new HttpClient();

            foreach (string id in EditionsIDs)
            {
                OLEdition edition = new OLEdition()
                {
                    ID = id,
                    Data = await OLEditionLoader.GetDataAsync(client, id),
                    CoverS = await OLImageLoader.GetCoverAsync(client, "OLID", id, "S")
                };
                CheckOLEditionData(edition.Data);
                Assert.NotEmpty(edition.CoverS);
            }
        }

        [Fact]
        [Trait("Category", "OpenLibraryClient")]
        public async Task OLMyBooksTests()
        {
            OpenLibraryClient client = new OpenLibraryClient();

            bool logged = await client.TryLoginAsync(AccountEmail, AccountPassword);

            Assert.True(logged);

            CheckOLMyBooksData(await client.MyBooks.GetCurrentlyReadingAsync());
            CheckOLMyBooksData(await client.MyBooks.GetAlreadyReadAsync());
            CheckOLMyBooksData(await client.MyBooks.GetWantToReadAsync());
        }
        #endregion

        #region Equality / Code Generation Tests
        [Fact]
        [Trait("Category", "Equality")]
        public async Task OLWorkEquality()
        {
            OpenLibraryClient client = new OpenLibraryClient();

            var container0 = await client.GetWorkAsync(WorksIDs[0]);
            var container1 = await client.GetWorkAsync(WorksIDs[0]);
            var container2 = await client.GetWorkAsync(WorksIDs[1]);

            Assert.True(container0 == container1);
            Assert.True(container1 == container0);
            Assert.True(container0 != container2);
            Assert.True(container2 != container0);
            Assert.True(container1 != container2);
            Assert.True(container2 != container1);

            EqualityTests(container0, container1, container2);
        }

        [Fact]
        [Trait("Category", "Equality")]
        public async Task OLAuthorEquality()
        {
            OpenLibraryClient client = new OpenLibraryClient();

            var container0 = await client.GetAuthorAsync(AuthorIDs[0]);
            var container1 = await client.GetAuthorAsync(AuthorIDs[0]);
            var container2 = await client.GetAuthorAsync(AuthorIDs[1]);

            Assert.True(container0 == container1);
            Assert.True(container1 == container0);
            Assert.True(container0 != container2);
            Assert.True(container2 != container0);
            Assert.True(container1 != container2);
            Assert.True(container2 != container1);

            EqualityTests(container0, container1, container2);
        }

        [Fact]
        [Trait("Category", "Equality")]
        public async Task OLEditionEquality()
        {
            OpenLibraryClient client = new OpenLibraryClient();

            var container0 = await client.GetEditionAsync(EditionsIDs[0]);
            var container1 = await client.GetEditionAsync(EditionsIDs[0]);
            var container2 = await client.GetEditionAsync(EditionsIDs[1]);

            Assert.True(container0 == container1);
            Assert.True(container1 == container0);
            Assert.True(container0 != container2);
            Assert.True(container2 != container0);
            Assert.True(container1 != container2);
            Assert.True(container2 != container1);

            EqualityTests(container0, container1, container2);
        }

        [Fact]
        [Trait("Category", "Equality")]
        public async Task OLWorkDataEquality()
        {
            OpenLibraryClient client = new OpenLibraryClient();

            var container0 = await client.Work.GetDataAsync(WorksIDs[0]);
            var container1 = await client.Work.GetDataAsync(WorksIDs[0]);
            var container2 = await client.Work.GetDataAsync(WorksIDs[1]);

            Assert.True(container0 == container1);
            Assert.True(container1 == container0);
            Assert.True(container0 != container2);
            Assert.True(container2 != container0);
            Assert.True(container1 != container2);
            Assert.True(container2 != container1);

            EqualityTests(container0, container1, container2);
        }

        [Fact]
        [Trait("Category", "Equality")]
        public async Task OLAuthorDataEquality()
        {
            OpenLibraryClient client = new OpenLibraryClient();

            var container0 = await client.Author.GetDataAsync(AuthorIDs[0]);
            var container1 = await client.Author.GetDataAsync(AuthorIDs[0]);
            var container2 = await client.Author.GetDataAsync(AuthorIDs[1]);

            Assert.True(container0 == container1);
            Assert.True(container1 == container0);
            Assert.True(container0 != container2);
            Assert.True(container2 != container0);
            Assert.True(container1 != container2);
            Assert.True(container2 != container1);

            EqualityTests(container0, container1, container2);
        }

        [Fact]
        [Trait("Category", "Equality")]
        public async Task OLEditionDataEquality()
        {
            OpenLibraryClient client = new OpenLibraryClient();

            var container0 = await client.Edition.GetDataAsync(EditionsIDs[0]);
            var container1 = await client.Edition.GetDataAsync(EditionsIDs[0]);
            var container2 = await client.Edition.GetDataAsync(EditionsIDs[1]);

            Assert.True(container0 == container1);
            Assert.True(container1 == container0);
            Assert.True(container0 != container2);
            Assert.True(container2 != container0);
            Assert.True(container1 != container2);
            Assert.True(container2 != container1);

            EqualityTests(container0, container1, container2);
        }

        [Fact]
        [Trait("Category", "Equality")]
        public async Task OLListEquality()
        {
            OpenLibraryClient client = new OpenLibraryClient();

            var container0 = await client.List.GetListAsync("luca3317", "OL225359L");
            var container1 = await client.List.GetListAsync("luca3317", "OL225359L");
            var container2 = await client.List.GetListAsync("luca3317", "OL225360L");

            Assert.True(container0 == container1);
            Assert.True(container1 == container0);
            Assert.True(container0 != container2);
            Assert.True(container2 != container0);
            Assert.True(container1 != container2);
            Assert.True(container2 != container1);

            EqualityTests(container0, container1, container2);
        }

        [Fact]
        [Trait("Category", "Equality")]
        public async Task OLMyBooksEquality()
        {
            OpenLibraryClient client = new OpenLibraryClient();

            var container0 = await client.MyBooks.GetCurrentlyReadingAsync("luca3317");
            var container1 = await client.MyBooks.GetCurrentlyReadingAsync("luca3317");
            var container2 = await client.MyBooks.GetAlreadyReadAsync("luca3317");

            Assert.True(container0 == container1);
            Assert.True(container1 == container0);
            Assert.True(container0 != container2);
            Assert.True(container2 != container0);
            Assert.True(container1 != container2);
            Assert.True(container2 != container1);

            EqualityTests(container0, container1, container2);
        }

        [Fact]
        [Trait("Category", "Equality")]
        public async Task OLRecentChangesEquality()
        {
            OpenLibraryClient client = new OpenLibraryClient();

            var container0 = (await client.RecentChanges.GetRecentChangesAsync("2022"))[0];
            var container1 = (await client.RecentChanges.GetRecentChangesAsync("2022"))[0];
            var container2 = (await client.RecentChanges.GetRecentChangesAsync("2021"))[0];

            Assert.True(container0 == container1);
            Assert.True(container1 == container0);
            Assert.True(container0 != container2);
            Assert.True(container2 != container0);
            Assert.True(container1 != container2);
            Assert.True(container2 != container1);

            EqualityTests(container0, container1, container2);
        }

        [Fact]
        [Trait("Category", "Equality")]
        public async Task OLSubjectDataEquality()
        {
            OpenLibraryClient client = new OpenLibraryClient();

            var container0 = await client.Subject.GetDataAsync("magic");
            var container1 = await client.Subject.GetDataAsync("magic");
            var container2 = await client.Subject.GetDataAsync("fantasy");

            Assert.True(container0 == container1);
            Assert.True(container1 == container0);
            Assert.True(container0 != container2);
            Assert.True(container2 != container0);
            Assert.True(container1 != container2);
            Assert.True(container2 != container1);

            EqualityTests(container0, container1, container2);
        }

        private void EqualityTests(object o, object equal, object unequal)
        {
            Assert.NotNull(o);
            Assert.NotNull(equal);
            Assert.NotNull(unequal);

            Assert.True(o.Equals(o));
            Assert.True(equal.Equals(equal));
            Assert.True(unequal.Equals(unequal));
            Assert.True(o.Equals(equal));
            Assert.True(equal.Equals(o));
            Assert.True(!o.Equals(unequal));
            Assert.True(!unequal.Equals(o));
            Assert.True(!equal.Equals(unequal));
            Assert.True(!unequal.Equals(equal));

            Assert.True(EqualityComparer<object>.Default.Equals(o, o));
            Assert.True(EqualityComparer<object>.Default.Equals(equal, equal));
            Assert.True(EqualityComparer<object>.Default.Equals(unequal, unequal));
            Assert.True(EqualityComparer<object>.Default.Equals(o, equal));
            Assert.True(EqualityComparer<object>.Default.Equals(equal, o));
            Assert.False(EqualityComparer<object>.Default.Equals(o, unequal));
            Assert.False(EqualityComparer<object>.Default.Equals(unequal, o));
            Assert.False(EqualityComparer<object>.Default.Equals(unequal, equal));
            Assert.False(EqualityComparer<object>.Default.Equals(equal, unequal));

            Assert.Equal(o.GetHashCode(), equal.GetHashCode());
            Assert.NotEqual(o.GetHashCode(), unequal.GetHashCode());
        }
        #endregion

        #region Serialization Tests
        [Fact]
        [Trait("Category", "Serialization")]
        public async Task OLWorkDataSerializationTest()
        {
            HttpClient client = new HttpClient();

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
        [Trait("Category", "Serialization")]
        public async Task OLEditionDataSerializationTest()
        {
            HttpClient client = new HttpClient();

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
        [Trait("Category", "Serialization")]
        public async Task OLAuthorDataSerializationTest()
        {
            HttpClient client = new HttpClient();

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
        [Trait("Category", "Serialization")]
        public async Task OLRatingsDataSerializationTest()
        {
            HttpClient client = new HttpClient();

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
        [Trait("Category", "Serialization")]
        public async Task OLBookshelvesDataSerializationTest()
        {
            HttpClient client = new HttpClient();

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
        [Trait("Category", "Serialization")]
        public async Task OLSubjectDataSerializationTest()
        {
            HttpClient client = new HttpClient();

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
        [Trait("Category", "Serialization")]
        public async Task OLWorkSerializationTest()
        {
            HttpClient client = new HttpClient();

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
        [Trait("Category", "Serialization")]
        public async Task OLAuthorSerializationTest()
        {
            HttpClient client = new HttpClient();

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
        [Trait("Category", "Serialization")]
        public async Task OLEditionSerializationTest()
        {
            HttpClient client = new HttpClient();

            foreach (string id in EditionsIDs)
            {
                var data = await OLEditionLoader.GetDataAsync(client, id);
                var cover = await OLImageLoader.GetCoverAsync(client, "OLID", id, "S");

                OLEdition edition = new OLEdition()
                {
                    ID = id,
                    Data = data,
                    CoverS = cover
                };

                string serialized = JsonConvert.SerializeObject(edition);
                var deserialized = JsonConvert.DeserializeObject<OLEdition>(serialized);
                string reserialized = JsonConvert.SerializeObject(deserialized);
                Assert.Equal(edition, deserialized);
                Assert.Equal(serialized, reserialized);
            }
        }

        [Fact]
        [Trait("Category", "Serialization")]
        public async Task OLRecentChangesSerializationTest()
        {
            HttpClient client = new HttpClient();
            var data = await OLRecentChangesLoader.GetRecentChangesAsync(client, year: "2020");

            string serialized = JsonConvert.SerializeObject(data);
            var deserialized = JsonConvert.DeserializeObject<OLRecentChangesData[]>(serialized);
            string reserialized = JsonConvert.SerializeObject(deserialized);
            Assert.Equal(data, deserialized);
            Assert.Equal(serialized, reserialized);
        }

        [Fact]
        [Trait("Category", "Serialization")]
        public async Task OLPartnerSerializationTest()
        {
            HttpClient client = new HttpClient();
            var data = await OLPartnerLoader.GetMultiDataAsync(client, SetBibkeyPrefix("olid", EditionsIDs[0]), SetBibkeyPrefix("olid", EditionsIDs[1]));

            string serialized = JsonConvert.SerializeObject(data);
            var deserialized = JsonConvert.DeserializeObject<OLPartnerData[]>(serialized);
            string reserialized = JsonConvert.SerializeObject(deserialized);
            Assert.Equal(data, deserialized);
            Assert.Equal(serialized, reserialized);
        }
        #endregion

        #region OpenLibraryUtility Tests
        [Fact]
        [Trait("Category", "OpenLibraryUtility")]
        public void URLBuilderTests()
        {
            var ps = new KeyValuePair<string, string>[]
            {
                new ("limit", "10"),
                new ("key", "value")
            };

            //string pstring = ".json?limit=10&key=value";

            string id = "OL1234W", path = "ratings";

            Assert.Equal(BuildWorksUri(id, path, ps), BuildUri(OLRequestAPI.Books_Works, id + "/" + path, ps));
            Assert.Equal("https://openlibrary.org/works/OL1234W/ratings.json?limit=10&key=value", BuildUri(OLRequestAPI.Books_Works, id + "/" + path, ps).ToString());

            path = "works";
            Assert.Equal(BuildAuthorsUri(id, path, ps), BuildUri(OLRequestAPI.Authors, id + "/" + path, ps));
            Assert.Equal("https://openlibrary.org/authors/OL1234W/works.json?limit=10&key=value", BuildUri(OLRequestAPI.Authors, id + "/" + path, ps).ToString());

            path = "lists";
            Assert.Equal(BuildEditionsUri(id, path, ps), BuildUri(OLRequestAPI.Books_Editions, id + "/" + path, ps));
            Assert.Equal("https://openlibrary.org/books/OL1234W/lists.json?limit=10&key=value", BuildUri(OLRequestAPI.Books_Editions, id + "/" + path, ps).ToString());

            Assert.Equal(BuildISBNUri(id), BuildUri(OLRequestAPI.Books_ISBN, id));
            Assert.Equal("https://openlibrary.org/isbn/OL1234W.json", BuildUri(OLRequestAPI.Books_ISBN, id).ToString());

            string isbn = "0123456789", bibkey = "ISBN:" + isbn;
            Assert.Equal(BuildBooksUri(bibkey), BuildUri(OLRequestAPI.Books, "", new KeyValuePair<string, string>("bibkeys", bibkey)));
            Assert.Equal("https://openlibrary.org/api/books.json?bibkeys=ISBN:0123456789", BuildUri(OLRequestAPI.Books, "", new KeyValuePair<string, string>("bibkeys", bibkey)).ToString());

            string subject = "egypt";
            Assert.Equal(BuildSubjectsUri(subject, path, ps), BuildUri(OLRequestAPI.Subjects, subject + "/" + path, ps));
            Assert.Equal("https://openlibrary.org/subjects/egypt/lists.json?limit=10&key=value", BuildUri(OLRequestAPI.Subjects, subject + "/" + path, ps).ToString());

            string query = "nabokov"; path = "authors";
            var ps2 = new KeyValuePair<string, string>[]
            {
                new ("limit", "10"),
                new ("key", "value"),
                new ("q", query)
            };
            Assert.Equal(BuildSearchUri(query, path, ps), BuildUri(OLRequestAPI.Search, path, ps2));
            Assert.Equal("https://openlibrary.org/search/authors.json?limit=10&key=value&q=nabokov", BuildUri(OLRequestAPI.Search, path, ps2).ToString());

            string username = "luca3317"; id = "OL1234L"; path = "editions";
            Assert.Equal(BuildListsUri(username, id, path, ps), BuildUri(OLRequestAPI.Lists, username + "/lists/" + id + "/" + path, ps));
            Assert.Equal("https://openlibrary.org/people/luca3317/lists/OL1234L/editions.json?limit=10&key=value", BuildUri(OLRequestAPI.Lists, username + "/lists/" + id + "/" + path, ps).ToString());

            string year = "2012", month = "08", day = "30", kind = "merge-authors";
            Assert.Equal(BuildRecentChangesUri(year, month, day, kind, ps), BuildUri(OLRequestAPI.RecentChanges, year + "/" + month + "/" + day + "/" + kind, ps));
            Assert.Equal("https://openlibrary.org/recentchanges/2012/08/30/merge-authors.json?limit=10&key=value", BuildUri(OLRequestAPI.RecentChanges, year + "/" + month + "/" + day + "/" + kind, ps).ToString());

            CoverIdType type = CoverIdType.ID; ImageSize size = ImageSize.Small;
            path = type.ToString() + "/" + id + "-" + size.GetString();
            Assert.Equal(BuildCoversUri(type, id, size, ps), BuildUri(OLRequestAPI.Covers, path, ps));
            Assert.Equal(BuildCoversUri(type.GetString(), id, size.GetString(), ps), BuildUri(OLRequestAPI.Covers, path, ps));
            Assert.Equal("https://covers.openlibrary.org/b/ID/OL1234L-S.jpg?limit=10&key=value", BuildUri(OLRequestAPI.Covers, path, ps).ToString());

            PartnerIdType ptype = PartnerIdType.ISBN; id = "0596156715";
            Assert.Equal("https://openlibrary.org/api/volumes/brief/isbn/0596156715.json", BuildPartnerUri(ptype, id).ToString());

            string id1 = "id:1;lccn:50006784";
            string id2 = "olid:OL6179000M;lccn:55011330";
            string ids = id1 + "|" + id2;
            Assert.Equal("https://openlibrary.org/api/volumes/brief/json/id:1;lccn:50006784|olid:OL6179000M;lccn:55011330", BuildPartnerMultiUri(id1, id2).ToString());
        }

        [Fact]
        [Trait("Category", "OpenLibraryUtility")]
        public void IdHelpersTests()
        {
            string k1 = "works/OL1234A", k2 = " /some/path/OL1234A", k3 = "/path/OL1234A/";
            Assert.Equal("OL1234A", ExtractIdFromKey(k1));
            Assert.Equal("OL1234A", ExtractIdFromKey(k2));
            Assert.Equal("OL1234A", ExtractIdFromKey(k3));

            string bk1 = "type:ID", bk2 = "this is a text?!:ID", bk3 = "ID";
            Assert.Equal("ID", GetRawBibkey(bk1));
            Assert.Equal("ID", GetRawBibkey(bk2));
            Assert.Equal("ID", GetRawBibkey(bk3));
            Assert.Equal("type", GetBibkeyPrefix(bk1));
            Assert.Equal("this is a text?!", GetBibkeyPrefix(bk2));
            Assert.Equal("", GetBibkeyPrefix(bk3));

            Assert.Equal("TYPE:ID", SetBibkeyPrefix("TYPE", bk1));
            Assert.Equal("TYPE:ID", SetBibkeyPrefix("TYPE", bk2));
            Assert.Equal("TYPE:ID", SetBibkeyPrefix("TYPE", bk3));

        }
        #endregion

        #region Utility
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
            // None of the fields on list data seem to be guaranteed to be
            // set depending on the source
            // Checks on the extensiondata dont indicate different aliases
        }

        void CheckOLRecentChangesData(OLRecentChangesData? data)
        {
            Assert.NotNull(data);
            Assert.NotEqual("", data.ID);
            Assert.NotEqual("", data.Timestamp);
            Assert.NotEqual("", data.AuthorKey);
            Assert.NotEqual("", data.Kind);
            Assert.NotEmpty(data.Changes);
            foreach (var change in data.Changes)
            {
                Assert.NotNull(change);
                Assert.NotEqual(-1, change.Revision);
                Assert.NotEqual("", change.Key);
            }
        }

        void CheckOLMyBooksData(OLMyBooksData? data)
        {
            Assert.NotNull(data);
            Assert.NotEqual(-1, data.Page);
            if (data.ReadingLogEntries == null) return;

            foreach (var entry in data.ReadingLogEntries)
            {
                Assert.NotNull(entry);
                Assert.NotNull(entry.LoggedDate);
                Assert.NotNull(entry.Work);
            }
        }

        void CheckOLSeedData(OLSeedData? data)
        {
            Assert.NotNull(data);
            Assert.NotEqual("", data.Title);
            Assert.NotEqual("", data.Type);
            Assert.NotEqual("", data.Key);
        }
        #endregion
    }
}
#pragma warning restore 8604, 8602