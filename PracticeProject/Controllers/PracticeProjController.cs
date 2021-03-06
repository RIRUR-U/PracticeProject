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
        [HttpPost]
        [Route("PartnerTypeCreate")]
        public async Task<IActionResult> PartnerTypeCreate(PartnerTypeViewModel obj)
        {

            return Ok(await _practice.CreatePartnerType(obj));



        }
        [HttpPost]
        [Route("PartnerCreate")]
        public async Task<IActionResult> PartnerCreate(PartnerViewModel obj)
        {

            return Ok(await _practice.CreatePartner(obj));

        }
        [HttpPost]
        [Route("PurchaseItem")]
        public async Task<IActionResult> PurchaseItemFromSupplier(PurchaseViewModel obj)
        {

            return Ok(await _practice.PurchaseItemFromSupplier(obj));

        }
        [HttpPost]
        [Route("SalesItem")]
        public async Task<IActionResult> SalesItem(SalesviewModel obj)
        {

            return Ok(await _practice.SalesItem(obj));

        }

        [HttpPost]
        [Route("GetItemWiseDailyPurchaseViewModel")]
        public async Task<IActionResult> GetItemWiseDailyPurchaseViewModel(DateTime dtePurchaseDate)
        {

            return Ok(await _practice.GetItemWiseDailyPurchaseViewModel(dtePurchaseDate));

        }
        [HttpPost]
        [Route("GetItemWiseMonthlySalesReport")]
        public async Task<IActionResult> GetItemWiseMonthlySalesReport(DateTime dteSalesDate)
        {

            return Ok(await _practice.GetItemWiseMonthlySalesReport(dteSalesDate));

        }
        [HttpPost]
        [Route("GetSupplierWiseDailyPurchaseReport")]
        public async Task<IActionResult> GetSupplierWiseDailyPurchaseReport(DateTime dtePurchaseDate)
        {

            return Ok(await _practice.GetSupplierWiseDailyPurchaseReport(dtePurchaseDate));

        }
        [HttpPost]
        [Route("GetCustomerWiseMonthlySalesReport")]
        public async Task<IActionResult> GetCustomerWiseMonthlySalesReport(DateTime dteSalesDate)
        {

            return Ok(await _practice.GetCustomerWiseMonthlySalesReport(dteSalesDate));

        }
        [HttpPost]
        [Route("GetItemWiseDailyPuchaseVSSalesReport")]
        public async Task<IActionResult> GetItemWiseDailyPuchaseVSSalesReport(DateTime dteDate)
        {

            return Ok(await _practice.GetItemWiseDailyPuchaseVSSalesReport(dteDate));

        }
    }
}
