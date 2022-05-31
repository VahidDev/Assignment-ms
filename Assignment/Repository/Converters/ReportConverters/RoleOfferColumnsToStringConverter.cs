using AutoMapper;
using DomainModels.Dtos.Abstraction;
using Repository.Constants;

namespace Repository.Converters
{
    public class RoleOfferColumnsToStringConverter : IValueResolver<object, object, string>
    {
        public string Resolve
            ( object source
            , object destination
            , string destMember
            , ResolutionContext context)
        {
            IRoleOfferColumnsToStringConvertible toStringConvertible 
                = source as IRoleOfferColumnsToStringConvertible;
            if (toStringConvertible.RoleOfferColumns != null)
            {
                return string
                    .Join(DbValueSeperatorConstants.TripleDashSeperator
                    , toStringConvertible.RoleOfferColumns);
            }
            return null;
        }
    }
}
