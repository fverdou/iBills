using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using iBillPrism.Abstractions;
using iBillPrism.Contracts;
using iBillPrism.Models;
using SQLite;

namespace iBillPrism.Services
{
    public class DbRepository : IRepository
    {
        //readonly SQLiteAsyncConnection _database;
        public DbRepository(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);            
            
            _database.CreateTableAsync<Bill>().Wait();
            _database.CreateTableAsync<BillType>().Wait();
        }

        readonly SQLiteAsyncConnection _database;

        public Task Add<T>(T entity) where T : Entity
        {
            return _database.InsertAsync(entity);
        }
        public Task Update<T>(T entity) where T : Entity
        {
            return _database.UpdateAsync(entity);
        }
        public Task Remove<T>(T entity) where T : Entity
        {
            return _database.DeleteAsync(entity);
        }
        public async Task<IEnumerable<T>> GetAll<T>()
            where T : Entity, new()
        {
            return await _database.Table<T>().ToListAsync();
        }
        
    }
}
