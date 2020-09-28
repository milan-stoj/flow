using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flow.Models;

namespace Flow.ViewModels
{
    public class WorkstationKioskViewModel
    {
        public Department Department { get; set; }
        public Workstation Workstation { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
