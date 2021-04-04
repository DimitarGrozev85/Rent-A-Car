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

    public class CarsController : Controller
    {
        private readonly Rent_a_carContext _context;

        public CarsController(Rent_a_carContext context)
        {
            _context = context;
        }

        [CustomAuthorize("Admin,Employee,Driver")]
        // GET: Cars
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cars.ToListAsync());
        }

        [CustomAuthorize("Admin,Employee,Driver")]
        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        [CustomAuthorize("Admin,Employee")]
        // GET: Cars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        [CustomAuthorize("Admin,Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Make,Model,Price,InsuranceCost,MaintenanceCost")] Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Cars/Edit/5
        [CustomAuthorize("Admin,Employee")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: Cars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize("Admin,Employee")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Make,Model,Price,InsuranceCost,MaintenanceCost")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Cars/Delete/5
        [CustomAuthorize("Admin,Employee")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize("Admin,Employee")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
