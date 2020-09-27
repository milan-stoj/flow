using Flow.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flow.ViewComponents
{
    public class QualityIssueViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public QualityIssueViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {

            return View();
        }

    }
}
