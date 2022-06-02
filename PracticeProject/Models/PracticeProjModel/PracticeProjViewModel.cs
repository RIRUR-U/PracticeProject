namespace PracticeProject.Models.PracticeProjViewModel
{
    public class PracticeProjViewModel
    {
        public class ItemsViewModel
        {
            public long IntItemId { get; set; }
            public string? StrItemName { get; set; }
            public long NumStockQuantity { get; set; }
            public bool? IsActive { get; set; }
        }
        public class GetItemsViewModel
        {
            public long IntItemId { get; set; }
            public string? StrItemName { get; set; }
            public long? NumStockQuantity { get; set; }
        }
        public class EditViewModel
        {
            public long IntItemId { get; set; }
            public string? StrItemName { get; set; }
            public long? NumStockQuantity { get; set; }
        }
        public class PartnerViewModel
        {
            public long IntPartnerId { get; set; }
            public string StrPartnerName { get; set; } = null!;
            public long IntPartnerTypeId { get; set; }
            public bool? IsActive { get; set; }
        }
        public class PartnerTypeViewModel
        {
            public long IntPartnerTypeId { get; set; }
            public string StrPartnerTypeName { get; set; } = null!;
            public bool? IsActive { get; set; }
        }
        public class PurchaseViewModel
        {
            public long IntPurchaseId { get; set; }
            public long IntSupplierId { get; set; }
            public DateTime DtePurchaseDate { get; set; }
            public bool? IsActive { get; set; }
            public List<PurchaseDetailsViewModel> PurchaseItemList { get; set; }
        }
        public class PurchaseDetailsViewModel
        {
            
            public long IntDetailsId { get; set; }
            public long IntPurchaseId { get; set; }
            public long IntItemId { get; set; }
            public long NumItemQuantity { get; set; }
            public long NumUnitPrice { get; set; }
            public string? StrItemName { get; set; }
            public bool? IsActive { get; set; }
        }
        public class SalesviewModel
        {
            public long IntSalesId { get; set; }
            public long IntCustomerId { get; set; }
            public DateTime DteSalesDate { get; set; }
            public bool? IsActive { get; set; }
            public List<SalesDetailsViewModel> salesItemList { get; set; }
        }

        public class SalesDetailsViewModel
        {
            public long IntDetailsId { get; set; }
            public long IntSalesId { get; set; }
            public long IntItemId { get; set; }
            public long IntItemQuantity { get; set; }
            public long NumUnitPrice { get; set; }
            public bool? IsActive { get; set; }
        }

        public class GetItemWiseDailyPurchaseViewModel
        {
            public long IntPurchaseId { get; set; }
            public long IntSupplierId { get; set; }
            //public string StrSupplierName { get; set; }
            public long PuchaseQuantity { get; set; }
            public DateTime DtePurchaseDate { get; set; }
            public long IntItemId { get; set; }
            public string? StrItemName { get; set; }
            public long UnitPrice { get; set; }
            public bool? IsActive { get; set; }
            PurchaseDetailsViewModel? purchaseDetails { get; set; }
        }
        public class GetItemWiseMonthlySalesViewModel
        {
            public long IntSalesId { get; set; }
            public long IntItemId { get; set; }
            public long IntCustomerId { get; set; }
            public string? StrItemName { get; set; }
            public long IntSalesItemQuantity { get; set; }
            public long UnitPrice { get; set; }
            public DateTime DteSalesDate { get; set; }
            public bool? IsActive { get; set; }
            SalesDetailsViewModel? salesdetailes { get; set; }
        }
    }
}
