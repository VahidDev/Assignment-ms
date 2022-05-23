using AutoMapper;
using DomainModels.Dtos.Abstraction;
using Repository.Constants;

namespace Repository.Converters
{
    public class RequirementValueToStringConverter : IValueResolver<object, object, string>
    {
        public string Resolve(object source, object destination, string destMember, ResolutionContext context)
        {
            IValueFromStringConvertible valueConvertible = source as IValueFromStringConvertible;
            if (valueConvertible != null)
            {
                return string.Join(DbValueSeperatorConstants.CommaSeperator,valueConvertible.Value);
            }
            return null;
        }
    }
}
