using iBillPrism.Abstractions;
using iBillPrism.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iBillPrism.Contracts
{
    public interface IRepository
    {
        Task Add<T>(T entity) where T : Entity;
        Task Update<T>(T entity) where T : Entity;
        Task Remove<T>(T entity) where T : Entity;
        Task<IEnumerable<T>> GetAll<T>() where T : Entity, new();
    }
}
