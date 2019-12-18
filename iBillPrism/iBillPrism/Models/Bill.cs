using System;
using System.Collections.Generic;
using System.Text;
using iBillPrism.Abstractions;
using SQLite;

namespace iBillPrism.Models
{
    public class Bill : Entity
    {        
        [Ignore]
        public BillType Type
        {
            get => _billType;
            set
            {
                _billType = value;
                BillTypeId = _billType.Id;
            }
        }

        // not to be used from code only for db-repository
        public int BillTypeId { get; set; }

        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? PayDate { get; set; }

        private BillType _billType;
    }
}
