using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flow.Models;
using Flow.Models.Logs;

namespace Flow.ViewModels
{
    public class WorkstationKioskViewModel
    {
        public Department Department { get; set; }
        public Workstation Workstation { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public List<UnitLog> UnitLog { get; set; }

    }
}
