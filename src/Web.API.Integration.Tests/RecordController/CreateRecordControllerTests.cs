using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Web.API.Services.Records.Models;
using Xunit;

namespace Web.API.Integration.Tests.RecordController
{
    public class CreateRecordControllerTests : IClassFixture<RecordApiFactory>
    {
        private readonly HttpClient _httpClient;

        public CreateRecordControllerTests(RecordApiFactory recordApiFactory)
        {
            _httpClient = recordApiFactory.CreateClient();
        }

        [Fact]
        public async Task CreateRecord_Success()
        {
            // Arrange
            var request = new CreateRecordRequest
            {
                Name = "POST",
                Description = "POST"
            };

            var content = GetHttpContent(request);
            
            // Act
            var response = await _httpClient.PostAsync("record", content);
            var responseAsString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.DoesNotContain(Guid.Empty.ToString(), responseAsString);
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
        public async Task CreateRecord_InvalidRequest(string name, string description, string expectedError)
        {
            // Arrange
            var request = new CreateRecordRequest
            {
                Name = name,
                Description = description
            };

            var content = GetHttpContent(request);

            // Act
            var response = await _httpClient.PostAsync("record", content);
            var responseAsString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains(expectedError, responseAsString);
        }

        private static HttpContent GetHttpContent(CreateRecordRequest request)
        {
            var content = new StringContent(JsonSerializer.Serialize(request));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return content;
        }
    }
}
