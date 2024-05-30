using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManeskinDomain.Model;
using ManeskinInfrastructure;
using ManeskinInfrastructure.Models;
using System.Diagnostics;

using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ManeskinInfrastructure.Controllers
{
    public class QueriesController : Controller
    {

        private readonly ManeskinWContext _context;

        public QueriesController(ManeskinWContext context)
        {
            _context = context;
        }

        // GET: Queries
        public async Task<IActionResult> Index()
        {
            return View(await _context.Query.ToListAsync());
        }

        // GET: Queries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query = await _context.Query
                .FirstOrDefaultAsync(m => m.Id == id);
            if (query == null)
            {
                return NotFound();
            }

            return View(query);
        }

        // GET: Queries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Queries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tittle,Duration")] ManeskinDomain.Model.Query query)
        {
            if (ModelState.IsValid)
            {
                _context.Add(query);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(query);
        }

        // GET: Queries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query = await _context.Query.FindAsync(id);
            if (query == null)
            {
                return NotFound();
            }
            return View(query);
        }

        // POST: Queries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tittle,Duration")] ManeskinDomain.Model.Query query)
        {
            if (id != query.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(query);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QueryExists(query.Id))
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
            return View(query);
        }

        // GET: Queries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query = await _context.Query
                .FirstOrDefaultAsync(m => m.Id == id);
            if (query == null)
            {
                return NotFound();
            }

            return View(query);
        }

        // POST: Queries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var query = await _context.Query.FindAsync(id);
            if (query != null)
            {
                _context.Query.Remove(query);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QueryExists(int id)
        {
            return _context.Query.Any(e => e.Id == id);
        }

        // GET: Queries/Search
        public IActionResult Search()
        {
            return View();
        }

        // POST: Queries/Search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(int duration)
        {

            var songs = await _context.Songs
                 .Where(s => s.Duration == duration)
                 .Select(s => new ManeskinDomain.Model.Query
                 {
                     Tittle = s.Tittle,
                     Duration = s.Duration
                 })
                 .ToListAsync();

            return View("SearchResults", songs);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchCitiesInTwoTours(int tourCount)
        {


            var cities = await _context.Perfomances
                .GroupBy(p => new { p.Location.City, p.Location.Country })
                .Where(g => g.Select(p => p.TourId).Distinct().Count() >= tourCount)
                .Select(g => new ManeskinDomain.Model.Query
                {
                    City = g.Key.City,
                    Country = g.Key.Country,
                    ToursVisited = g.Select(p => p.TourId).Distinct().Count()
                })
                .ToListAsync();

            return View("CitiesResults", cities);
        }


        // POST: Queries/SearchAlbums
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchAlbums(int songCount)
        {

            var albums = await _context.Albumsses
                .Where(a => a.Songs.Count >= songCount)
                .Select(a => new ManeskinDomain.Model.Query
                {
                    AlbumId = a.AlbumId,
                    Tittle = a.Tittle,
                    SongCount = a.Songs.Count
                })
                .ToListAsync();

            if (albums == null || !albums.Any())
            {
                ViewBag.Message = "No albums found with the specified criteria.";
                return View("AlbumResults", new List<ManeskinDomain.Model.Query>());
            }

            return View("AlbumResults", albums);

        }

        // POST: Queries/SearchLocations
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchLocations(int concertCount)
        {

            var locations = await _context.Locations
                .Where(l => l.Perfomances.Count >= concertCount)
                .Select(l => new ManeskinDomain.Model.Query
                {
                    LocationId = l.LocationId,
                    Country = l.Country,
                    City = l.City,
                    ConcertCount = l.Perfomances.Count
                })
                .ToListAsync();

            return View("LocationResults", locations);

        }
    }
}
