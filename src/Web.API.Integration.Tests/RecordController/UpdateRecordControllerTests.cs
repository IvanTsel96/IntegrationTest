using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Web.API.Integration.Tests.RecordController
{
    public class UpdateRecordControllerTests : IClassFixture<RecordApiFactory>
    {
        private readonly HttpClient _httpClient;

        public UpdateRecordControllerTests(RecordApiFactory recordApiFactory)
        {
            _httpClient = recordApiFactory.CreateClient();
        }

        [Fact]
        public async Task UpdateRecord_Seccuss()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
