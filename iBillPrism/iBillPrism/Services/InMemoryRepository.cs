using iBillPrism.Contracts;
using iBillPrism.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBillPrism.Services
{
    class InMemoryRepository : IRepository
    {
        public InMemoryRepository()
        {
            _listOfBills.Add(new Bill { Amount = 100, Type = "Energy Bill", DueDate = DateTime.Now.AddDays(20) });
            _listOfBills.Add(new Bill { Amount = 120, Type = "Telephone Bill", DueDate = DateTime.Now.AddDays(20) });
            _listOfBills.Add(new Bill { Amount = 50, Type = "Cellphone Bill", DueDate = DateTime.Now.AddDays(20) });
        }
        public Task Add(Bill bill)
        {
            _listOfBills.Add(bill);

            return Task.CompletedTask;
        }

        public Task Update(Bill bill)
        {
            Bill existing = _listOfBills.SingleOrDefault(x => x.Id == bill.Id);
            if (existing != null)
            {
                existing = bill;
            }

            return Task.CompletedTask;
        }

        public Task<IEnumerable<Bill>> GetAll()
        {            
            return Task.FromResult<IEnumerable<Bill>>(_listOfBills);
        }

        public Task Remove(Bill bill)
        {
            _listOfBills.Remove(bill);
            return Task.CompletedTask;
        }

        private List<Bill> _listOfBills = new List<Bill>();
    }
}
