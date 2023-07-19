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
    public class GradesController : Controller
    {
        private readonly StudentsDbContext _context;

        public GradesController(StudentsDbContext context)
        {
            _context = context;
        }

        // GET: Grades
        public async Task<IActionResult> Index(String SelectedStudent, String SelectedSubject)
        {

            var studentsDbContext = _context.Grades
                                            .Include(g => g.Classification)
                                            .Include(g => g.GradeVal)
                                            .Include(g => g.Professor)
                                            .Include(g => g.Student)
                                            .Include(g => g.Subject)
                                            .AsQueryable();

            ViewData["BaseStudent"] = "";
            ViewData["BaseSubject"] = "";

            if (!String.IsNullOrEmpty(SelectedStudent))
            {
                studentsDbContext = studentsDbContext.Where(s => s.StudentID.ToString() == SelectedStudent);
                ViewData["BaseStudent"] = SelectedStudent;
            }

            if (!String.IsNullOrEmpty(SelectedSubject))
            {
                studentsDbContext = studentsDbContext.Where(s => s.SubjectID.ToString() == SelectedSubject);
                ViewData["BaseSubject"] = SelectedSubject;
            }

            List<SelectListItem> studentsList = new SelectList(_context.Students.Select(s => new { ID = s.StudentId, Name = s.StudentId + " - " + s.AlbumNr }), "ID", "Name").ToList();
            studentsList.Insert(0, (new SelectListItem { Text = "[None]", Value = "" }));

            ViewData["StudentList"] = studentsList;

            List<SelectListItem> subjectList = new SelectList(_context.Subjects, "SubjectID", "Name").ToList();
            subjectList.Insert(0, (new SelectListItem { Text = "[None]", Value = "" }));

            ViewData["SubjectList"] = subjectList;

            return View(await studentsDbContext.ToListAsync());
        }

        // GET: Grades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Grades == null)
            {
                return NotFound();
            }

            var grade = await _context.Grades
                .Include(g => g.Classification)
                .Include(g => g.GradeVal)
                .Include(g => g.Professor)
                .Include(g => g.Student)
                .Include(g => g.Subject)
                .FirstOrDefaultAsync(m => m.GradeID == id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // GET: Grades/Create
        public IActionResult Create()
        {
            ViewData["ClassificationID"] = new SelectList(_context.Classifications.Select(s => new {ID = s.ClassificationID, Name = s.ClassificationID + " - " + s.Name}), "ID", "Name");
            ViewData["GradeValID"] = new SelectList(_context.GradeVals.Select(s => new {ID = s.GradeValID, Name = s.Value + " (" + s.Name + ")"}), "ID", "Name");
            ViewData["ProfessorID"] = new SelectList(_context.Professors.Select(s => new {ID = s.ProfessorID, Name = s.ProfessorID + " - " + s.AlbumNr }), "ID", "Name");
            ViewData["StudentID"] = new SelectList(_context.Students.Select(s => new { ID = s.StudentId, Name = s.StudentId + " - " + s.AlbumNr }), "ID", "Name");
            ViewData["SubjectID"] = new SelectList(_context.Subjects.Select(s => new { ID = s.SubjectID, Name = s.SubjectID + " - " + s.Name }), "ID", "Name");
            return View();
        }

        // POST: Grades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GradeID,Date,StudentID,ProfessorID,GradeValID,SubjectID,ClassificationID")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassificationID"] = new SelectList(_context.Classifications.Select(s => new { ID = s.ClassificationID, Name = s.ClassificationID + " - " + s.Name }), "ID", "Name");
            ViewData["GradeValID"] = new SelectList(_context.GradeVals.Select(s => new { ID = s.GradeValID, Name = s.Value + " (" + s.Name + ")" }), "ID", "Name");
            ViewData["ProfessorID"] = new SelectList(_context.Professors.Select(s => new { ID = s.ProfessorID, Name = s.ProfessorID + " - " + s.AlbumNr }), "ID", "Name");
            ViewData["StudentID"] = new SelectList(_context.Students.Select(s => new { ID = s.StudentId, Name = s.StudentId + " - " + s.AlbumNr }), "ID", "Name");
            ViewData["SubjectID"] = new SelectList(_context.Subjects.Select(s => new { ID = s.SubjectID, Name = s.SubjectID + " - " + s.Name }), "ID", "Name");
            return View(grade);
        }

        // GET: Grades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Grades == null)
            {
                return NotFound();
            }

            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }
            ViewData["ClassificationID"] = new SelectList(_context.Classifications.Select(s => new { ID = s.ClassificationID, Name = s.ClassificationID + " - " + s.Name }), "ID", "Name");
            ViewData["GradeValID"] = new SelectList(_context.GradeVals.Select(s => new { ID = s.GradeValID, Name = s.Value + " (" + s.Name + ")" }), "ID", "Name");
            ViewData["ProfessorID"] = new SelectList(_context.Professors.Select(s => new { ID = s.ProfessorID, Name = s.ProfessorID + " - " + s.AlbumNr }), "ID", "Name");
            ViewData["StudentID"] = new SelectList(_context.Students.Select(s => new { ID = s.StudentId, Name = s.StudentId + " - " + s.AlbumNr }), "ID", "Name");
            ViewData["SubjectID"] = new SelectList(_context.Subjects.Select(s => new { ID = s.SubjectID, Name = s.SubjectID + " - " + s.Name }), "ID", "Name");
            return View(grade);
        }

        // POST: Grades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GradeID,Date,StudentID,ProfessorID,GradeValID,SubjectID,ClassificationID")] Grade grade)
        {
            if (id != grade.GradeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeExists(grade.GradeID))
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
            ViewData["ClassificationID"] = new SelectList(_context.Classifications.Select(s => new { ID = s.ClassificationID, Name = s.ClassificationID + " - " + s.Name }), "ID", "Name");
            ViewData["GradeValID"] = new SelectList(_context.GradeVals.Select(s => new { ID = s.GradeValID, Name = s.Value + " (" + s.Name + ")" }), "ID", "Name");
            ViewData["ProfessorID"] = new SelectList(_context.Professors.Select(s => new { ID = s.ProfessorID, Name = s.ProfessorID + " - " + s.AlbumNr }), "ID", "Name");
            ViewData["StudentID"] = new SelectList(_context.Students.Select(s => new { ID = s.StudentId, Name = s.StudentId + " - " + s.AlbumNr }), "ID", "Name");
            ViewData["SubjectID"] = new SelectList(_context.Subjects.Select(s => new { ID = s.SubjectID, Name = s.SubjectID + " - " + s.Name }), "ID", "Name");
            return View(grade);
        }

        // GET: Grades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Grades == null)
            {
                return NotFound();
            }

            var grade = await _context.Grades
                .Include(g => g.Classification)
                .Include(g => g.GradeVal)
                .Include(g => g.Professor)
                .Include(g => g.Student)
                .Include(g => g.Subject)
                .FirstOrDefaultAsync(m => m.GradeID == id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // POST: Grades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Grades == null)
            {
                return Problem("Entity set 'StudentsDbContext.Grades'  is null.");
            }
            var grade = await _context.Grades.FindAsync(id);
            if (grade != null)
            {
                _context.Grades.Remove(grade);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradeExists(int id)
        {
          return (_context.Grades?.Any(e => e.GradeID == id)).GetValueOrDefault();
        }
    }
}
