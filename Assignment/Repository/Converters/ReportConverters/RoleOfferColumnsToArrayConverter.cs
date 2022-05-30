using AutoMapper;
using DomainModels.Dtos.Abstraction;
using Repository.Constants;

namespace Repository.Converters
{
    public class RoleOfferColumnsToArrayConverter : IValueResolver<object, object, object[]>
    {
        public object[] Resolve
            (
            object source, 
            object destination, 
            object[] destMember, 
            ResolutionContext context
            )
        {
            IRoleOfferColumnsToArrayConvertible valueConvertible
                = source as IRoleOfferColumnsToArrayConvertible;
            if (valueConvertible != null)
            {
                return valueConvertible.RoleOfferColumns
                    .Split(DbValueSeperatorConstants.TripleDashSeperator);
            }
            return null;
        }
    }
}
