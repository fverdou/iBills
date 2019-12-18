using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace iBillPrism.Abstractions
{
    public class Entity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }       
       
    }
}
