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
                    await _context.TblItems.AddRangeAsync(newObjList);
                    await _context.SaveChangesAsync();


                }
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
        public async Task<MessageHelper> SalesItem(SalesviewModel obj)
        {
            try
            {
                List<TblSalesDetail> salesDetailsList = new List<TblSalesDetail>();

                foreach (var item in obj.salesItemList)
                {
                    TblSalesDetail salesDetail = new TblSalesDetail
                    {
                        IntSalesId = item.IntSalesId,
                        IntItemId = item.IntItemId,
                        IntItemQuantity = item.IntItemQuantity,
                        NumUnitPrice = item.NumUnitPrice,
                        IsActive = true
                    };

                    var quantity = (from i in _context.TblItems
                                    where i.IntItemId == item.IntItemId && i.IsActive == true
                                    select i.NumStockQuantity).FirstOrDefault();

                    if (item.IntItemQuantity < quantity)
                    {
                        salesDetailsList.Add(salesDetail);
                    }

                }
                TblSale sales = new TblSale
                {
                    IntCustomerId = obj.IntCustomerId,
                    DteSalesDate = obj.DteSalesDate,
                    IsActive = true
                };
                await _context.TblSales.AddRangeAsync(sales);
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
        public async Task<MessageHelper> PurchaseItem(PurchaseViewModel obj)
        {
            try
            {
                List<TblPurchaseDetail> purchaseDetailsList = new List<TblPurchaseDetail>();

                foreach (var item in obj.PurchaseItemList)
                {
                    TblPurchaseDetail purchaseDetail = new TblPurchaseDetail
                    {
                        IntItemId = item.IntItemId,
                        IntPurchaseId = item.IntPurchaseId,
                        NumItemQuantity = item.NumItemQuantity,
                        NumUnitPrice = item.NumUnitPrice,
                        IsActive = true
                    };

                    var quantity = from i in _context.TblItems
                                   where i.IntItemId == item.IntItemId && i.IsActive == true
                                   select new
                                   {
                                       itemQuantity = i.NumStockQuantity
                                   };
                    

                }

                TblPurchase purchase = new TblPurchase
                {
                    IntSupplierId = obj.IntSupplierId,
                    DtePurchaseDate = obj.DtePurchaseDate,
                    IsActive = true
                };

                await _context.TblPurchases.AddRangeAsync(purchase);
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

    }
        



        
    
}
