using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flow.Data;
using Flow.ViewModels;
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
            FlowIndexViewModel flowIndexViewModel = new FlowIndexViewModel()
            {
                ActiveDepartments = _context.Department.Count(),
                ActiveEmployees = _context.Users.Count(),
                //ActiveWorkstations = _context.Workstation.Count();
                ActiveEngineers = _context.Users.Where(u => u.UserRole == "QA" || u.UserRole == "MfgEngineer").Count(),
                ActiveSupervisors= _context.Users.Where(u => u.UserRole == "Supervisor").Count(),
                ActiveOperators= _context.Users.Where(u => u.UserRole == "Operator").Count(),
            };

            return View(flowIndexViewModel);
        }
    }
}
