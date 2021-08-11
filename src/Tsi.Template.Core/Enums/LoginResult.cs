using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsi.Template.Core.Enums
{
    public enum LoginResult
    {
        NotFound,
        WrongPassword,
        LockedOut,
        NotActive,
        Success
    } 

    public enum RegisterResult
    {
        UsernameNotProvided,
        UsernameUsed,
        EmailNotProvided,
        EmailUsed,
        PhoneNumberUsed,
        Success
    }
}
