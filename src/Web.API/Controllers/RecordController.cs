using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.API.Services.Records;
using Web.API.Services.Records.Models;

namespace Web.API.Controllers
{
    [Route("record")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private readonly IRecordService _recordService;

        public RecordController(IRecordService recordService)
        {
            _recordService = recordService;
        }

        [HttpGet]
        public async Task<IList<RecordResponse>> Get()
        {
            return await _recordService.Get();
        }

        [HttpGet("{id:guid}")]
        public async Task<RecordResponse> GetById([FromRoute] Guid id)
        {
            return await _recordService.GetById(id);
        }

        [HttpPost]
        public async Task<Guid> Create([FromBody] CreateRecordRequest request)
        {
            return await _recordService.Create(request);
        }

        [HttpPut("{id:guid}")]
        public async Task Update([FromRoute] Guid id, [FromBody] UpdateRecordRequest request)
        {
            await _recordService.Update(id, request);
        }

        [HttpDelete("{id:guid}")]
        public async Task Delete([FromRoute] Guid id)
        {
            await _recordService.Delete(id);
        }
    }
}
