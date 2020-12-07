using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ILoveEnglishSchool.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ILoveEnglishSchool.Data;
using ILoveEnglishSchool.Models;
using ILoveEnglishSchool.Controllers;

namespace ILoveEnglishSchool.Controllers
{
    public class PartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Part
        public async Task<IActionResult> Index(int id)
        {
            var applicationDbContext = _context.Parts.Where(p => p.LessonId.Equals(id)).Include(p=>p.Lesson);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Part/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var part = await _context.PartModulesEnumerable.Include(p=>p.Part)
                .Include(p => p.Part.Lesson).ToListAsync();
            if (part == null)
            {
                return NotFound();
            }

            return View(part.Where(p=>p.PartId.Equals(id)));
        }

        // GET: Part/Create
        public IActionResult Create(int id) {
            ViewData["ID"] = id;
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
                return RedirectToAction(nameof(Index),"PartModules", new {id = part.PartId});
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

            ViewData["LessonId"] =await _context.Lessons.ToListAsync();
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
                return RedirectToAction(nameof(Index), new {id = part.LessonId});
            }
            ViewData["LessonId"] = await _context.Lessons.ToListAsync();
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
            return RedirectToAction(nameof(Index),new{id=id});
        }

        private bool PartExists(int id)
        {
            return _context.Parts.Any(e => e.PartId == id);
        }
    }
}
