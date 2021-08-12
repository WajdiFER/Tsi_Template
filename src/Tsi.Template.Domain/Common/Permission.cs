using Tsi.Template.Core.Abstractions;

namespace Tsi.Template.Domain.Common
{
    public class Permission: BaseEntity, ICommonEntity
    { 
        public string Name { get; set; } 

        public string SystemName { get; set; }
    }
}
