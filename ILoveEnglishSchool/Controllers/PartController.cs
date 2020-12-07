using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ILoveEnglishSchool.Data;
using ILoveEnglishSchool.Models;

namespace ILoveEnglishSchool
{
    public class PartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Part
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Parts.Include(p => p.Lesson);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Part/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var part = await _context.Parts
                .Include(p => p.Lesson)
                .FirstOrDefaultAsync(m => m.PartId == id);
            if (part == null)
            {
                return NotFound();
            }

            return View(part);
        }

        // GET: Part/Create
        public IActionResult Create()
        {
            ViewData["LessonId"] = new SelectList(_context.Lessons, "LessonId", "LessonId");
            return View();
        }

        // POST: Part/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PartId,WelcomeImage,Name,LessonId,Type")] Part part)
        {
            if (ModelState.IsValid)
            {
                _context.Add(part);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LessonId"] = new SelectList(_context.Lessons, "LessonId", "LessonId", part.LessonId);
            return View(part);
        }

        // GET: Part/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var part = await _context.Parts.FindAsync(id);
            if (part == null)
            {
                return NotFound();
            }
            ViewData["LessonId"] = new SelectList(_context.Lessons, "LessonId", "LessonId", part.LessonId);
            return View(part);
        }

        // POST: Part/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PartId,WelcomeImage,Name,LessonId,Type")] Part part)
        {
            if (id != part.PartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(part);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartExists(part.PartId))
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
            ViewData["LessonId"] = new SelectList(_context.Lessons, "LessonId", "LessonId", part.LessonId);
            return View(part);
        }

        // GET: Part/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var part = await _context.Parts
                .Include(p => p.Lesson)
                .FirstOrDefaultAsync(m => m.PartId == id);
            if (part == null)
            {
                return NotFound();
            }

            return View(part);
        }

        // POST: Part/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var part = await _context.Parts.FindAsync(id);
            _context.Parts.Remove(part);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartExists(int id)
        {
            return _context.Parts.Any(e => e.PartId == id);
        }
    }
}
