
namespace DomainModels.Dtos
{
    public class UpdateFilterDto
    {
        public int? Id { get; set; }
        public string Requirement { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }
    }
}
