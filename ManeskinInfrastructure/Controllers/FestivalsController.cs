using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManeskinDomain.Model;
using ManeskinInfrastructure;

/*namespace ManeskinInfrastructure.Controllers
{
    public class FestivalsController : Controller
    {
        private readonly ManeskinWContext _context;

        public FestivalsController(ManeskinWContext context)
        {
            _context = context;
        }

        // GET: Festivals
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Index", "Peromances");
            //знаходження за катнгорією
            ViewBag.PerfomanceId = id;
            ViewBag.Date = name;

            var maneskinWContext = _context.Festivals.Where(a => a.PerfomanceId == id).Include(a => a.PerfomanceId);
            return View(await maneskinWContext.ToListAsync());
        }

        // GET: Festivals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var festival = await _context.Festivals
                .Include(f => f.Location)
                .FirstOrDefaultAsync(m => m.FestivalId == id);
            if (festival == null)
            {
                return NotFound();
            }

            return View(festival);
        }

        // GET: Festivals/Create
        public IActionResult Create(int perfomanceId)
        {
            //ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "City");
            ViewBag.PerfomanceId = perfomanceId;
            ViewBag.Date = _context.Perfomances.Where(c => c.PerfomanceId == perfomanceId).FirstOrDefault().Date;
            return View();
        }

        // POST: Festivals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int perfomanceId, [Bind("FestivalId,PerfomanceId,FestivalName,Date,LocationId")] Festival festival)
        {
            if (ModelState.IsValid)
            {
                _context.Add(festival);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Festivals", new { id = perfomanceId, name = _context.Perfomances.Where(c => c.PerfomanceId == perfomanceId).FirstOrDefault().Date });
            }
           //ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "City", festival.LocationId);
            return RedirectToAction("Index", "Festivals", new { id = perfomanceId, name = _context.Perfomances.Where(c => c.PerfomanceId == perfomanceId).FirstOrDefault().Date });
            //return View(festival);
        }

        // GET: Festivals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var festival = await _context.Festivals.FindAsync(id);
            if (festival == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "City", festival.LocationId);
            return View(festival);
        }

        // POST: Festivals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FestivalId,PerfomanceId,FestivalName,Date,LocationId")] Festival festival)
        {
            if (id != festival.FestivalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(festival);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FestivalExists(festival.FestivalId))
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
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "City", festival.LocationId);
            return View(festival);
        }

        // GET: Festivals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var festival = await _context.Festivals
                .Include(f => f.Location)
                .FirstOrDefaultAsync(m => m.FestivalId == id);
            if (festival == null)
            {
                return NotFound();
            }

            return View(festival);
        }

        // POST: Festivals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var festival = await _context.Festivals.FindAsync(id);
            if (festival != null)
            {
                _context.Festivals.Remove(festival);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FestivalExists(int id)
        {
            return _context.Festivals.Any(e => e.FestivalId == id);
        }
    }
}
*/