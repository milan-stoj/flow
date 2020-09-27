﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Flow.Data;
using Flow.Models;

namespace Flow.Controllers
{
    public class WorkstationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkstationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Workstations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Workstations.Include(w => w.Department);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Workstations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workstation = await _context.Workstations
                .Include(w => w.Department)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (workstation == null)
            {
                return NotFound();
            }

            return View(workstation);
        }

        // GET: Workstations/Create
        [HttpGet("Workstations/Create")]
        public IActionResult Create(int id)
        {
            ViewData["DepartmentID"] = id;
            
            return View();
        }

        // POST: Workstations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Code,Name,Online,QA,DepartmentID")] Workstation workstation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workstation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Departments", new { id = workstation.DepartmentID });

            }
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "ID", "ID", workstation.DepartmentID);
            return View(workstation);
        }

        // GET: Workstations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workstation = await _context.Workstations.FindAsync(id);
            if (workstation == null)
            {
                return NotFound();
            }
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "ID", "ID", workstation.DepartmentID);
            return View(workstation);
        }

        // POST: Workstations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Code,Name,Online,QA,DepartmentID")] Workstation workstation)
        {
            if (id != workstation.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workstation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkstationExists(workstation.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "ID", "ID", workstation.DepartmentID);
            return View(workstation);
        }

        // GET: Workstations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workstation = await _context.Workstations
                .Include(w => w.Department)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (workstation == null)
            {
                return NotFound();
            }

            return View(workstation);
        }

        // POST: Workstations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workstation = await _context.Workstations.FindAsync(id);
            _context.Workstations.Remove(workstation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkstationExists(int id)
        {
            return _context.Workstations.Any(e => e.ID == id);
        }
    }
}
