using System.ComponentModel;

namespace Tsi.Template.Core.Enums
{
    public enum NavigationItems
    {
        None = 0,
        //Tiers
        Grh = 1,
        Departement = 2,
        Employee = 4 ,

        Administration = 8,
        Users = 16,
        UserRoles = 32,
        Acl = 64,

        Gesc = 128,
        Product = 256


    }
}