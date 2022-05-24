using Microsoft.AspNetCore.Mvc;
using PracticeProject.Services.Interfaces;
using static PracticeProject.Models.PracticeProjViewModel.PracticeProjViewModel;

namespace PracticeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PracticeProjController : ControllerBase
    {
        private readonly IPracticeProj _practice;
        public PracticeProjController(IPracticeProj _practice)
        {
            this._practice = _practice;
        }
        [HttpPost]
        [Route("CreateItems")]
        public async Task<IActionResult> CreateItems(List<ItemsViewModel> obj)
        {

            return Ok(await _practice.CreateItems(obj));

        }

        [HttpGet]
        [Route("GetItems")]
        public async Task<List<GetItemsViewModel>> GetItems(long IntItemId)
        {
            return await _practice.GetItems(IntItemId);

        }

    }
}
