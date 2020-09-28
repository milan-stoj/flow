using Flow.Data;
using Flow.Models;
using Flow.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Flow.ViewComponents
{
    public class WorkstationControlsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public WorkstationControlsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(WorkstationKioskViewModel model)
        {
            if(model.Workstation.CurrentUnit == null)
            {
                return View("Standby");
            } 

            if(model.Workstation.CurrentUnit.QA == true)
            {
                return View("Hold");
            }
            return View("WIP");
        }

    }
}
