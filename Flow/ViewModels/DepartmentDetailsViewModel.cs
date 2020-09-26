using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flow.Models;

namespace Flow.ViewModels
{
    public class DepartmentDetailsViewModel
    {
        public Department Department { get; set; }

        public IEnumerable<Workstation> Workstations { get; set; }




    }
}
