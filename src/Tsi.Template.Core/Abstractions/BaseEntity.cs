using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsi.Template.Core.Abstractions
{
    /// <summary>
    /// Represents the base class for entities
    /// </summary>
    public abstract class BaseEntity 
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public int Id { get; set; }

    }
}
