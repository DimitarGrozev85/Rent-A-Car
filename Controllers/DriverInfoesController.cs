using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rent_a_car.Data;
using Rent_a_car.Models;

namespace Rent_a_car.Controllers
{
    using Utils;

    public class DriverInfoesController : Controller
    {
        private readonly Rent_a_carContext _context;

        public DriverInfoesController(Rent_a_carContext context)
        {
            _context = context;
        }

        // GET: DriverInfoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.DriverInfos.ToListAsync());
        }

        // GET: DriverInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driverInfo = await _context.DriverInfos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (driverInfo == null)
            {
                return NotFound();
            }

            return View(driverInfo);
        }

        [CustomAuthorize("Admin,Employee")]
        // GET: DriverInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DriverInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize("Admin,Employee")]
        public async Task<IActionResult> Create([Bind("Id,Age,ExpDriverLicense")] DriverInfo driverInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(driverInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(driverInfo);
        }

        // GET: DriverInfoes/Edit/5
        [CustomAuthorize("Admin,Employeе")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driverInfo = await _context.DriverInfos.FindAsync(id);
            if (driverInfo == null)
            {
                return NotFound();
            }
            return View(driverInfo);
        }

        // POST: DriverInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize("Admin,Employee")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Age,ExpDriverLicense")] DriverInfo driverInfo)
        {
            if (id != driverInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(driverInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DriverInfoExists(driverInfo.Id))
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
            return View(driverInfo);
        }

        // GET: DriverInfoes/Delete/5
        [CustomAuthorize("Admin,Employee")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driverInfo = await _context.DriverInfos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (driverInfo == null)
            {
                return NotFound();
            }

            return View(driverInfo);
        }

        // POST: DriverInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize("Admin,Employee")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var driverInfo = await _context.DriverInfos.FindAsync(id);
            _context.DriverInfos.Remove(driverInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DriverInfoExists(int id)
        {
            return _context.DriverInfos.Any(e => e.Id == id);
        }
    }
}
