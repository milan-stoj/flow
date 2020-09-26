using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flow.Data;
using Flow.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Flow.Controllers
{
    public class WorkstationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkstationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WorkstationsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: WorkstationsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WorkstationsController/Create
        public IActionResult Create(int? id)
        {
            Department department = _context.Department.Single(d => d.ID == id);
            Workstation workstation = new Workstation();
            workstation.Department = department;
            workstation.DepartmentID = department.ID;
            return View(workstation);
        }

        // POST: WorkstationsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID, Code, Name, Online, Department, DepartmentID")] Workstation workstation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workstation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Departments", new { id = workstation.DepartmentID });
            }
            return View(workstation);
        }

        // GET: WorkstationsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WorkstationsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WorkstationsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WorkstationsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
