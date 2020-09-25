using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Flow.Models
{
    public class Plant
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public int Employees { get; set; }





    }
}
