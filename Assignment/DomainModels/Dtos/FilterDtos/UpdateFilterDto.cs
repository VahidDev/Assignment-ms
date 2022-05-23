
using DomainModels.Dtos.Abstraction;

namespace DomainModels.Dtos
{
    public class UpdateFilterDto : IValueFromStringConvertible
    {
        public int? Id { get; set; }
        public string Requirement { get; set; }
        public string Operator { get; set; }
        public object[] Value { get; set; }
    }
}
