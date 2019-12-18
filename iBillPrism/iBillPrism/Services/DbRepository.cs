using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using iBillPrism.Contracts;
using iBillPrism.Models;
using SQLite;

namespace iBillPrism.Services
{
    public class DbRepository : IRepository
    {
        static readonly IEnumerable<BillType> initialTable = new[] {
                    new BillType
                    {
                        IsCustom=false,
                        Type="Energy Bill"
                    },
                    new BillType
                    {
                        IsCustom=false,
                        Type="Gas Bill"
                    },
                    new BillType
                    {
                        IsCustom=false,
                        Type="Telephone Bill"
                    },
                    new BillType
                    {
                        IsCustom=false,
                        Type="Cellphone Bill"
                    },
                    new BillType
                    {
                        IsCustom=false,
                        Type="Loan Bill"
                    },
                };
        public DbRepository(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Bill>().Wait();
            int customBills = _database.Table<BillType>().CountAsync(x => !x.IsCustom).Result;
            if (customBills == 0)
            {
                _database.InsertAllAsync(initialTable).Wait();
            }
        }

        readonly SQLiteAsyncConnection _database;

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
            return await _database.Table<Bill>().ToListAsync();
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
