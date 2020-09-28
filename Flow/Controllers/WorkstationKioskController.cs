using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                    UnitLog = _context.UnitLogs.Include(u => u.Workstation).Where(w => w.Unit == _context.Workstations.Where(w => w.CurrentUser.Id == userId).FirstOrDefault().CurrentUnit).OrderBy(u => u.EventDate).ToList()
                };

                if (workstationKioskViewModel.UnitLog.Count == 0 && workstationKioskViewModel.Workstation.CurrentUnit != null)
                {
                    await CreateLog(workstationKioskViewModel, "CHECKIN");
                    workstationKioskViewModel.UnitLog = _context.UnitLogs.Where(w => w.Unit == _context.Workstations.Where(w => w.CurrentUser.Id == userId).FirstOrDefault().CurrentUnit).ToList();
                }
                return View(workstationKioskViewModel);
            }
        }

        public async Task<IActionResult> CreateLog(WorkstationKioskViewModel model, string logEvent)
        {
            UnitLog unitLog = new UnitLog
            {
                ApplicationUser = model.ApplicationUser,
                Unit = model.Workstation.CurrentUnit,
                Workstation = model.Workstation,
                EventDate = DateTime.Now,
                Event = logEvent
            };
            await _context.AddAsync(unitLog);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: WorkstationKioskController/Details/5
        public async Task<IActionResult> Login(int? id)
        {
            if (id == null)
            {
                var applicationDbContext = _context.Workstations.Include(w => w.Department).Where(w => w.CurrentUser == null);
                return View(await applicationDbContext.ToListAsync());
            }

            var workstation = _context.Workstations.FirstOrDefaultAsync(w => w.ID == id).Result;
            workstation.Online = true;
            workstation.CurrentUser = _applicationUserRepository.GetApplicationUser(User.FindFirstValue(ClaimTypes.NameIdentifier));

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Logout(int? id)
        {
            var entity = _context.Workstations.Include(w => w.CurrentUser).Include(w=>w.CurrentUnit).FirstOrDefaultAsync(w => w.ID == id).Result;
            if(entity.CurrentUnit != null)
            {
                CheckOutPart(id).Wait();
            }
            entity.CurrentUser = null;
            entity.Online = false;
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
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
            var UnitHistory = _context.Units.Any(u => u.UnitNumber == unit.UnitNumber);
            if (UnitHistory == false)
            {
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

            var workstation = _context.Workstations
                .Include(w => w.CurrentUser)
                .Include(w => w.CurrentUnit)
                .FirstOrDefaultAsync(w => w.CurrentUser.Id == userId).Result;

            var startedUnit = _context
                .Units
                .Include(u => u.UnitType)
                .Where(u => u.UnitNumber == unit.UnitNumber).FirstOrDefault();


            DateTime workStart = _context.UnitLogs.Where(u => u.Unit == startedUnit).OrderByDescending(u => u.EventDate).First(u => u.Event == "CHECKOUT").EventDate;
            double nonValueAdd = TimeDifference(workStart);

            var newLog = new UnitLog
            {
                ApplicationUser = workstation.CurrentUser,
                Unit = startedUnit,
                Workstation = workstation,
                EventDate = DateTime.Now,
                InventoryTime = nonValueAdd,
                Event = "CHECKIN"
            };

            _context.Workstations.Where(w => w.CurrentUser.Id == userId).FirstOrDefault().CurrentUnit = startedUnit;

            await _context.AddAsync(newLog);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: WorkstationKioskController/Edit/5
        public async Task<IActionResult> CheckOutPart(int? id)
        {
            var entity = _context.Workstations
                .Include(w => w.CurrentUser)
                .Include(w => w.CurrentUnit)
                .FirstOrDefaultAsync(w => w.ID == id).Result;

            DateTime workStart = _context.UnitLogs.Where(u => u.Unit == entity.CurrentUnit).OrderByDescending(u => u.EventDate).First(u => u.Event == "CHECKIN").EventDate;
            double valueAdd = TimeDifference(workStart);

            var newLog = new UnitLog
            {
                ApplicationUser = entity.CurrentUser,
                Unit = entity.CurrentUnit,
                Workstation = entity,
                EventDate = DateTime.Now,
                ValueAddedTime = valueAdd,
                Event = "CHECKOUT"
            };

            await _context.AddAsync(newLog);

            entity.CurrentUnit = null;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // POST: WorkstationKioskController/Edit/5
        public async Task<IActionResult> HoldPart(int? id)
        {
            var entity = _context.Workstations
                .Include(w => w.CurrentUser)
                .Include(w => w.CurrentUnit)
                .FirstOrDefaultAsync(w => w.ID == id).Result;

            DateTime workStart = _context.UnitLogs.Where(u => u.Unit == entity.CurrentUnit).OrderByDescending(u => u.EventDate).First(u => u.Event == "CHECKIN").EventDate;
            double valueAdd = TimeDifference(workStart);
            
            var newLog = new UnitLog
            {
                ApplicationUser = entity.CurrentUser,
                Unit = entity.CurrentUnit,
                Workstation = entity,
                EventDate = DateTime.Now,
                ValueAddedTime = valueAdd,
                Event = "QA HOLD"
            };

            await _context.AddAsync(newLog);

            entity.CurrentUnit.QA = true;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveHold(WorkstationKioskViewModel model)
        {
            var userTry = _context.Users.Where(u => u.Email == model.ApplicationUser.Email).FirstOrDefault();

            if(userTry.UserRole == "QA" && userTry != null)
            {
                var entity = _context.Workstations
                    .Include(w => w.CurrentUser)
                    .Include(w => w.CurrentUnit)
                    .FirstOrDefaultAsync(w => w.ID == model.Workstation.ID).Result;

                DateTime holdStart = _context.UnitLogs.Where(u => u.Unit == entity.CurrentUnit).OrderByDescending(u => u.EventDate).First(u => u.Event == "QA HOLD").EventDate;
                double nonValueAdd = TimeDifference(holdStart);
                    
                var newLog = new UnitLog
                {
                    ApplicationUser = entity.CurrentUser,
                    Unit = entity.CurrentUnit,
                    Workstation = entity,
                    EventDate = DateTime.Now,
                    NonValueAddedTime = nonValueAdd,
                    Event = "GREEN TAG"
                };
                await _context.AddAsync(newLog);
                entity.CurrentUnit.QA = false;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> CompletePart(int? id)
        {
            var entity = _context.Workstations
                .Include(w => w.CurrentUser)
                .Include(w => w.CurrentUnit).Include(u => u.CurrentUnit.UnitType)
                .FirstOrDefaultAsync(w => w.ID == id).Result;

            DateTime workStart = _context.UnitLogs.Where(u => u.Unit == entity.CurrentUnit).OrderByDescending(u => u.EventDate).First(u => u.Event == "CHECKIN").EventDate;
            DateTime unitStart = _context.UnitLogs.Where(u => u.Unit == entity.CurrentUnit).OrderBy(u => u.EventDate).First().EventDate;

            double valueAdd = TimeDifference(workStart);
            double completionTime = TimeDifference(unitStart);
            double eff = completionTime / entity.CurrentUnit.UnitType.StandardRate;

            var newLog = new UnitLog
            {
                ApplicationUser = entity.CurrentUser,
                Unit = entity.CurrentUnit,
                Workstation = entity,
                EventDate = DateTime.Now,
                ValueAddedTime = valueAdd,
                CompletionTime = completionTime,
                Efficiency = eff,
                Event = "COMPLETE"
            };

            await _context.AddAsync(newLog);

            entity.CurrentUnit = null;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public double TimeDifference(DateTime start)
        {
            TimeSpan ts = DateTime.Now - start;
            return (double)ts.TotalMinutes;
        }
    }
}
