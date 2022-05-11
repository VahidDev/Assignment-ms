using NpgsqlTypes;

namespace DomainModels.Models.Enums
{
    public enum Statusenum
    {
        [PgName("Assign")]
        Assigned,
        [PgName("Waitlist")]
        Waitlisted,
        [PgName("Free")]
        Free
    }
}
