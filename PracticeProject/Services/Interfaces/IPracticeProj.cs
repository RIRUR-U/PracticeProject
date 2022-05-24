using PracticeProject.Models;
using PracticeProject.Models.PracticeProjViewModel;
using static PracticeProject.Models.PracticeProjViewModel.PracticeProjViewModel;

namespace PracticeProject.Services.Interfaces
{
    public interface IPracticeProj
    {
        Task<MessageHelper> CreateItems(List<ItemsViewModel> objlist);
        Task<List<GetItemsViewModel>> GetItems(long IntItemId);
    }
}
