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
        public DbRepository(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Bill>().Wait();
        }

        readonly SQLiteAsyncConnection _database;

        public Task Add(Bill bill)
        {
            return _database.InsertAsync(bill);
        }

        public Task Update(Bill bill)
        {
            return _database.UpdateAsync(bill);
        }

        public Task Remove(Bill bill)
        {
            return _database.DeleteAsync(bill);
        }

        public async Task<IEnumerable<Bill>> GetAll()
        {
            return await _database.Table<Bill>().ToListAsync();
        }
    }
}
