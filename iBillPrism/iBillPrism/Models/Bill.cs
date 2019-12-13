using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace iBillPrism.Models
{
    public class Bill
    {
        [PrimaryKey, AutoIncrement] 
        public int Id { get; set; }

        public string Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? PayDate { get; set; }

    }
}
