using Tsi.Template.Core.Abstractions;

namespace Tsi.Template.Domain.Common
{
    public partial class Setting : BaseEntity, ICommonEntity 
    {
        public Setting()
        {
        }

        public Setting(string name, string value)
        {
            Name = name;
            Value = value; 
        }

        public string Name { get; set; }

        public string Value { get; set; } 

        public override string ToString()
        {
            return Name;
        }
    }
}
