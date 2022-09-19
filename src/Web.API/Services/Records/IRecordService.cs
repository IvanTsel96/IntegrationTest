using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.API.Services.Records.Models;

namespace Web.API.Services.Records
{
    public interface IRecordService
    {
        Task<IList<RecordResponse>> Get();
        Task<RecordResponse> GetById(Guid id);
        Task<Guid> Create(CreateRecordRequest request);
        Task Update(Guid id, UpdateRecordRequest request);
        Task Delete(Guid id);
    }
}
