using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Web.API.Integration.Tests.Mock;
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
        public async Task DeleteRecord_Success()
        {
            // Arrange
            var id = RecordMockHelper.IdToDelete;

            // Act
            var response = await _httpClient.DeleteAsync($"record/{id}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteRecord_NotFound()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var response = await _httpClient.DeleteAsync($"record/{id}");
            var responseAsString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Contains($"Запись с ID: {id} не найдена", responseAsString);
        }
    }
}
