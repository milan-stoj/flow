using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Flow.Data;
using Flow.Models;
using Flow.Models.Logs;
using Flow.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flow.Controllers
{
    [Authorize]
    public class WorkstationKioskController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IApplicationUserRepository _applicationUserRepository;

        public WorkstationKioskController(ApplicationDbContext context, IApplicationUserRepository applicationUserRepository)
        {
            _context = context;
            _applicationUserRepository = applicationUserRepository;
        }
        
        // GET: WorkstationKioskController
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var WorkstationLink = _context.Workstations.Any(w => w.CurrentUser.Id == userId);
            if (WorkstationLink == false)
            {
                return RedirectToAction(nameof(Login));
            }
            else
            {
                WorkstationKioskViewModel workstationKioskViewModel = new WorkstationKioskViewModel()
                {
                    ApplicationUser = _applicationUserRepository.GetApplicationUser(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    Department = _context.Workstations.Where(w => w.CurrentUser.Id == userId).Select(w => w.Department).FirstOrDefault(),
                    Workstation = _context.Workstations.Where(w => w.CurrentUser.Id == userId).Include(w => w.CurrentUnit).Include(w => w.CurrentUnit.UnitType).FirstOrDefault(),
                    UnitLog = _context.UnitLogs.Where(w => w.Unit == _context.Workstations.Where(w => w.CurrentUser.Id == userId).FirstOrDefault().CurrentUnit).ToList()
                };

                if (workstationKioskViewModel.UnitLog.Count == 0)
                {
                    await CreateLog(workstationKioskViewModel, "INIT");
                }

                return View(workstationKioskViewModel);
            }
        }

        public async Task<IActionResult> CreateLog(WorkstationKioskViewModel model, string logEvent)
        {
            UnitLog unitLog = new UnitLog();
            unitLog.ApplicationUser = model.ApplicationUser;
            unitLog.Unit = model.Workstation.CurrentUnit;
            unitLog.Workstation = model.Workstation;
            unitLog.EventDate = DateTime.Now;
            unitLog.Event = logEvent;
            _context.Add(unitLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: WorkstationKioskController/Details/5
        public async Task<IActionResult> Login(int? id)
        {
            if (id == null)
            {
                var applicationDbContext = _context.Workstations.Include(w => w.Department);
                return View(await applicationDbContext.ToListAsync());
            }
            _context.Workstations.FirstOrDefaultAsync(w => w.ID == id).Result.CurrentUser = _applicationUserRepository.GetApplicationUser(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Logout(int? id)
        {
            _context.Workstations.FirstOrDefaultAsync(w => w.ID == id).Result.CurrentUser = default;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
        // GET: WorkstationKioskController/Create
        public ActionResult CheckInPart()
        {
            return View();
        }

        // POST: WorkstationKioskController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckInPart([Bind("ID,UnitNumber")] Unit unit)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            unit.UnitType = _context.UnitTypes.Where(u => u.Prefix == unit.UnitNumber.Substring(0, 4)).FirstOrDefault();
            _context.Workstations.Where(w => w.CurrentUser.Id == userId).FirstOrDefault().CurrentUnit = unit;



            if (ModelState.IsValid)
            {
                _context.Add(unit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: WorkstationKioskController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WorkstationKioskController/Edit/5
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

        // GET: WorkstationKioskController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WorkstationKioskController/Delete/5
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
