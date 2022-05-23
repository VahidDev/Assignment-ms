using AutoMapper;
using DomainModels.Dtos.Abstraction;

namespace Repository.Converters
{
    public class RequirementValueToArrayConverter : IValueResolver<object, object, object[]>
    {
        public object[] Resolve(object source, object destination, object[] destMember, ResolutionContext context)
        {
            IValueFromArrayConvertible valueConvertible = source as IValueFromArrayConvertible;
            if(valueConvertible!=null)
            {
                return valueConvertible.Value.Split(",");
            }
            return null;
        }
    }
}
