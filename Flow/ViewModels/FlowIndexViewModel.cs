using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flow.ViewModels
{
    public class FlowIndexViewModel
    {
        public int ActivePlants { get; set; }
        public int ActiveEmployees { get; set; }
        public int ActiveSupervisors { get; set; }
        public int ActiveEngineers { get; set; }
        public int ActiveOperators { get; set; }
        public int ActiveDepartments { get; set; }
        public int ActiveWorkstations { get; set; }
        public int UnitTypes { get; set; }
    }
}
