using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAExpiations.Data;
using SAExpiations.Models;
using SAExpiations.ViewModels;

namespace SAExpiations.Controllers
{
    public class LocalServiceAreasController : Controller
    {
        private readonly ExpiationsContext _context;

        public LocalServiceAreasController(ExpiationsContext context)
        {
            _context = context;
        }

        // GET: LocalServiceAreas
        public async Task<IActionResult> Index(int? year)
        {
            var currentDate = DateTime.Now.Year;
            if (year == null) year = currentDate;
            var Years = new List<int> { currentDate, currentDate - 1, currentDate - 2 };
            var LocalArea = new List<LocalAreaVM>();
            var area = _context.Expiations
                .Where(c => c.IssueDate.Year == year)
                .Where(c => !string.IsNullOrEmpty(c.LocalServiceAreaCode)
                && !string.IsNullOrEmpty(c.ExpiationOffenceCode))
                .Select(c => new
                {
                    c.LocalServiceAreaCode,
                    c.IssueDate
                })
                .AsEnumerable()
                .OrderBy(c => c.LocalServiceAreaCode)
                .GroupBy(c => c.LocalServiceAreaCode);

            foreach (var items in area)
            {
                LocalArea.Add(new LocalAreaVM
                {
                    localServiceArea = items.Key,
                    localServiceArea1 = _context.LocalServiceAreas
                    .Where(c => c.LocalServiceAreaCode == items.Key)
                    .Select(c => c.LocalServiceArea1)
                    .First(),

                    RegionCount = items.Count()
                });
            }

            return View(new LocalServiceVM
            {
                Years = Years,
                LocalArea = LocalArea,
                SelectedYear = (int)year

            });
        }

        // GET: LocalServiceAreas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.LocalServiceAreas == null)
            {
                return NotFound();
            }

            var localServiceArea = await _context.LocalServiceAreas
                .FirstOrDefaultAsync(m => m.LocalServiceAreaCode == id);
            if (localServiceArea == null)
            {
                return NotFound();
            }

            return View(localServiceArea);
        }

        // GET: LocalServiceAreas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LocalServiceAreas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LocalServiceAreaCode,LocalServiceArea1")] LocalServiceArea localServiceArea)
        {
            if (ModelState.IsValid)
            {
                _context.Add(localServiceArea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(localServiceArea);
        }

        // GET: LocalServiceAreas/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.LocalServiceAreas == null)
            {
                return NotFound();
            }

            var localServiceArea = await _context.LocalServiceAreas.FindAsync(id);
            if (localServiceArea == null)
            {
                return NotFound();
            }
            return View(localServiceArea);
        }

        // POST: LocalServiceAreas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("LocalServiceAreaCode,LocalServiceArea1")] LocalServiceArea localServiceArea)
        {
            if (id != localServiceArea.LocalServiceAreaCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(localServiceArea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocalServiceAreaExists(localServiceArea.LocalServiceAreaCode))
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
            return View(localServiceArea);
        }

        // GET: LocalServiceAreas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.LocalServiceAreas == null)
            {
                return NotFound();
            }

            var localServiceArea = await _context.LocalServiceAreas
                .FirstOrDefaultAsync(m => m.LocalServiceAreaCode == id);
            if (localServiceArea == null)
            {
                return NotFound();
            }

            return View(localServiceArea);
        }

        // POST: LocalServiceAreas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.LocalServiceAreas == null)
            {
                return Problem("Entity set 'ExpiationsContext.LocalServiceAreas'  is null.");
            }
            var localServiceArea = await _context.LocalServiceAreas.FindAsync(id);
            if (localServiceArea != null)
            {
                _context.LocalServiceAreas.Remove(localServiceArea);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocalServiceAreaExists(string id)
        {
          return _context.LocalServiceAreas.Any(e => e.LocalServiceAreaCode == id);
        }
    }
}
