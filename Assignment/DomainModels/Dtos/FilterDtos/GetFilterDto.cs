namespace DomainModels.Dtos
{
    public class GetFilterDto : BaseDto
    {
        public string Requirement { get; set; }
        public string Operator { get; set; }
        public object[] Value { get; set; }
    }
}
