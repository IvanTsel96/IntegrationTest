using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Web.API.Integration.Tests.Extensions;
using Web.API.Integration.Tests.Mock;
using Xunit;

namespace Web.API.Integration.Tests.RecordController
{
    public class GetRecordControllerTests : IClassFixture<RecordApiFactory>
    {
        private readonly HttpClient _httpClient;

        public GetRecordControllerTests(RecordApiFactory recordApiFactory)
        {
            _httpClient = recordApiFactory
                .WithWebHostBuilder(builder => builder
                    .ConfigureServices(services => services.SeedDatabase(SeedGetRecordDatabase)))
                .CreateClient();
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
            Assert.Contains(records, record => record.Id == RecordMockHelper.IdToGet);
            Assert.Contains(records, record => record.Name == "GET");
        }

        private static void SeedGetRecordDatabase(DbContext context)
        {
            if (!context.Set<Domain.Record>().Any(x => x.Id == RecordMockHelper.IdToGet))
            {
                context.Add(RecordMockHelper.GetRecord(RecordMockHelper.IdToGet, "GET"));
            }
        }
    }
}
