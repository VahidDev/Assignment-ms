using AutoMapper;
using DomainModels.Dtos.Abstraction;
using Repository.Constants;

namespace Repository.Converters
{
    public class VolunteerColumnsToArrayConverter : IValueResolver<object, object, object[]>
    {
        public object[] Resolve
            (
            object source, 
            object destination, 
            object[] destMember, 
            ResolutionContext context
            )
        {
            IVolunteerColumnsToArrayConvertible valueConvertible 
                = source as IVolunteerColumnsToArrayConvertible;
            if (valueConvertible.VolunteerColumns != null)
            {
                return valueConvertible.VolunteerColumns
                    .Split(DbValueSeperatorConstants.TripleDashSeperator);
            }
            return null;
        }
    }
}
