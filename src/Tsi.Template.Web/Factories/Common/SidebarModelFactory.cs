using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tsi.Template.Abstraction.Common;
using Tsi.Template.Core;
using Tsi.Template.Core.Attributes;
using Tsi.Template.Core.Enums;
using Tsi.Template.Web.Factories.Common;

namespace SageClient.Core.Factories.SideBar
{
    [Injectable(typeof(ISidebarModelFactory))]
    public class SidebarModelFactory : ISidebarModelFactory
    {
        private readonly IUserService _userService;
        private readonly IUserRegistrationService _userRegistrationService;

        public SidebarModelFactory(IUserService userService, IUserRegistrationService userRegistrationService)
        {
            _userService = userService;
            _userRegistrationService = userRegistrationService;
        }


        public async Task<IList<MenuItem>> PrepareMenuItemsAsync(int selectedTab)
        {
            var result = new List<MenuItem>
            {
                await BuildGrhNavigationItemsAsync(selectedTab)
            };

            if (await _userService.IsUserAdminAsync(await _userRegistrationService.GetCurrentUserAsync()))
            {
                result.Add(await BuildAdministrationNavigationItemsAsync(selectedTab));
            }

            return result.OrderBy(menu => menu.DisplayOrder).ToList();
        }

        private async Task<MenuItem> BuildGrhNavigationItemsAsync(int selectedTab)
        {
            return await Task.FromResult(new MenuItem(selectedTab)
            {
                Icon = "far fa-address-book",
                DisplayOrder = 3,
                SystemName = NavigationItems.Grh,
                Items = new List<MenuItem>
                    {
                        new(selectedTab)
                        {
                            Icon = "fas fa-align-justify",
                            DisplayOrder = 1,
                            SystemName = NavigationItems.Departement,
                            Href = "/Departement"
                        },
                        new(selectedTab)
                        {
                            Icon = "fas fa-align-justify",
                            DisplayOrder = 2,
                            SystemName = NavigationItems.Employee,
                            Href = "/Employee"
                        }
                    }
            });
        }

        private async Task<MenuItem> BuildGescNavigationItemsAsync(int selectedTab)
        {
            return await Task.FromResult(new MenuItem(selectedTab)
            {
                Icon = "fas fa-dollar-sign",
                DisplayOrder = 3,
                SystemName = NavigationItems.Gesc,
                Items = new List<MenuItem>
                    {
                        new(selectedTab)
                        {
                            Icon = "fas fa-align-justify",
                            DisplayOrder = 1,
                            SystemName = NavigationItems.Product,
                            Href = "/Product"
                        } 
                    }
            });
        }

        private async Task<MenuItem> BuildAdministrationNavigationItemsAsync(int selectedTab)
        {
            return await Task.FromResult(new MenuItem(selectedTab)
            {
                Icon = "fas fa-user-cog",
                DisplayOrder = 4,
                SystemName = NavigationItems.Administration,
                Items = new List<MenuItem>
                {
                    new(selectedTab)
                    {
                        Icon = "fas fa-user-friends",
                        DisplayOrder = 1,
                        SystemName = NavigationItems.Users,
                            Href = "Admin/Home"
                    },
                    new(selectedTab)
                    {
                        Icon = "fas fa-user-tag",
                        DisplayOrder = 3,
                        SystemName = NavigationItems.UserRoles,
                            Href = "Admin/Home"
                    },
                    new(selectedTab)
                    {
                        Icon = "fas fa-users-cog",
                        DisplayOrder = 3,
                        SystemName = NavigationItems.Acl,
                            Href = "Admin/Home"
                    }
                }
            });
        } 
    }
}
