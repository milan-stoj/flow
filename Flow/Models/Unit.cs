using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Flow.Models
{
    public class Unit
    {
        [Key]
        public int ID { get; set; }

        public string UnitNumber { get; set; } // Scanned in as QR string with gun.

        [ForeignKey("UnitType")]
        public int UnitTypeID { get; set; }

        public virtual UnitType UnitType { get; set; }
    }
}
