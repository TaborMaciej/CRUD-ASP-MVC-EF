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

namespace lista7_zad1.Controllers
{
    [Authorize]
    public class ClassificationsController : Controller
    {
        private readonly StudentsDbContext _context;

        public ClassificationsController(StudentsDbContext context)
        {
            _context = context;
        }

        // GET: Classifications
        public async Task<IActionResult> Index()
        {
              return _context.Classifications != null ? 
                          View(await _context.Classifications.ToListAsync()) :
                          Problem("Entity set 'StudentsDbContext.Classifications'  is null.");
        }

        // GET: Classifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Classifications == null)
            {
                return NotFound();
            }

            var classification = await _context.Classifications
                .FirstOrDefaultAsync(m => m.ClassificationID == id);
            if (classification == null)
            {
                return NotFound();
            }

            return View(classification);
        }

        // GET: Classifications/Create

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Classifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("ClassificationID,Name")] Classification classification)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classification);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classification);
        }

        // GET: Classifications/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Classifications == null)
            {
                return NotFound();
            }

            var classification = await _context.Classifications.FindAsync(id);
            if (classification == null)
            {
                return NotFound();
            }
            return View(classification);
        }

        // POST: Classifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ClassificationID,Name")] Classification classification)
        {
            if (id != classification.ClassificationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassificationExists(classification.ClassificationID))
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
            return View(classification);
        }

        // GET: Classifications/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Classifications == null)
            {
                return NotFound();
            }

            var classification = await _context.Classifications
                .FirstOrDefaultAsync(m => m.ClassificationID == id);
            if (classification == null)
            {
                return NotFound();
            }

            return View(classification);
        }

        // POST: Classifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Classifications == null)
            {
                return Problem("Entity set 'StudentsDbContext.Classifications'  is null.");
            }
            var classification = await _context.Classifications.FindAsync(id);
            if (classification != null)
            {
                var classGrades = await _context.Grades.Where(g => g.ClassificationID == id).ToListAsync();

                foreach (var grades in classGrades)
                {
                    grades.ClassificationID = null;
                }
                _context.Classifications.Remove(classification);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassificationExists(int id)
        {
          return (_context.Classifications?.Any(e => e.ClassificationID == id)).GetValueOrDefault();
        }
    }
}
