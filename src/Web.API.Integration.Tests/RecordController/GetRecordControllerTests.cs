using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Web.API.Integration.Tests.RecordController
{
    public class GetRecordControllerTests : IClassFixture<RecordApiFactory>
    {
        private readonly HttpClient _httpClient;

        public GetRecordControllerTests(RecordApiFactory recordApiFactory)
        {
            _httpClient = recordApiFactory.CreateClient();
        }

        [Fact]
        public async Task GetRecord_Seccuss()
        {
            // Act
            var response = await _httpClient.GetAsync("record");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
