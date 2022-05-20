
using System.Text.Json.Serialization;

namespace DomainModels.Dtos
{
    public class UpdateFilterDto
    {
        public int? Id { get; set; }
        public string Requirement { get; set; }
        public string Operator { get; set; }
        public object[] Value { get; set; }
    }
}
