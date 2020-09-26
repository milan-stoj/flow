using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Flow.Models
{
    public class Workstation
    {
        [Key]
        public int ID { get; set; }

        public int Code { get; set; }

        public string Name { get; set; }

        public bool Online { get; set; }


        [ForeignKey("Department")]
        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }

    }
}
