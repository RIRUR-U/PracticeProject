using System;
using System.Collections.Generic;

namespace PracticeProject.Data.Entity
{
    public partial class TblSalesDetail
    {
        public long IntDetailsId { get; set; }
        public long IntSalesId { get; set; }
        public long IntItemId { get; set; }
        public long? IntItemQuantity { get; set; }
        public long NumUnitPrice { get; set; }
        public bool? IsActive { get; set; }
    }
}
