using Tsi.Template.Core.Abstractions;

namespace Tsi.Template.Domain.Common
{
    public class PermissionUserRoleMapping: BaseEntity, ICommonEntity
    {
        public int UserRoleId { get; set; }
        public int PermissionId { get; set; } 
    }
}
