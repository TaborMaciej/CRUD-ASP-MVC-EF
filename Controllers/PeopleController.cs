using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lista7_zad1.Context;
using lista7_zad1.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace lista7_zad1.Controllers
{
    [Authorize]
    public class PeopleController : Controller
    {
        private readonly StudentsDbContext _context;

        public PeopleController(StudentsDbContext context)
        {
            _context = context;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
              return _context.Osoby != null ? 
                          View(await _context.Osoby.ToListAsync()) :
                          Problem("Entity set 'StudentsDbContext.Osoby'  is null.");
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Osoby == null)
            {
                return NotFound();
            }

            var people = await _context.Osoby
                .FirstOrDefaultAsync(m => m.OsobaId == id);
            if (people == null)
            {
                return NotFound();
            }

            return View(people);
        }

        // GET: People/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("OsobaId,Name,Surname,BirthDate")] People people)
        {
            if (ModelState.IsValid)
            {
                _context.Add(people);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(people);
        }

        // GET: People/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Osoby == null)
            {
                return NotFound();
            }

            var people = await _context.Osoby.FindAsync(id);
            if (people == null)
            {
                return NotFound();
            }
            return View(people);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("OsobaId,Name,Surname,BirthDate")] People people)
        {
            if (id != people.OsobaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(people);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeopleExists(people.OsobaId))
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
            return View(people);
        }

        // GET: People/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Osoby == null)
            {
                return NotFound();
            }

            var people = await _context.Osoby
                .FirstOrDefaultAsync(m => m.OsobaId == id);
            if (people == null)
            {
                return NotFound();
            }

            return View(people);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Osoby == null)
            {
                return Problem("Entity set 'StudentsDbContext.Osoby'  is null.");
            }
            var people = await _context.Osoby.FindAsync(id);
            if (people != null)
            {
                _context.Osoby.Remove(people);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeopleExists(int id)
        {
          return (_context.Osoby?.Any(e => e.OsobaId == id)).GetValueOrDefault();
        }
    }
}
