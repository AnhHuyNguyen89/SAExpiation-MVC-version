using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAExpiations.Data;
using SAExpiations.Models;
using SAExpiations.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SAExpiations.Controllers
{
    public class ExpiationOffencesController : Controller
    {
        private readonly ExpiationsContext _context;

        public ExpiationOffencesController(ExpiationsContext context)
        {
            _context = context;
        }

        // GET: ExpiationOffences
        public async Task<IActionResult> Index(string searchText , string codeSearch)
        {
           
            var vm = new ExpiationCodeList();
            vm.SearchText = searchText;
            #region searchText
            var code = _context.ExpiationOffences
                          .OrderBy(p => p.ExpiationOffenceCode)
                          .Where(p => !string.IsNullOrWhiteSpace(p.ExpiationOffenceCode))
                          .Select(p => p);
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                code = code.Where(p => p.ExpiationOffenceDescription.Contains(searchText)
                     || p.ExpiationOffenceDescription.StartsWith(searchText));

                code = code.OrderBy(c => (c.ExpiationOffenceDescription.StartsWith(searchText) ? 0 : 1))
                                 .ThenBy(c => c.ExpiationOffenceDescription.Contains(searchText) ? 0 : 1)
                                 .ThenBy(c => c.ExpiationOffenceDescription);
            }
            if (!string.IsNullOrWhiteSpace(codeSearch))
            {
                code = code.Where(p => p.ExpiationOffenceCode.Contains(codeSearch)
                     || p.ExpiationOffenceCode.StartsWith(codeSearch));

                code = code.OrderBy(c => (c.ExpiationOffenceCode.StartsWith(codeSearch) ? 0 : 1))
                                 .ThenBy(c => c.ExpiationOffenceCode.Contains(codeSearch) ? 0 : 1)
                                 .ThenBy(c => c.ExpiationOffenceCode);
            }
            vm.expiationCodes = _context.ExpiationOffences
                .Where(it => !string.IsNullOrEmpty(it.ExpiationOffenceCode))
                .OrderBy(it => it.ExpiationOffenceCode)
                .Select(it => it.ExpiationOffenceCode)
                .ToList();
            #endregion
            vm.Items = await code.ToListAsync();
            return View(vm);
        }

        // GET: ExpiationOffences/Details/5
        public async Task<IActionResult> Details(string id, int? year)
        {
            var currentDate = DateTime.Now.Year;
            if (year == null) year = currentDate;
            var report = new Dictionary<string, Dictionary<string, int>>();
            var query = _context.Expiations
                .Where(i => i.IssueDate.Year == year && i.ExpiationOffenceCode == id)
                .Select(i => new
                {
                    i.IssueDate.Month,
                    i.NoticeStatusDesc
                })
                .AsEnumerable()
                .OrderBy(i => i.Month)
                .GroupBy(i => i.Month);
            
            foreach(var item in query)
            {
                var dt = new DateTime(2000, item.Key, 10);
                var k = dt.ToString("MMMM");
                report.Add(k, new Dictionary<string, int>());
                var descriptions = item.GroupBy(ic => ic.NoticeStatusDesc);
                foreach(var desc in descriptions)
                {
                    report[k].Add(desc.Key, desc.AsEnumerable().Count());
                }
            }
            var codeDescription = _context.ExpiationOffences
                .Where(it => it.ExpiationOffenceCode == id)
                .Select(it => it.ExpiationOffenceDescription)
                .First();
            var Count = _context.Expiations
                .Where(it => it.IssueDate.Year == year && it.ExpiationOffenceCode == id)
                .Count();
            var vm = new ExpiationCodeList
            {
                offenceCode = id,
                offenceDescription = codeDescription,
                Years = new List<int> { currentDate, currentDate - 1, currentDate - 2 },
                report = report,
                Count = Count
            };

            var details = _context.Expiations
                .Where(i => i.ExpiationOffenceCode == id)
                .Select(i => new Expiation
                {
                    IssueDate = i.IssueDate,
                    ExpiationOffenceCode = i.ExpiationOffenceCode,
                    ExpiationOffenceCodeNavigation = i.ExpiationOffenceCodeNavigation
                })
                .OrderByDescending(i => i.IssueDate);

            if (id == null || _context.Expiations == null)
            {
                return NotFound();
            }

            if (_context.Expiations == null)
            {
                return NotFound();
            }
            vm.expiations = await details.ToListAsync();
            return View(vm);
        }

        // GET: ExpiationOffences/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExpiationOffences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExpiationOffenceCode,ExpiationOffenceDescription")] ExpiationOffence expiationOffence)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expiationOffence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expiationOffence);
        }

        // GET: ExpiationOffences/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.ExpiationOffences == null)
            {
                return NotFound();
            }

            var expiationOffence = await _context.ExpiationOffences.FindAsync(id);
            if (expiationOffence == null)
            {
                return NotFound();
            }
            return View(expiationOffence);
        }

        // POST: ExpiationOffences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ExpiationOffenceCode,ExpiationOffenceDescription")] ExpiationOffence expiationOffence)
        {
            if (id != expiationOffence.ExpiationOffenceCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expiationOffence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpiationOffenceExists(expiationOffence.ExpiationOffenceCode))
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
            return View(expiationOffence);
        }

        // GET: ExpiationOffences/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.ExpiationOffences == null)
            {
                return NotFound();
            }

            var expiationOffence = await _context.ExpiationOffences
                .FirstOrDefaultAsync(m => m.ExpiationOffenceCode == id);
            if (expiationOffence == null)
            {
                return NotFound();
            }

            return View(expiationOffence);
        }

        // POST: ExpiationOffences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.ExpiationOffences == null)
            {
                return Problem("Entity set 'ExpiationsContext.ExpiationOffences'  is null.");
            }
            var expiationOffence = await _context.ExpiationOffences.FindAsync(id);
            if (expiationOffence != null)
            {
                _context.ExpiationOffences.Remove(expiationOffence);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpiationOffenceExists(string id)
        {
          return _context.ExpiationOffences.Any(e => e.ExpiationOffenceCode == id);
        }
    }
}
