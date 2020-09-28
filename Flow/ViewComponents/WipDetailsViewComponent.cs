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
    public class WipDetailsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public WipDetailsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(Workstation workstation)
        {
            string view = "Default";
            var model = workstation.CurrentUnit;
            if(model == null)
            {
                view = "Standby";
            }
            return View(view, model);
        }
    }
}
