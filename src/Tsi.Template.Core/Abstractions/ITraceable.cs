using System;

namespace Tsi.Template.Core.Abstractions
{
    public interface ITraceable
    {
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
        string CreatedBy { get; set; }
        string UpdatedBy { get; set; }
    }
}
