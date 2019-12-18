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
    class InMemoryRepository : IRepository
    {
        public InMemoryRepository()
        {
            BillType billType1 = new BillType { Type = "Energy Bill", IsCustom = true };
            BillType billType2 = new BillType { Type = "Telephone Bill", IsCustom = true };
            BillType billType3 = new BillType { Type = "Cellphone Bill", IsCustom = true };
            _listOfBills.Add(new Bill { Amount = 100, Type = billType1, DueDate = DateTime.Now.AddDays(20) });
            _listOfBills.Add(new Bill { Amount = 120, Type = billType2, DueDate = DateTime.Now.AddDays(20) });
            _listOfBills.Add(new Bill { Amount = 50, Type = billType3, DueDate = DateTime.Now.AddDays(20) });
            _listOfBillTypes.Add(new BillType { Type = "Energy Bill", IsCustom = false });
            _listOfBillTypes.Add(new BillType { Type = "Gas Bill", IsCustom = false });
            _listOfBillTypes.Add(new BillType { Type = "Telephone Bill", IsCustom = false });
            _listOfBillTypes.Add(new BillType { Type = "Cellphone Bill", IsCustom = false });
            _listOfBillTypes.Add(new BillType { Type = "Loan Bill", IsCustom = false });

        }
        public Task AddBill(Bill bill)
        {
            _listOfBills.Add(bill);

            return Task.CompletedTask;
        }
        public Task UpdateBill(Bill bill)
        {
            Entity existing = _listOfBills.SingleOrDefault(x => x.Id == bill.Id);
            if (existing != null)
            {
                existing = bill;
            }

            return Task.CompletedTask;
        }
        public Task RemoveBill(Bill bill)
        {
            _listOfBills.Remove(bill);
            return Task.CompletedTask;
        }
        public Task<IEnumerable<Bill>> GetAllBills()
        {
            return Task.FromResult<IEnumerable<Bill>>(_listOfBills);
        }

        public Task AddBillType(BillType billtype)
        {
            _listOfBillTypes.Add(billtype);

            return Task.CompletedTask;
        }

        public Task UpdateBillType(BillType billtype)
        {
            Entity existing = _listOfBillTypes.SingleOrDefault(x => x.Id == billtype.Id);
            if (existing != null)
            {
                existing = billtype;
            }

            return Task.CompletedTask;
        }

        public Task RemoveBillType(BillType billtype)
        {
            _listOfBillTypes.Remove(billtype);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<BillType>> GetAllBillTypes()
        {
            return Task.FromResult<IEnumerable<BillType>>(_listOfBillTypes);
        }

        private List<Bill> _listOfBills = new List<Bill>();
        private List<BillType> _listOfBillTypes = new List<BillType>();
    }
}
