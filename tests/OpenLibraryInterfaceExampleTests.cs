using Moq;
using OpenLibraryNET;

namespace Tests
{
    public class OpenLibraryConsumer
    {
        private readonly IOpenLibraryClient _client;

        public OpenLibraryConsumer(IOpenLibraryClient? client = null)
        {
            _client = client switch
            {
                { } => client,
                _ => new OpenLibraryClient()
            };
        }

        public async Task<OLEdition> GetEdition(string editionId) =>
            await _client.GetEditionAsync(editionId);

        public async Task<OpenLibraryNET.Data.OLWorkData[]?> Search(string query) =>
            await _client.Search.GetSearchResultsAsync(query);
    }

    public class OpenLibraryInterfaceExampleTests
    {
        private readonly Mock<IOpenLibraryClient> _client = new();

        [Fact]
        public async Task Will_mock_async_open_library_client_calls_with_a_return_body()
        {
            var exampleEdition = new OLEdition()
            {
                ID = "This is a mocked Id",
                Data = new OpenLibraryNET.Data.OLEditionData() { Title = "This is a mocked title" }
            };
            _client.Setup(x => x.GetEditionAsync(It.IsAny<string>())).ReturnsAsync(exampleEdition);

            var consumer = new OpenLibraryConsumer(_client.Object);
            var result = await consumer.GetEdition("ExampleTitle");

            Assert.Equal(exampleEdition, result);
        }

        [Fact]
        public async Task Will_mock_async_open_library_client_calls_with_null_return()
        {
            OpenLibraryNET.Data.OLWorkData[]? expected = null;
            _client
                .Setup(x => x.Search.GetSearchResultsAsync(It.IsAny<string>()))
                .ReturnsAsync(expected);

            var consumer = new OpenLibraryConsumer(_client.Object);
            var result = await consumer.Search("Example Search Query");

            Assert.Null(result);
        }

        [Fact]
        public async Task Will_mock_when_an_exception_is_thrown()
        {
            _client
                .Setup(x => x.GetEditionAsync(It.IsAny<string>()))
                .ThrowsAsync(new InvalidOperationException());

            var consumer = new OpenLibraryConsumer(_client.Object);
            await Assert.ThrowsAsync<InvalidOperationException>(
                async () => await consumer.GetEdition("ExampleTitle")
            );
        }
    }
}
