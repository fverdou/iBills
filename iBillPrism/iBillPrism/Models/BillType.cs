using System;
using System.Collections.Generic;
using System.Text;
using iBillPrism.Abstractions;
using SQLite;

namespace iBillPrism.Models
{
    public class BillType : Entity
    {
        public string Type { get; set; }
        public bool IsCustom { get; set; }
    }
}
