using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.API.Domain;

namespace Web.API.Infrastructure.Repositories.Records
{
    public class RecordRepository : IRecordRepository
    {
        private readonly DatabaseContext _context;

        public RecordRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IList<Record>> Get()
        {
            return await _context.Set<Record>().ToListAsync();
        }

        public Task<Record> GetById(Guid id)
        {
            return _context.Set<Record>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Guid> Create(Record record)
        {
            await _context.Set<Record>().AddAsync(record);

            await _context.SaveChangesAsync();

            return record.Id;
        }

        public async Task Update(Record record)
        {
            _context.Set<Record>().Update(record);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(Record record)
        {
            _context.Set<Record>().Remove(record);

            await _context.SaveChangesAsync();
        }
    }
}
