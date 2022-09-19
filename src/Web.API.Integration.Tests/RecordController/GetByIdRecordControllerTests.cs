using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Web.API.Integration.Tests.RecordController
{
    public class GetByIdRecordControllerTests : IClassFixture<RecordApiFactory>
    {
        private readonly HttpClient _httpClient;

        public GetByIdRecordControllerTests(RecordApiFactory recordApiFactory)
        {
            _httpClient = recordApiFactory.CreateClient();
        }

        [Fact]
        public async Task GetByIdRecord_Seccuss()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
