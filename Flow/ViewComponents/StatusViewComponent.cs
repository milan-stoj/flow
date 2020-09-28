using Flow.Data;
using Flow.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Flow.ViewComponents
{
    public class StatusViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public StatusViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(Workstation workstation)
        {
            if(workstation.CurrentUnit == null)
            {
                return View("Standby");
            } 

            if(workstation.CurrentUnit.QA == true)
            {
                return View("Hold");
            }
            return View("WIP");
        }

    }
}
