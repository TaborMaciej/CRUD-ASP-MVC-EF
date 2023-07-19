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
    public class StudentsController : Controller
    {
        private readonly StudentsDbContext _context;

        public StudentsController(StudentsDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var studentsDbContext = _context.Students.Include(s => s.Major).Include(s => s.People);
            return View(await studentsDbContext.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Major)
                .Include(s => s.People)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["MajorID"] = new SelectList(_context.Majors.Select(s => new {ID = s.MajorID, Name = s.MajorID + " - " + s.Name}), "ID", "Name");
            ViewData["PeopleID"] = new SelectList(_context.Osoby.Select(s => new {ID = s.OsobaId, Name = s.OsobaId + " - " + s.Name + " " + s.Surname}), "ID", "Name");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("StudentId,AlbumNr,PeopleID,MajorID")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MajorID"] = new SelectList(_context.Majors.Select(s => new { ID = s.MajorID, Name = s.MajorID + " - " + s.Name }), "ID", "Name");
            ViewData["PeopleID"] = new SelectList(_context.Osoby.Select(s => new { ID = s.OsobaId, Name = s.OsobaId + " - " + s.Name + " " + s.Surname }), "ID", "Name");
            return View(student);
        }

        // GET: Students/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["MajorID"] = new SelectList(_context.Majors.Select(s => new { ID = s.MajorID, Name = s.MajorID + " - " + s.Name }), "ID", "Name");
            ViewData["PeopleID"] = new SelectList(_context.Osoby.Select(s => new { ID = s.OsobaId, Name = s.OsobaId + " - " + s.Name + " " + s.Surname }), "ID", "Name");
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,AlbumNr,PeopleID,MajorID")] Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
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
            ViewData["MajorID"] = new SelectList(_context.Majors.Select(s => new { ID = s.MajorID, Name = s.MajorID + " - " + s.Name }), "ID", "Name");
            ViewData["PeopleID"] = new SelectList(_context.Osoby.Select(s => new { ID = s.OsobaId, Name = s.OsobaId + " - " + s.Name + " " + s.Surname }), "ID", "Name");
            return View(student);
        }

        // GET: Students/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Major)
                .Include(s => s.People)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'StudentsDbContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                var studGrades = await _context.Grades.Where(g => g.StudentID == id).ToListAsync();

                foreach (var grades in studGrades)
                {
                    grades.StudentID = null;
                }
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
          return (_context.Students?.Any(e => e.StudentId == id)).GetValueOrDefault();
        }
    }
}
