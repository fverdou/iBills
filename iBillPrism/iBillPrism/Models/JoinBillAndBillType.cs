using System;
using System.Collections.Generic;
using System.Text;

namespace iBillPrism.Models
{
    class JoinBillAndBillType
    {
        public int Id { get; set; }
        public int BillTypeId { get; set; }

        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? PayDate { get; set; }

        public string Description { get; set; }
        public bool IsCustom { get; set; }
    }
}
