using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PracticeProject.Data;
using PracticeProject.Models;
using PracticeProject.Services.Interfaces;
using PracticeProject.Models.PracticeProjViewModel;
using System.Data;

using PracticeProject.Data.Entity;
using static PracticeProject.Models.PracticeProjViewModel.PracticeProjViewModel;

namespace PracticeProject.Services
{
    public class PracticeProjService : IPracticeProj
    {
        private readonly HomeDbContext _context;
        DataTable dt = new DataTable();
        MessageHelper mes = new MessageHelper();
        public PracticeProjService(HomeDbContext _context)
        {

            this._context = _context;

        }

        public async Task<MessageHelper> CreateItems(List<ItemsViewModel> objlist)
        {
            try
            {
                List<TblItem> newObjList = new List<TblItem>();
                foreach (var obj in objlist)
                {
                    var createmulti = _context.TblItems.Where(x => x.StrItemName == obj.StrItemName && x.IsActive == true)
                        .Select(a => a).FirstOrDefault();
                    if (createmulti != null)
                    {
                        throw new Exception($"{obj.StrItemName} Name Already Exits");
                    }
                    TblItem objNew = new TblItem()
                    {
                        NumStockQuantity = obj.NumStockQuantity,
                        StrItemName = obj.StrItemName,
                        IsActive = true
                    };
                   newObjList.Add(objNew);     
                }
                await _context.TblItems.AddRangeAsync(newObjList);
                await _context.SaveChangesAsync();
                mes.StatusCode = 200;
                mes.Message = "CREATE SUCCESSFULLY";
            }
            catch (Exception ex)
            {
                mes.Message = ex.Message;
                mes.StatusCode = 500;
            }
            return mes;
        }
        public async Task<List<GetItemsViewModel>> GetItems(long IntItemId)
        {
            var data = _context.TblItems.Where(x => x.IntItemId == IntItemId || IntItemId == 0)
                                            .Select(a => new GetItemsViewModel()
                                            {
                                                IntItemId = a.IntItemId,
                                                StrItemName = a.StrItemName,
                                                NumStockQuantity = a.NumStockQuantity,

                                            }
                                            ).ToList();

            return data;

        }
        public async Task<MessageHelper> EditItems(List<EditViewModel> objlist)
        {
            try
            {
                List<TblItem> newObjList = new List<TblItem>();
                foreach (var obj in objlist)
                {
                    var data = _context.TblItems.Where(x => x.IntItemId == obj.IntItemId)
                                                 .Select(a => a).FirstOrDefault();
                    data.StrItemName = obj.StrItemName;
                    data.NumStockQuantity = (long)obj.NumStockQuantity;
                    newObjList.Add(data);
                }
                _context.TblItems.UpdateRange(newObjList);
                await _context.SaveChangesAsync();
                mes.StatusCode = 200;
                mes.Message = "UPDATED SUCCESSFULLY";

            }
            
        
            catch (Exception ex)
            {
                mes.Message = ex.Message;
                mes.StatusCode = 500;
            }
            return mes;
        }

