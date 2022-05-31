using AutoMapper;
using DomainModels.Dtos.Abstraction;
using Repository.Constants;

namespace Repository.Converters
{
    public class RequirementValueToArrayConverter : IValueResolver<object, object, object[]>
    {
        public object[] Resolve(object source, object destination, object[] destMember, ResolutionContext context)
        {
            IValueToArrayConvertible valueConvertible = source as IValueToArrayConvertible;
            if(valueConvertible.Value != null)
            {
                return valueConvertible.Value.Split(DbValueSeperatorConstants.TripleDashSeperator);
            }
            return null;
        }
    }
}
