using System;
using Tsi.Template.Core.Abstractions;

namespace Tsi.Template.Domain.Gesc.Catalog
{
    public class Product : BaseEntity, ISoftDelete
    {   
        public string Code { get; set; }
        public string Libelle { get; set; }
        public decimal Price { get; set; }

         
        #region ISoftDelete
        public bool Deleted { get; set; }
        public DateTime DeletedAt { get; set; }
        #endregion
    }
}
