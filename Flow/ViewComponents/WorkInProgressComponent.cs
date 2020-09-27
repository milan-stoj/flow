using Flow.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flow.ViewComponents
{
   public class WorkInProgressViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public WorkInProgressViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {

            return View();
        }

    }
}
