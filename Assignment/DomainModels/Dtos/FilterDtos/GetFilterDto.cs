using DomainModels.Dtos.Base;

namespace DomainModels.Dtos
{
    public class GetFilterDto:BaseDto
    {
        public string Requirement { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }
    }
}
