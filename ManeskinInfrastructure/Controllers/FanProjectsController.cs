using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManeskinDomain.Model;
using ManeskinInfrastructure;

namespace ManeskinInfrastructure.Controllers
{
    public class FanProjectsController : Controller
    {
        private readonly ManeskinWContext _context;

        public FanProjectsController(ManeskinWContext context)
        {
            _context = context;
        }

        // GET: FanProjects
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Index", "FanProjects");
            //знаходження за катнгорією
            ViewBag.PerfomanceId = id;
            ViewBag.Date = name;

            var maneskinWContext = _context.FanProjects.Where(a => a.PerfomanceId == id).Include(a => a.Perfomance);
            return View(await maneskinWContext.ToListAsync());
        }

        // GET: FanProjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fanProject = await _context.FanProjects
                .Include(f => f.Perfomance)
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (fanProject == null)
            {
                return NotFound();
            }

            return View(fanProject);
        }

        // GET: FanProjects/Create
        public IActionResult Create(int perfomanceId)
        {
            //ViewData["PerfomanceId"] = new SelectList(_context.Perfomances, "PerfomanceId", "PerfomanceId");
            ViewBag.PerfomanceId = perfomanceId;
            ViewBag.Date = _context.Perfomances.Where(c => c.PerfomanceId == perfomanceId).FirstOrDefault().Date;
            return View();
        }

        // POST: FanProjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int perfomanceId, [Bind("ProjectId,PerfomanceId,ProjectName,Date")] FanProject fanProject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fanProject);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "FanProjects", new { id = perfomanceId, name = _context.Perfomances.Where(c => c.PerfomanceId == perfomanceId).FirstOrDefault().Date });
            }

            return RedirectToAction("Index", "FanProjects", new { id = perfomanceId, name = _context.Perfomances.Where(c => c.PerfomanceId == perfomanceId).FirstOrDefault().Date });
        }

        // GET: FanProjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fanProject = await _context.FanProjects.FindAsync(id);
            if (fanProject == null)
            {
                return NotFound();
            }
            ViewData["PerfomanceId"] = new SelectList(_context.Perfomances, "PerfomanceId", "PerfomanceId", fanProject.PerfomanceId);
            return View(fanProject);
        }

        // POST: FanProjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectId,PerfomanceId,ProjectName,Date")] FanProject fanProject)
        {
            if (id != fanProject.ProjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fanProject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FanProjectExists(fanProject.ProjectId))
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
            ViewData["PerfomanceId"] = new SelectList(_context.Perfomances, "PerfomanceId", "PerfomanceId", fanProject.PerfomanceId);
            return View(fanProject);
        }

        // GET: FanProjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fanProject = await _context.FanProjects
                .Include(f => f.Perfomance)
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (fanProject == null)
            {
                return NotFound();
            }

            return View(fanProject);
        }

        // POST: FanProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fanProject = await _context.FanProjects.FindAsync(id);
            if (fanProject != null)
            {
                _context.FanProjects.Remove(fanProject);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FanProjectExists(int id)
        {
            return _context.FanProjects.Any(e => e.ProjectId == id);
        }
    }
}
