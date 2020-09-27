using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flow.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Flow.Controllers
{
    [Authorize]
    public class WorkstationKioskController : Controller
    {
        private readonly ApplicationDbContext _context;
        public WorkstationKioskController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // GET: WorkstationKioskController
        public ActionResult Index()
        {

            //if (id == null)
            //{
            //    return NotFound();
            //}

            return View();
        }

        // GET: WorkstationKioskController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WorkstationKioskController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkstationKioskController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
