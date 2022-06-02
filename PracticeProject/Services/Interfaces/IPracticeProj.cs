using PracticeProject.Models;
using PracticeProject.Models.PracticeProjViewModel;
using static PracticeProject.Models.PracticeProjViewModel.PracticeProjViewModel;

namespace PracticeProject.Services.Interfaces
{
    public interface IPracticeProj
    {
        Task<MessageHelper> CreateItems(List<ItemsViewModel> objlist);
        Task<List<GetItemsViewModel>> GetItems(long IntItemId);
        Task<MessageHelper> CreatePartnerType(PartnerTypeViewModel obj);
        Task<MessageHelper> CreatePartner(PartnerViewModel obj);
        Task<MessageHelper> PurchaseItemFromSupplier(PurchaseViewModel obj);
        Task<MessageHelper> SalesItem(SalesviewModel obj);
        Task<List<GetItemWiseDailyPurchaseViewModel>> GetItemWiseDailyPurchaseViewModel(DateTime dtePurchaseDate);
        Task<List<GetItemWiseMonthlySalesViewModel>> GetItemWiseMonthlySalesReport(DateTime dteSalesDate);
        Task<List<GetItemWiseDailyPurchaseViewModel>> GetSupplierWiseDailyPurchaseReport(DateTime dtePurchaseDate);

    }
}
