using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flow.ViewComponents
{
    public class ClockViewComponent : ViewComponent
    {
        public ClockViewComponent()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}
