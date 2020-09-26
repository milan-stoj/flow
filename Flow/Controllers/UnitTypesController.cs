using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Flow.Data;
using Flow.Models;
using Microsoft.AspNetCore.Authorization;

namespace Flow.Controllers
{
    [Authorize(Roles = "Administrator, MfgEngineer")]
    public class UnitTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UnitTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UnitTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.UnitType.ToListAsync());
        }

        // GET: UnitTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unitType = await _context.UnitType
                .FirstOrDefaultAsync(m => m.ID == id);
            if (unitType == null)
            {
                return NotFound();
            }

            return View(unitType);
        }

        // GET: UnitTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UnitTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Prefix,Description,WorkInstructions,StandardRate")] UnitType unitType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(unitType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(unitType);
        }

        // GET: UnitTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unitType = await _context.UnitType.FindAsync(id);
            if (unitType == null)
            {
                return NotFound();
            }
            return View(unitType);
        }

        // POST: UnitTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Prefix,Description,WorkInstructions,StandardRate")] UnitType unitType)
        {
            if (id != unitType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unitType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnitTypeExists(unitType.ID))
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
            return View(unitType);
        }

        // GET: UnitTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unitType = await _context.UnitType
                .FirstOrDefaultAsync(m => m.ID == id);
            if (unitType == null)
            {
                return NotFound();
            }

            return View(unitType);
        }

        // POST: UnitTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var unitType = await _context.UnitType.FindAsync(id);
            _context.UnitType.Remove(unitType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnitTypeExists(int id)
        {
            return _context.UnitType.Any(e => e.ID == id);
        }
    }
}
