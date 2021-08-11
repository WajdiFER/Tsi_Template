using Tsi.Template.Core.Abstractions;

namespace Tsi.Template.Domain.Common
{
    public class User: BaseEntity, ICommonEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string NormalizedUsername { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; } 
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public bool LockedOut { get; set; }
        public bool Active { get; set; }
        public int FailedAttempts { get; set; }
    }
}
