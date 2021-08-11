using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsi.Template.Core.Abstractions;

namespace Tsi.Template.Core.Configuration
{
    public class UserSettings: ISettings
    {
        public bool UserNameEnabled { get; set; }
        public bool AdministratorActivationRequired { get; set; }
    }
}
