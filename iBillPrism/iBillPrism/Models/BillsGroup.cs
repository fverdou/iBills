using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace iBillPrism.Models
{
    public class BillsGroup : List<Bill>
    {
        public DateTime Heading { get; }
                
        public BillsGroup(DateTime dateTime, List<Bill> bills)
            : base(bills)
        {
            Heading = dateTime;
        }
    }
}
