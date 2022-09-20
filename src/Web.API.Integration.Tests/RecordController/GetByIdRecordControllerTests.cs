﻿using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Web.API.Integration.Tests.Mock;
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
        public async Task GetByIdRecord_Success()
        {
            // Arrange
            var id = RecordMockHelper.IdToGet;

            // Act
            var response = await _httpClient.GetAsync($"record/{id}");
            var record = await response.Content.ReadFromJsonAsync<Domain.Record>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(record);
            Assert.Equal(id, record.Id);
            Assert.Equal("GET", record.Name);
        }

        [Fact]
        public async Task GetByIdRecord_NotFound()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var response = await _httpClient.GetAsync($"record/{id}");
            var responseAsString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Contains($"Запись с ID: {id} не найдена", responseAsString);
        }
    }
}
