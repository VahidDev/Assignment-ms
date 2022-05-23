using System.ComponentModel.DataAnnotations;

namespace DomainModels.Dtos
{
    public class BaseDto: IBaseDto
    {
        [Required]
        public int? Id { get; set; }
    }
}
