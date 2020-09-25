using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flow.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Flow.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class FlowController : Controller
    {
        private readonly ApplicationDbContext _context;
        public FlowController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
