using iBillPrism.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iBillPrism.Contracts
{
    public interface IRepository
    {
        Task Add(Bill bill);
        Task Update(Bill bill);
        Task Remove(Bill bill);
        Task<IEnumerable<Bill>> GetAll();
    }
}
