using System.Net;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Web.API.Integration.Tests.Mock;
using Web.API.Services.Records.Models;
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
        public async Task UpdateRecord_Success()
        {
            // Arrange
            var request = new UpdateRecordRequest
            {
                Name = "PUT Edited"
            };
            var content = GetHttpContent(request);

            // Act
            var response = await _httpClient.PutAsync($"record/{RecordMockHelper.IdToUpdate}", content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData(
            null,
            null,
            "The Name field is required.")]
        [InlineData(
            @"Long text. Long text. Long text. Long text. Long text. Long text.
            Long text. Long text. Long text. Long text. Long text. Long text.",
            null,
            "The field Name must be a string or array type with a maximum length of '128'.")]
        [InlineData(
            "Name",
            @"Long text. Long text. Long text. Long text. Long text. Long text.
            Long text. Long text. Long text. Long text. Long text. Long text.
            Long text. Long text. Long text. Long text. Long text. Long text.
            Long text. Long text. Long text. Long text. Long text. Long text.
            Long text. Long text. Long text. Long text. Long text. Long text.
            Long text. Long text. Long text. Long text. Long text. Long text.
            Long text. Long text. Long text. Long text. Long text. Long text.",
            "The field Description must be a string or array type with a maximum length of '512'.")]
        public async Task UpdateRecord_InvalidRequest(string name, string description, string expectedError)
        {
            // Arrange
            var request = new UpdateRecordRequest
            {
                Name = name,
                Description = description
            };

            var content = GetHttpContent(request);

            // Act
            var response = await _httpClient.PutAsync($"record/{RecordMockHelper.IdToUpdate}", content);
            var responseAsString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains(expectedError, responseAsString);
        }

        [Fact]
        public async Task UpdateRecord_NotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            var request = new UpdateRecordRequest
            {
                Name = "Name",
                Description = "Description"
            };
            var content = GetHttpContent(request);

            // Act
            var response = await _httpClient.PutAsync($"record/{id}", content);
            var responseAsString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Contains($"Запись с ID: {id} не найдена", responseAsString);
        }

        private static HttpContent GetHttpContent(UpdateRecordRequest request)
        {
            var content = new StringContent(JsonSerializer.Serialize(request));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return content;
        }
    }
}
