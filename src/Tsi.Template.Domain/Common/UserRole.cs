using Tsi.Template.Core.Abstractions;

namespace Tsi.Template.Domain.Common
{
    public class UserRole : BaseEntity, ICommonEntity
    {
        public string SystemName { get; set; }
        public string Name { get; set; } 
    }
}
