﻿using Microsoft.Data.SqlClient;
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
    public class PracticeProjService: IPracticeProj
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
                    var createmulti = _context.TblItems.Where(x => x.StrItemName == obj.StrItemName && x.IsActive==true)
                        .Select(a => a).FirstOrDefault();
                    if (createmulti != null)
                    {
                        throw new Exception($"{obj.StrItemName} Name Already Exits");
                    }
                    TblItem objNew = new TblItem()
                    {
                        NumStockQuantity = obj.NumStockQuantity,
                        StrItemName = obj.StrItemName,
                        IsActive= true
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


    }
    
}
