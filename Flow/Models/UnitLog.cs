﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Flow.Models.Logs
{
    public class UnitLog
    {
        [Key]
        public int ID { get; set; }
        public string Event { get; set;}
        public DateTime EventDate { get; set; }
        public int CompletionTime { get; set; }
        public int Efficiency { get; set; }

        [ForeignKey("Workstation")]
        public int WorkstationID { get; set; }
        public virtual Workstation Workstation { get; set; }
        
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("Unit")]
        public int UnitID { get; set; }
        public virtual Unit Unit { get; set; }

    }
}
