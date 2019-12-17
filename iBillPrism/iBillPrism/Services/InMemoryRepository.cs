using iBillPrism.Abstractions;
using iBillPrism.Contracts;
using iBillPrism.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBillPrism.Services
{
    //class InMemoryRepository : IRepository
    //{
    //    public InMemoryRepository()
    //    {
    //        _listOfBills.Add(new Bill { Amount = 100, Type = "Energy Bill", DueDate = DateTime.Now.AddDays(20) });
    //        _listOfBills.Add(new Bill { Amount = 120, Type = "Telephone Bill", DueDate = DateTime.Now.AddDays(20) });
    //        _listOfBills.Add(new Bill { Amount = 50, Type = "Cellphone Bill", DueDate = DateTime.Now.AddDays(20) });
    //        _listOfBills.Add(new BillType { Type = "Energy Bill", IsCustom = false });
    //    }
    //    public Task Add<T>(T entity) where T : Entity
    //    {
    //        _listOfBills.Add(entity);

    //        return Task.CompletedTask;
    //    }
    //    public Task Update<T>(T entity) where T : Entity
    //    {
    //        Entity existing = _listOfBills.SingleOrDefault(x => x.Id == entity.Id);
    //        if (existing != null)
    //        {
    //            existing = entity;
    //        }

    //        return Task.CompletedTask;
    //    }
    //    public Task<IEnumerable<T>> GetAll<T>()
    //        where T : Entity, new()
    //    {
    //        return Task.FromResult<IEnumerable<Entity>>(_listOfBills);
    //    }
    //    public Task Remove<T>(T entity) where T : Entity
    //    {
    //        _listOfBills.Remove(entity);
    //        return Task.CompletedTask;
    //    }

    //    private List<Entity> _listOfBills = new List<Entity>();
    //}
}
