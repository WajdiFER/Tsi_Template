using System;

namespace Tsi.Template.Core.Abstractions
{
    /// <summary>
    /// Used to mark an entity as soft delete
    /// Soft delete means the data will not be deleted from the database,
    /// but will be flaged as deleted
    /// </summary>
    public interface ISoftDelete
    {
        bool Deleted { get; set; }
        DateTime DeletedAt { get; set; }
    }
}