        public async Task<MessageHelper> CreatePartnerType(PartnerTypeViewModel obj)
        {
            try
            {
                
                    var create = _context.TblPartnerTypes.Where(x => x.StrPartnerTypeName == obj.StrPartnerTypeName && x.IsActive == true)
                        .Select(a => a).FirstOrDefault();


                if (create != null)
                {
                    //throw new Exception($"{obj.StrItemName} Name Already Exits");
                    mes.Message = "Already Exists";
                }
                else
                {
                    TblPartnerType objNewPartnerType = new TblPartnerType()
                    {
                        StrPartnerTypeName = obj.StrPartnerTypeName,
                        IsActive = true
                    };
                    await _context.TblPartnerTypes.AddRangeAsync(objNewPartnerType);
                    await _context.SaveChangesAsync();
                    mes.StatusCode = 200;
                    mes.Message = "CREATE SUCCESSFULLY";
                }
            }
            catch (Exception ex)
            {
                mes.Message = ex.Message;
                mes.StatusCode = 500;
            }
            return mes;
        }
        public async Task<MessageHelper> CreatePartner(PartnerViewModel obj)
        {
            try
            {
                TblPartner partner = new TblPartner
                {
                    StrPartnerName = obj.StrPartnerName,
                    IntPartnerTypeId = obj.IntPartnerTypeId,
                    IsActive = true
                };

                await _context.TblPartners.AddAsync(partner);
                await _context.SaveChangesAsync();

                mes.Message = "Creates Successful";
                mes.StatusCode = 200;
            }
            catch (Exception ex)
            {
                mes.Message = ex.Message;
                mes.StatusCode = 500;
            }

            return mes;
        }
        public async Task<MessageHelper> PurchaseItemFromSupplier(PurchaseViewModel obj)
        {
            try
            {
                List<TblPurchaseDetail> purchaseDetailsList = new List<TblPurchaseDetail>();

                foreach (var item in obj.PurchaseItemList)
                {
                    var quantity = _context.TblItems.Where(x => x.IntItemId == item.IntItemId).FirstOrDefault();

                    TblPurchaseDetail purchaseDetail = new TblPurchaseDetail
                    {
                        IntItemId = item.IntItemId,
                        IntPurchaseId = item.IntPurchaseId,
                        NumItemQuantity = item.NumItemQuantity,
                        NumUnitPrice = item.NumUnitPrice,
                        IsActive = true

                    };
                    if (quantity != null)
                    {
                        //throw new Exception($"{obj.StrItemName} Name Already Exits");
                        mes.Message = "Already Exists";
                        var row = _context.TblItems.Where(x => x.IntItemId == item.IntItemId).FirstOrDefault();
                        row.NumStockQuantity += item.NumItemQuantity;
                        _context.TblItems.Update(row);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {

                        TblItem objNew = new TblItem()
                        {
                            NumStockQuantity = item.NumItemQuantity,
                            StrItemName = item.StrItemName,
                            IsActive = true
                        };

                        await _context.TblItems.AddAsync(objNew);
                        await _context.SaveChangesAsync();
                    }
                    purchaseDetailsList.Add(purchaseDetail);

                }

                TblPurchase purchase = new TblPurchase
                {
                    IntSupplierId = obj.IntSupplierId,
                    DtePurchaseDate = obj.DtePurchaseDate,
                    IsActive = true
                };

                await _context.TblPurchases.AddAsync(purchase);
                await _context.SaveChangesAsync();


                await _context.TblPurchaseDetails.AddRangeAsync(purchaseDetailsList);
                await _context.SaveChangesAsync();

                mes.Message = "Purchased Successful";
                mes.StatusCode = 200;
            }
            catch (Exception ex)
            {
                mes.Message = ex.Message;
                mes.StatusCode = 500;
            }

            return mes;
        }
        public async Task<MessageHelper> SalesItem(SalesviewModel obj)
        {
            try
            {
                List<TblSalesDetail> salesDetailsList = new List<TblSalesDetail>();

                foreach (var item in obj.salesItemList)
                {
                    var quantity = (from i in _context.TblItems
                                    where i.IntItemId == item.IntItemId && i.IsActive == true
                                    select i.NumStockQuantity).FirstOrDefault();

                    TblSalesDetail salesDetail = new TblSalesDetail
                    {
                        IntSalesId = item.IntSalesId,
                        IntItemId = item.IntItemId,
                        IntItemQuantity = item.IntItemQuantity,
                        NumUnitPrice = item.NumUnitPrice,
                        IsActive = true
                    };
                    if (item.IntItemQuantity <= quantity)
                    {
                        salesDetailsList.Add(salesDetail);
                        var row = _context.TblItems.Where(x=> x.IntItemId==item.IntItemId).FirstOrDefault();
                        row.NumStockQuantity -= item.IntItemQuantity;
                        _context.TblItems.Update(row);
                        await _context.SaveChangesAsync();
                    }

                }
                TblSale sales = new TblSale
                {
                    IntCustomerId = obj.IntCustomerId,
                    DteSalesDate = obj.DteSalesDate,
                    IsActive = true
                };
                await _context.TblSales.AddAsync(sales);
                await _context.SaveChangesAsync();

                await _context.TblSalesDetails.AddRangeAsync(salesDetailsList);
                await _context.SaveChangesAsync();

                mes.Message = "Sales Successful";
                mes.StatusCode = 200;
            }
            catch (Exception ex)
            {
                mes.Message = ex.Message;
                mes.StatusCode = 500;
            }

            return mes;

        }
        
        public async Task<List<GetItemWiseDailyPurchaseViewModel>> GetItemWiseDailyPurchaseViewModel(DateTime dtePurchaseDate)
        {
            try
            {
                List<GetItemWiseDailyPurchaseViewModel> purchase = (from par in _context.TblPurchases
                                                                    join parDet in _context.TblPurchaseDetails on par.IntPurchaseId equals parDet.IntPurchaseId
                                                                    from it in _context.TblItems
                                                                    join pd in _context.TblPurchaseDetails on it.IntItemId equals pd.IntItemId
                                                                    where (par.DtePurchaseDate.Date ==dtePurchaseDate.Date)
                                                                    group new {par, parDet, it} by new {parDet.IntItemId, par.DtePurchaseDate, par.IntPurchaseId,it.StrItemName} into c
                                                                    select new GetItemWiseDailyPurchaseViewModel
                                                                    {
                                                                        IntItemId= c.Key.IntItemId,
                                                                        IntPurchaseId = c.Key.IntPurchaseId,
                                                                        DtePurchaseDate = c.Key.DtePurchaseDate,
                                                                        StrItemName = c.Key.StrItemName,
                                                                        PuchaseQuantity = c.Sum(x=> x.parDet.NumItemQuantity),
                                                                        UnitPrice = c.Sum(x=> x.parDet.NumUnitPrice)
                                                                    }).ToList();
                return purchase;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GetItemWiseMonthlySalesViewModel>> GetItemWiseMonthlySalesReport(DateTime dteSalesDate)
        {
            try
            {
                List<GetItemWiseMonthlySalesViewModel> salary = (from sal in _context.TblSales
                                                                 join salDet in _context.TblSalesDetails on sal.IntSalesId equals salDet.IntSalesId
                                                                 from it in _context.TblItems
                                                                 join sd in _context.TblSalesDetails on it.IntItemId equals sd.IntItemId
                                                                 where (sal.DteSalesDate.Date == dteSalesDate.Date)
                                                                 group new {sal, salDet, it} by new {salDet.IntItemId, sal.DteSalesDate, sal.IntSalesId, it.StrItemName} into d
                                                                 select new GetItemWiseMonthlySalesViewModel
                                                                 {
                                                                     IntItemId = d.Key.IntItemId,
                                                                     IntSalesId = d.Key.IntSalesId,
                                                                     DteSalesDate = d.Key.DteSalesDate,
                                                                     StrItemName = d.Key.StrItemName,
                                                                     IntSalesItemQuantity = (long)d.Sum(y => y.salDet.IntItemQuantity),
                                                                     UnitPrice = d.Sum(y => y.salDet.NumUnitPrice)
                                                                 } ).ToList();
                return salary;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GetItemWiseDailyPurchaseViewModel>> GetSupplierWiseDailyPurchaseReport(DateTime dtePurchaseDate)
        {
            try
            {
                List<GetItemWiseDailyPurchaseViewModel> purchase = (from par in _context.TblPurchases
                                                                    join parDet in _context.TblPurchaseDetails on par.IntPurchaseId equals parDet.IntPurchaseId
                                                                    from it in _context.TblItems
                                                                    join pd in _context.TblPurchaseDetails on it.IntItemId equals pd.IntItemId
                                                                    from pt in _context.TblPartners
                                                                    join pdd in _context.TblPartnerTypes on pt.IntPartnerTypeId equals pdd.IntPartnerTypeId
                                                                    where (par.DtePurchaseDate.Date == dtePurchaseDate.Date)
                                                                    group new { par, parDet, it,pt,pdd } by new { par.DtePurchaseDate, par.IntPurchaseId, it.StrItemName,pt.StrPartnerName } into c
                                                                    select new GetItemWiseDailyPurchaseViewModel
                                                                    {
                                                                        SupplierName =c.Key.StrPartnerName,
                                                                        DtePurchaseDate = c.Key.DtePurchaseDate,
                                                                        StrItemName = c.Key.StrItemName,
                                                                        PuchaseQuantity = c.Sum(x => x.parDet.NumItemQuantity),
                                                                        UnitPrice = c.Sum(x => x.parDet.NumUnitPrice)
                                                                    }).ToList();
                return purchase;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GetItemWiseMonthlySalesViewModel>> GetCustomerWiseMonthlySalesReport(DateTime dteSalesDate)
        {
            try
            {
                List<GetItemWiseMonthlySalesViewModel> salary = (from sal in _context.TblSales
                                                                 join salDet in _context.TblSalesDetails on sal.IntSalesId equals salDet.IntSalesId
                                                                 from it in _context.TblItems
                                                                 join sd in _context.TblSalesDetails on it.IntItemId equals sd.IntItemId
                                                                 from pt in _context.TblPartners
                                                                 join pdd in _context.TblPartnerTypes on pt.IntPartnerTypeId equals pdd.IntPartnerTypeId
                                                                 where (sal.DteSalesDate.Date == dteSalesDate.Date)
                                                                 group new { sal, salDet, it,pt,pdd } by new { salDet.IntItemId, sal.DteSalesDate, sal.IntSalesId, it.StrItemName, pt.StrPartnerName } into d
                                                                 select new GetItemWiseMonthlySalesViewModel
                                                                 {
                                                                     CustomerName = d.Key.StrPartnerName,
                                                                     DteSalesDate = d.Key.DteSalesDate,
                                                                     StrItemName = d.Key.StrItemName,
                                                                     IntSalesItemQuantity = (long)d.Sum(y => y.salDet.IntItemQuantity),
                                                                     UnitPrice = d.Sum(y => y.salDet.NumUnitPrice)
                                                                 }).ToList();
                return salary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<GetItemWiseDailyPurchaseVSSalesViewModel>> GetItemWiseDailyPuchaseVSSalesReport(DateTime dteDate)
        {
            try
            {

                List<GetItemWiseDailyPurchaseVSSalesViewModel> report= (from par in _context.TblPurchases
                                                                        join parDet in _context.TblPurchaseDetails on par.IntPurchaseId equals parDet.IntPurchaseId
                                                                        from it in _context.TblItems
                                                                        join pd in _context.TblPurchaseDetails on it.IntItemId equals pd.IntItemId
                                                                        from sal in _context.TblSales
                                                                        join salDet in _context.TblSalesDetails on sal.IntSalesId equals salDet.IntSalesId
                                                                        from itt in _context.TblItems
                                                                        join sd in _context.TblSalesDetails on itt.IntItemId equals sd.IntItemId
                                                                        where (par.DtePurchaseDate.Date == dteDate.Date || sal.DteSalesDate.Date == dteDate.Date)
                                                                        group new { par, parDet, it,sal, salDet, itt} by new { parDet.IntItemId, par.DtePurchaseDate, par.IntPurchaseId,it.StrItemName, sal.DteSalesDate, sal.IntSalesId } into c
                                                                        select new GetItemWiseDailyPurchaseVSSalesViewModel
                                                                        {
                                                                            IntItemId = c.Key.IntItemId,
                                                                            IntPurchaseId = c.Key.IntPurchaseId,
                                                                            DtePurchaseDate = c.Key.DtePurchaseDate,
                                                                            StrItemName = c.Key.StrItemName,
                                                                            IntSalesId = c.Key.IntSalesId,
                                                                            DteSalesDate = c.Key.DteSalesDate,
                                                                            PuchaseQuantity = c.Sum(x => x.parDet.NumItemQuantity),
                                                                            UnitPriceP = c.Sum(x => x.parDet.NumUnitPrice),
                                                                            IntSalesItemQuantity = (long)c.Sum(y => y.salDet.IntItemQuantity),
                                                                            UnitPriceS = c.Sum(y => y.salDet.NumUnitPrice)

                                                                        }
                                                                        ).ToList();
                return report;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public async Task<List<>>

    }






}
