using System.ComponentModel.DataAnnotations;

namespace DomainModels.Dtos.Base
{
    public class BaseDto: IBaseDto
    {
        [Required]
        public int? Id { get; set; }
    }
}
