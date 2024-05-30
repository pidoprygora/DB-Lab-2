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
    public class PerfomancesController : Controller
    {
        private readonly ManeskinWContext _context;

        public PerfomancesController(ManeskinWContext context)
        {
            _context = context;
        }

        // GET: Perfomances
        public async Task<IActionResult> Index(int? id, string? name)
        {

            if (id == null) return RedirectToAction("Tours", "Index");
            ViewBag.TourId = id;
            ViewBag.TourName = name;
            var tourPer = _context.Perfomances.Where(b => b.TourId == id).Include(b => b.Tour).Include(b => b.Location);

            return View(await tourPer.ToListAsync());
        }



        // GET: Perfomances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perfomance = await _context.Perfomances
                .Include(p => p.Location)
                .Include(p => p.TourId)
                .FirstOrDefaultAsync(m => m.PerfomanceId == id);
            if (perfomance == null)
            {
                return NotFound();
            }


            return RedirectToAction("Index", "Fanprojects", new { id = perfomance.PerfomanceId, name = perfomance.Date });
            //return View(perfomance);
        }

       

        // GET: Perfomances/Create
        public IActionResult Create(int locationId, int tourId)
        {
            //ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "City");
            if (locationId != null)
            {
                ViewBag.LocationId = locationId;
                ViewBag.City = _context.Locations
                    .Where(c => c.LocationId == locationId)
                    .Select(c => c.City)
                    .FirstOrDefault();
            }
            else if (tourId != null)
            {
                ViewBag.LocationId = tourId;
                ViewBag.City = _context.Tours
                    .Where(c => c.TourId == tourId)
                    .Select(c => c.NameTour)
                    .FirstOrDefault();
            }

            return View();
        }

        // POST: Perfomances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int locationId, int tourId, [Bind("PerfomanceId,LocationId,Date,Information,TourId,FestivalId")] Perfomance perfomance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(perfomance);
                await _context.SaveChangesAsync();
                if (locationId != null)
                {
                    var locationCity = _context.Locations
                        .Where(c => c.LocationId == locationId)
                        .Select(c => c.City)
                        .FirstOrDefault();

                    return RedirectToAction("Index", "Perfomances", new { id = locationId, name = locationCity });
                }
                else if (tourId != null)
                {
                    var tourName = _context.Tours
                        .Where(c => c.TourId == tourId)
                        .Select(c => c.NameTour)
                        .FirstOrDefault();

                    return RedirectToAction("Index", "Tours", new { id = tourId, name = tourName });
                }
            }
            //ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "City", perfomance.LocationId);
            return RedirectToAction("Index", "Tours", new { id = tourId, name = _context.Tours.Where(c => c.TourId == locationId).FirstOrDefault().NameTour });
        }

        // GET: Perfomances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perfomance = await _context.Perfomances.FindAsync(id);
            if (perfomance == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "City", perfomance.LocationId);
            return View(perfomance);
        }

        // POST: Perfomances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PerfomanceId,LocationId,Date,Information,TourId,FestivalId")] Perfomance perfomance)
        {
            if (id != perfomance.PerfomanceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(perfomance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerfomanceExists(perfomance.PerfomanceId))
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
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "City", perfomance.LocationId);
            return View(perfomance);
        }

        // GET: Perfomances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perfomance = await _context.Perfomances
                .Include(p => p.Location)
                .FirstOrDefaultAsync(m => m.PerfomanceId == id);
            if (perfomance == null)
            {
                return NotFound();
            }

            return View(perfomance);
        }

        // POST: Perfomances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var perfomance = await _context.Perfomances.FindAsync(id);
            if (perfomance != null)
            {
                _context.Perfomances.Remove(perfomance);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PerfomanceExists(int id)
        {
            return _context.Perfomances.Any(e => e.PerfomanceId == id);
        }
    }
}
