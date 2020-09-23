using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Flow.Models
{
    public class Department
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Plant")]
        public int PlantID { get; set; }
        public virtual Plant Plant { get; set; }
    }
}
