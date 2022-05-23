using DomainModels.Dtos.Abstraction;
using System.ComponentModel.DataAnnotations;

namespace DomainModels.Dtos
{
    public class CreateFilterDto : IValueFromStringConvertible
    {
        [Required]
        public string Requirement { get; set; }
        [Required]
        public string Operator { get; set; }

        [Required]
        public object[] Value { get; set; }
    }
}
