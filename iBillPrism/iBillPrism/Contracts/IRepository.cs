using iBillPrism.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iBillPrism.Contracts
{
    public interface IRepository
    {
        Task AddBill(Bill bill);
        Task UpdateBill(Bill bill);
        Task RemoveBill(Bill bill);
        Task<IEnumerable<Bill>> GetAllBills();

        Task AddBillType(BillType billtype);
        Task UpdateBillType(BillType billtype);
        Task RemoveBillType(BillType billtype);
        Task<IEnumerable<BillType>> GetAllBillTypes();
    }
}
