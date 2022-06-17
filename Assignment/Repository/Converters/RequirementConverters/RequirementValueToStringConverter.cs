using AutoMapper;
using DomainModels.Dtos.Abstraction;
using Repository.Constants;

namespace Repository.Converters
{
    public class RequirementValueToStringConverter : IValueResolver<object, object, string>
    {
        public string Resolve(object source, object destination, string destMember, ResolutionContext context)
        {
            IValueToStringConvertible valueConvertible = source as IValueToStringConvertible;
            if (valueConvertible.Value != null && valueConvertible.Value.Length != 0)
            {
                return string.Join(DbValueSeperatorConstants.TripleDashSeperator, valueConvertible.Value);
            }
            return null;
        }
    }
}
