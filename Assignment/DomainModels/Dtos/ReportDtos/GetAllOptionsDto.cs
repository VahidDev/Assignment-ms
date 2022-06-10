namespace DomainModels.Dtos
{
    public class GetAllOptionsDto
    {
        public OptionsDto EntityOptions { get; set; } = new();
        public OptionsDto FunctionalAreaOptions { get; set; } = new();
        public OptionsDto JobTitleOptions { get; set; } = new();
        public OptionsDto LocaionOptions { get; set; } = new();
    }
}
