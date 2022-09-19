using System;

namespace Web.API.Services.Records.Models
{
    public class RecordResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
