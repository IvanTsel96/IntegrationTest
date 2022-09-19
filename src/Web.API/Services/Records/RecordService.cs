using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.API.Domain;
using Web.API.Infrastructure.Repositories.Records;
using Web.API.Services.Records.Models;

namespace Web.API.Services.Records
{
    public class RecordService : IRecordService
    {
        private readonly IRecordRepository _recordRepository;

        public RecordService(IRecordRepository recordRepository)
        {
            _recordRepository = recordRepository;
        }

        public async Task<IList<RecordResponse>> Get()
        {
            var records = await _recordRepository.Get();

            return records.Select(MapToRecordResponse).ToList();
        }

        public async Task<RecordResponse> GetById(Guid id)
        {
            var record = await _recordRepository.GetById(id);

            if (record == null)
            {
                throw new ApplicationException($"Запись с ID: {id} не найдена");
            }

            return MapToRecordResponse(record);
        }

        public Task<Guid> Create(CreateRecordRequest request)
        {
            var record = new Record
            {
                Name = request.Name,
                Description = request.Description
            };

            return _recordRepository.Create(record);
        }

        public async Task Update(Guid id, UpdateRecordRequest request)
        {
            var existingRecord = await _recordRepository.GetById(id);

            if (existingRecord == null)
            {
                throw new ApplicationException($"Запись с ID: {id} не найдена");
            }

            existingRecord.Name = request.Name;
            existingRecord.Description = request.Description;

            await _recordRepository.Update(existingRecord);
        }

        public async Task Delete(Guid id)
        {
            var record = await _recordRepository.GetById(id);

            if (record == null)
            {
                throw new ApplicationException($"Запись с ID: {id} не найдена");
            }

            await _recordRepository.Delete(record);
        }

        private static RecordResponse MapToRecordResponse(Record record)
        {
            return new RecordResponse
            {
                Id = record.Id,
                Name = record.Name,
                Description = record.Description
            };
        }
    }
}
