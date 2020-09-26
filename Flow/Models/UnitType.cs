using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Flow.Models
{
    public class UnitType
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Prefix")]
        public string Prefix { get; set; }
        
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Work Instructions")]
        public string WorkInstructions { get; set; }

        [Display(Name = "Rate (minutes)")]
        public int StandardRate { get; set; } //Minutes
    }
}
