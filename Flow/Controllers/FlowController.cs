﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flow.Data;
using Flow.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Flow.Controllers
{
    [Authorize(Roles = "Administrator, Supervisor, QA, MfgEngineer")]
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
                ActiveDepartments = _context.Departments.Count(),
                ActiveEmployees = _context.Users.Count(),
                ActiveWorkstations = _context.Workstations.Count(),
                ActiveEngineers = _context.Users.Where(u => u.UserRole == "QA" || u.UserRole == "MfgEngineer").Count(),
                ActiveSupervisors = _context.Users.Where(u => u.UserRole == "Supervisor").Count(),
                ActiveOperators = _context.Users.Where(u => u.UserRole == "Operator").Count(),
                UnitTypes = _context.UnitTypes.Count()
            };

            return View(flowIndexViewModel);
        }

        public IActionResult GetChartData()
        {
            var data = _context.Workstations
                .GroupBy(w => w.Department.Name)
                .Select(grp => new
                {
                    Department = grp.Key,
                    Count = grp.Count()
                })
                .OrderBy(o => o.Count)
                .ToList();
                        
            return Json(data);
        }

        //public IActionResult GetCompletionData()
        //{

        //}
    }
}
