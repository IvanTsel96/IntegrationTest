using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.API.Domain;

namespace Web.API.Infrastructure.Repositories.Records
{
    public interface IRecordRepository
    {
        Task<IList<Record>> Get();
        Task<Record> GetById(Guid id);
        Task<Guid> Create(Record record);
        Task Update(Record record);
        Task Delete(Record record);
    }
}
