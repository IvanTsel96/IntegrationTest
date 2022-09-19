using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Web.API.Integration.Tests.RecordController
{
    public class DeleteRecordControllerTests : IClassFixture<RecordApiFactory>
    {
        private readonly HttpClient _httpClient;

        public DeleteRecordControllerTests(RecordApiFactory recordApiFactory)
        {
            _httpClient = recordApiFactory.CreateClient();
        }

        [Fact]
        public async Task DeleteRecord_Seccuss()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
