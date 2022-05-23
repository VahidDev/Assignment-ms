namespace Assignment.Services.Abstraction
{
    public interface IRuntimeServices
    {
        T CreateCustomObject<T>(IDictionary<string, object> propNameAndValueDict);
    }
}
