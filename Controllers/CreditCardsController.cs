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

    public class CreditCardsController : Controller
    {
        private readonly Rent_a_carContext _context;

        public CreditCardsController(Rent_a_carContext context)
        {
            _context = context;
        }

        [CustomAuthorize("Admin,Employee,Driver")]
        // GET: CreditCards
        public async Task<IActionResult> Index()
        {
            var rent_a_carContext = _context.CreditCards.Include(c => c.User);
            return View(await rent_a_carContext.ToListAsync());
        }

        [CustomAuthorize("Admin,Employee,Driver")]
        // GET: CreditCards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditCard = await _context.CreditCards
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CreditCardId == id);
            if (creditCard == null)
            {
                return NotFound();
            }

            return View(creditCard);
        }

        [CustomAuthorize("Admin,Employee")]
        // GET: CreditCards/Create
        public IActionResult Create()
        {
            ViewData["UserName"] = new SelectList(_context.Users, "Username", "Username");
            return View();
        }

        // POST: CreditCards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize("Admin,Employee")]
        public async Task<IActionResult> Create([Bind("CreditCardId,ExpTime,UserId,Limit")] CreditCard creditCard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(creditCard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["UserName"] = new SelectList(_context.Users, "Username", "Username", creditCard.User.Username);
            return View(creditCard);
        }


        [CustomAuthorize("Admin,Employee")]
        // GET: CreditCards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditCard = await _context.CreditCards.FindAsync(id);
            if (creditCard == null)
            {
                return NotFound();
            }

            ViewData["UserName"] = new SelectList(_context.Users, "Username", "Username", creditCard.User.Username);
            return View(creditCard);
        }

        // POST: CreditCards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize("Admin,Employee")]
        public async Task<IActionResult> Edit(int id, [Bind("CreditCardId,ExpTime,UserId,Limit")] CreditCard creditCard)
        {
            if (id != creditCard.CreditCardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(creditCard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CreditCardExists(creditCard.CreditCardId))
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
            ViewData["UserName"] = new SelectList(_context.Users, "Username", "Username", creditCard.User.Username);
            return View(creditCard);
        }

        // GET: CreditCards/Delete/5
        [CustomAuthorize("Admin,Employee")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditCard = await _context.CreditCards
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CreditCardId == id);
            if (creditCard == null)
            {
                return NotFound();
            }

            return View(creditCard);
        }

        // POST: CreditCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize("Admin,Employee")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var creditCard = await _context.CreditCards.FindAsync(id);
            _context.CreditCards.Remove(creditCard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CreditCardExists(int id)
        {
            return _context.CreditCards.Any(e => e.CreditCardId == id);
        }
    }
}
