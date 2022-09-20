using System.ComponentModel.DataAnnotations;

namespace Web.API.Services.Records.Models
{
    public class UpdateRecordRequest
    {
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [MaxLength(512)]
        public string Description { get; set; }
    }
}
