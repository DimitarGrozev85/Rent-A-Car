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

    public class GaragesController : Controller
    {
        private readonly Rent_a_carContext _context;

        public GaragesController(Rent_a_carContext context)
        {
            _context = context;
        }

        // GET: Garages
        public async Task<IActionResult> Index()
        {
            var rent_a_carContext = _context.Garages.Include(g => g.Car);
            return View(await rent_a_carContext.ToListAsync());
        }

        // GET: Garages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var garage = await _context.Garages
                .Include(g => g.Car)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (garage == null)
            {
                return NotFound();
            }

            return View(garage);
        }

        // GET: Garages/Create
        public IActionResult Create()
        {
            ViewData["CarName"] = new SelectList(_context.Cars, "CarName", "CarName");
            return View();
        }

        // POST: Garages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CarId,StorageTime,DeliveryDate")] Garage garage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(garage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarName"] = new SelectList(_context.Cars, "CarName", "CarName", garage.Car.CarName);
            return View(garage);
        }

        // GET: Garages/Edit/5
        [CustomAuthorize("Admin,Employee")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var garage = await _context
                .Garages
                .Include(item => item.Car)
                .FirstOrDefaultAsync(item => item.Id == id);

            if (garage == null)
            {
                return NotFound();
            }

            ViewData["CarName"] = new SelectList(_context.Cars, "CarName", "CarName", garage.Car.CarName);
            return View(garage);
        }

        // POST: Garages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize("Admin,Employee")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CarId,StorageTime,DeliveryDate")] Garage garage)
        {
            if (id != garage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(garage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GarageExists(garage.Id))
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

            ViewData["CarName"] = new SelectList(_context.Cars, "CarName", "CarName", garage.Car.CarName);
            return View(garage);
        }

        // GET: Garages/Delete/5
        [CustomAuthorize("Admin,Employee")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var garage = await _context.Garages
                .Include(g => g.Car)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (garage == null)
            {
                return NotFound();
            }

            return View(garage);
        }

        // POST: Garages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize("Admin,Employee")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var garage = await _context.Garages.FindAsync(id);
            _context.Garages.Remove(garage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GarageExists(int id)
        {
            return _context.Garages.Any(e => e.Id == id);
        }
    }
}
