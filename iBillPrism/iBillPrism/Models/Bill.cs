﻿using System;
using System.Collections.Generic;
using System.Text;
using iBillPrism.Abstractions;
using SQLite;

namespace iBillPrism.Models
{
    public class Bill : Entity
    {
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? PayDate { get; set; }
    }
}
