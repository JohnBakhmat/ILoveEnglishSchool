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
    public class PartModulesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PartModulesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PartModules
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PartModulesEnumerable.Include(p => p.Part);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PartModules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partModules = await _context.PartModulesEnumerable
                .Include(p => p.Part)
                .FirstOrDefaultAsync(m => m.ModuleId == id);
            if (partModules == null)
            {
                return NotFound();
            }

            return View(partModules);
        }

        // GET: PartModules/Create
        public IActionResult Create()
        {
            ViewData["PartId"] = new SelectList(_context.Parts, "PartId", "PartId");
            return View();
        }

        // POST: PartModules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModuleId,PartId,Header,Description,ContentUrl")] PartModules partModules)
        {
            if (ModelState.IsValid)
            {
                _context.Add(partModules);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PartId"] = new SelectList(_context.Parts, "PartId", "PartId", partModules.PartId);
            return View(partModules);
        }

        // GET: PartModules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partModules = await _context.PartModulesEnumerable.FindAsync(id);
            if (partModules == null)
            {
                return NotFound();
            }
            ViewData["PartId"] = new SelectList(_context.Parts, "PartId", "PartId", partModules.PartId);
            return View(partModules);
        }

        // POST: PartModules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ModuleId,PartId,Header,Description,ContentUrl")] PartModules partModules)
        {
            if (id != partModules.ModuleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partModules);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartModulesExists(partModules.ModuleId))
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
            ViewData["PartId"] = new SelectList(_context.Parts, "PartId", "PartId", partModules.PartId);
            return View(partModules);
        }

        // GET: PartModules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partModules = await _context.PartModulesEnumerable
                .Include(p => p.Part)
                .FirstOrDefaultAsync(m => m.ModuleId == id);
            if (partModules == null)
            {
                return NotFound();
            }

            return View(partModules);
        }

        // POST: PartModules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var partModules = await _context.PartModulesEnumerable.FindAsync(id);
            _context.PartModulesEnumerable.Remove(partModules);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartModulesExists(int id)
        {
            return _context.PartModulesEnumerable.Any(e => e.ModuleId == id);
        }
    }
}
