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
            _database.CreateTableAsync<BillType>().Wait();
            int customBills = _database.Table<BillType>().CountAsync(x => !x.IsCustom).Result;
            if (customBills == 0)
            {
                _database.InsertAllAsync(initialTable).Wait();
            }
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
