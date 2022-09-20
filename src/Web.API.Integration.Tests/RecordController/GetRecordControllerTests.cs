using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
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
        public async Task GetRecord_Success()
        {
            // Act
            var response = await _httpClient.GetAsync("record");
            var records = await response.Content.ReadFromJsonAsync<IList<Domain.Record>>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(records);
            Assert.NotEmpty(records);
        }
    }
}
