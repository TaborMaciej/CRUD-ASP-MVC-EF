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
    public class ProfessorsController : Controller
    {
        private readonly StudentsDbContext _context;

        public ProfessorsController(StudentsDbContext context)
        {
            _context = context;
        }

        // GET: Professors
        public async Task<IActionResult> Index()
        {
            var studentsDbContext = _context.Professors.Include(p => p.People);
            return View(await studentsDbContext.ToListAsync());
        }

        // GET: Professors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Professors == null)
            {
                return NotFound();
            }

            var professor = await _context.Professors
                .Include(p => p.People)
                .FirstOrDefaultAsync(m => m.ProfessorID == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        // GET: Professors/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["PeopleID"] = new SelectList(_context.Osoby.Select(s => new { ID = s.OsobaId, Name = s.OsobaId + " - " + s.Name + " " + s.Surname}), "ID", "Name");
            return View();
        }

        // POST: Professors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("ProfessorID,AlbumNr,Password,PeopleID")] Professor professor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(professor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PeopleID"] = new SelectList(_context.Osoby.Select(s => new { ID = s.OsobaId, Name = s.OsobaId + " - " + s.Name + " " + s.Surname }), "ID", "Name");
            return View(professor);
        }

        // GET: Professors/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Professors == null)
            {
                return NotFound();
            }

            var professor = await _context.Professors.FindAsync(id);
            if (professor == null)
            {
                return NotFound();
            }
            ViewData["PeopleID"] = new SelectList(_context.Osoby.Select(s => new { ID = s.OsobaId, Name = s.OsobaId + " - " + s.Name + " " + s.Surname }), "ID", "Name");
            return View(professor);
        }

        // POST: Professors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ProfessorID,AlbumNr,Password,PeopleID")] Professor professor)
        {
            if (id != professor.ProfessorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(professor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfessorExists(professor.ProfessorID))
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
            ViewData["PeopleID"] = new SelectList(_context.Osoby.Select(s => new { ID = s.OsobaId, Name = s.OsobaId + " - " + s.Name + " " + s.Surname }), "ID", "Name");
            return View(professor);
        }

        // GET: Professors/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Professors == null)
            {
                return NotFound();
            }

            var professor = await _context.Professors
                .Include(p => p.People)
                .FirstOrDefaultAsync(m => m.ProfessorID == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        // POST: Professors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Professors == null)
            {
                return Problem("Entity set 'StudentsDbContext.Professors'  is null.");
            }
            var professor = await _context.Professors.FindAsync(id);
            if (professor != null)
            {
                var profGrades = await _context.Grades.Where(g => g.ProfessorID == id).ToListAsync(); 

                foreach(var grades in profGrades)
                {
                    grades.ProfessorID = null;
                }
                _context.Professors.Remove(professor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfessorExists(int id)
        {
          return (_context.Professors?.Any(e => e.ProfessorID == id)).GetValueOrDefault();
        }
    }
}
