using System;
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

        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }

        [ForeignKey("Workstation")]
        public int MyProperty { get; set; }
        public virtual Workstation Workstation { get; set; }
        
        [ForeignKey("ApplicationUser")]
        public int ApplicationUserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
