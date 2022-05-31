using AutoMapper;
using DomainModels.Dtos.Abstraction;
using Repository.Constants;

namespace Repository.Converters
{
    public class VolunteerColumnsToStringConverter : IValueResolver<object, object, string>
    {
        public string Resolve
            (
            object source, 
            object destination, 
            string destMember, 
            ResolutionContext context
            )
        {
            IVolunteerColumnsToStringConvertible toStringConvertible 
                = source as IVolunteerColumnsToStringConvertible;
            if (toStringConvertible.VolunteerColumns != null)
            {
                return string.Join(
                    DbValueSeperatorConstants.TripleDashSeperator, 
                    toStringConvertible.VolunteerColumns
                    );
            }
            return null;
        }
    }
}
