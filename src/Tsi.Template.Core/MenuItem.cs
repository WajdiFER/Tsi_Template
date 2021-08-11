using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Tsi.Template.Core.Enums;

namespace Tsi.Template.Core
{
    public class MenuItem
    {
        public bool Active => ((NavigationItems)_selectedTab & SystemName) != 0;
        public int DisplayOrder { get; set; }
        public NavigationItems SystemName { get; set; }
        public string Name => GetDescription();
        public string Icon { get; set; }
        public IList<MenuItem> Items { get; set; }

        private readonly int _selectedTab;

        public string Href { get; set; }


        public MenuItem(int selectedTab)
        {
            Items = new List<MenuItem>();
            _selectedTab = selectedTab;
        }

        private string GetDescription()
        {
            var enumType = typeof(NavigationItems);

            var memberInfos = enumType.GetMember(SystemName.ToString());

            var enumValueMemberInfo = memberInfos.FirstOrDefault(m => m.DeclaringType == enumType);

            var valueAttribute = enumValueMemberInfo?.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return valueAttribute?.Length > 0 ? ((DescriptionAttribute)valueAttribute[0]).Description : SystemName.ToString();
        }
    }
}
