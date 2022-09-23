using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Web.API.Integration.Tests.Extensions;
using Web.API.Integration.Tests.Mock;
using Xunit;

namespace Web.API.Integration.Tests.RecordController
{
    public class DeleteRecordControllerTests : IClassFixture<RecordApiFactory>
    {
        private readonly HttpClient _httpClient;

        public DeleteRecordControllerTests(RecordApiFactory recordApiFactory)
        {
            _httpClient = recordApiFactory
                .WithWebHostBuilder(builder => builder
                    .ConfigureServices(services => services.SeedDatabase(SeedDeleteRecordDatabase)))
                .CreateClient();
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

        private static void SeedDeleteRecordDatabase(DbContext context)
        {
            if (!context.Set<Domain.Record>().Any(x => x.Id == RecordMockHelper.IdToDelete))
            {
                context.Add(RecordMockHelper.GetRecord(RecordMockHelper.IdToDelete, "DELETE"));
            }
        }
    }
}
