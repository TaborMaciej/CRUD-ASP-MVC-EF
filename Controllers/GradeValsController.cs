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
    public class GradeValsController : Controller
    {
        private readonly StudentsDbContext _context;

        public GradeValsController(StudentsDbContext context)
        {
            _context = context;
        }

        // GET: GradeVals
        public async Task<IActionResult> Index()
        {
            var studentsDbContext = _context.GradeVals.OrderBy(s => s.Value);
            return studentsDbContext != null ? 
                          View(await studentsDbContext.ToListAsync()) :
                          Problem("Entity set 'StudentsDbContext.GradeVals'  is null.");
        }

        // GET: GradeVals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GradeVals == null)
            {
                return NotFound();
            }

            var gradeVal = await _context.GradeVals
                .FirstOrDefaultAsync(m => m.GradeValID == id);
            if (gradeVal == null)
            {
                return NotFound();
            }

            return View(gradeVal);
        }

        // GET: GradeVals/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: GradeVals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("GradeValID,Name,Value")] GradeVal gradeVal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gradeVal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gradeVal);
        }

        // GET: GradeVals/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GradeVals == null)
            {
                return NotFound();
            }

            var gradeVal = await _context.GradeVals.FindAsync(id);
            if (gradeVal == null)
            {
                return NotFound();
            }
            return View(gradeVal);
        }

        // POST: GradeVals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("GradeValID,Name,Value")] GradeVal gradeVal)
        {
            if (id != gradeVal.GradeValID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gradeVal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeValExists(gradeVal.GradeValID))
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
            return View(gradeVal);
        }

        // GET: GradeVals/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GradeVals == null)
            {
                return NotFound();
            }

            var gradeVal = await _context.GradeVals
                .FirstOrDefaultAsync(m => m.GradeValID == id);
            if (gradeVal == null)
            {
                return NotFound();
            }

            return View(gradeVal);
        }

        // POST: GradeVals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GradeVals == null)
            {
                return Problem("Entity set 'StudentsDbContext.GradeVals'  is null.");
            }
            var gradeVal = await _context.GradeVals.FindAsync(id);
            if (gradeVal != null)
            {
                var gradeVals = await _context.Grades.Where(g => g.GradeValID == id).ToListAsync();

                foreach (var grades in gradeVals)
                {
                    grades.GradeValID= null;
                }
                _context.GradeVals.Remove(gradeVal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradeValExists(int id)
        {
          return (_context.GradeVals?.Any(e => e.GradeValID == id)).GetValueOrDefault();
        }
    }
}
