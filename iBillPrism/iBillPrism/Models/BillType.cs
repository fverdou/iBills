using System;
using System.Collections.Generic;
using System.Text;
using iBillPrism.Abstractions;
using SQLite;

namespace iBillPrism.Models
{
    public class BillType : Entity, IEquatable<Entity>
    {
        public string Description { get; set; }
        public bool IsCustom { get; set; }

        public bool Equals(Entity other)
        {
            return ReferenceEquals(this, other) || this.Id == other?.Id;
        }

        public override bool Equals(object obj)
        {
            return Equals((Entity)obj);
        }

        public override int GetHashCode()
        {
            return this.Id;
        }
    }
}
