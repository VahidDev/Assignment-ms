using System.ComponentModel.DataAnnotations;

namespace DomainModels.Dtos
{
    public class CreateFilterDto
    {
        [Required]
        public string Requirement { get; set; }
        [Required]
        public string Operator { get; set; }
        [Required]
        public object[] Value { get; set; }
    }
}
