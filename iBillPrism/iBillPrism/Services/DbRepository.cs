using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iBillPrism.Contracts;
using iBillPrism.Models;
using SQLite;

namespace iBillPrism.Services
{
    public class DbRepository : IRepository
    {
        readonly SQLiteAsyncConnection _database;

        class JoinBillAndBillType // Information Hiding!
        {
            public int Id { get; set; }
            public int BillTypeId { get; set; }

            public decimal Amount { get; set; }
            public DateTime DueDate { get; set; }
            public DateTime? PayDate { get; set; }

            public string Description { get; set; }
            public bool IsCustom { get; set; }
        }

        static readonly IEnumerable<BillType> initialTable = new[] {
                    new BillType
                    {
                        IsCustom=false,
                        Description="Energy Bill"
                    },
                    new BillType
                    {
                        IsCustom=false,
                        Description="Gas Bill"
                    },
                    new BillType
                    {
                        IsCustom=false,
                        Description="Telephone Bill"
                    },
                    new BillType
                    {
                        IsCustom=false,
                        Description="Cellphone Bill"
                    },
                    new BillType
                    {
                        IsCustom=false,
                        Description="Loan Bill"
                    },
                };
        public DbRepository(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);

            _database.CreateTableAsync<Bill>().Wait();
            _database.CreateTableAsync<BillType>().Wait();

            int customBills = _database.Table<BillType>().CountAsync(x => !x.IsCustom).Result;
            if (customBills == 0)
            {
                _database.InsertAllAsync(initialTable).Wait();
            }
        }

        public Task AddBill(Bill bill)
        {
            return _database.InsertAsync(bill);
        }
        public Task UpdateBill(Bill bill)
        {
            return _database.UpdateAsync(bill);
        }
        public Task RemoveBill(Bill bill)
        {
            return _database.DeleteAsync(bill);
        }
        public async Task<IEnumerable<Bill>> GetAllBills()
        {
            //var bills = await _database.Table<Bill>().ToListAsync();
            //foreach (var bill in bills)
            //{
            //    bill.Type = await _database.Table<BillType>().Where(x => x.Id == bill.BillTypeId).FirstAsync();
            //}

            //return bills;

            var result = await _database.QueryAsync<JoinBillAndBillType>("SELECT b.Id, b.BillTypeId, b.Amount, b.DueDate, b.PayDate, bt.Description, bt.IsCustom FROM [Bill] as b INNER JOIN [BillType] as bt ON b.BillTypeId = bt.Id");
            return result.Select(x => new Bill
            {
                Id = x.Id,
                Amount = x.Amount,
                DueDate = x.DueDate,
                PayDate = x.PayDate,
                Type = new BillType
                {
                    Id = x.BillTypeId,
                    Description = x.Description,
                    IsCustom = x.IsCustom
                }
            }).ToList();
        }

        public Task AddBillType(BillType billtype)
        {
            return _database.InsertAsync(billtype);
        }

        public Task UpdateBillType(BillType billtype)
        {
            return _database.UpdateAsync(billtype);
        }

        public Task RemoveBillType(BillType billtype)
        {
            return _database.DeleteAsync(billtype);
        }

        public async Task<IEnumerable<BillType>> GetAllBillTypes()
        {
            return await _database.Table<BillType>().ToListAsync();
        }
    }
}
