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
    public class AlbumssesController : Controller
    {
        private readonly ManeskinWContext _context;

        public AlbumssesController(ManeskinWContext context)
        {
            _context = context;
        }

        // GET: Albumsses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Albumsses.ToListAsync());
        }

        // GET: Albumsses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albumss = await _context.Albumsses
                .FirstOrDefaultAsync(m => m.AlbumId == id);
            if (albumss == null)
            {
                return NotFound();
            }

            //return View(albumss);
            return RedirectToAction("Index", "Songs", new { id = albumss.AlbumId, name = albumss.Tittle } );
        }

        // GET: Albumsses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Albumsses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlbumId,Tittle,YearRelease,Length")] Albumss albumss)
        {
            
            if (ModelState.IsValid)
            {
                _context.Add(albumss);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(albumss);
        }

        // GET: Albumsses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albumss = await _context.Albumsses.FindAsync(id);
            if (albumss == null)
            {
                return NotFound();
            }
            return View(albumss);
        }

        // POST: Albumsses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AlbumId,Tittle,YearRelease,Length")] Albumss albumss)
        {
            if (id != albumss.AlbumId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(albumss);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumssExists(albumss.AlbumId))
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
            return View(albumss);
        }

        // GET: Albumsses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albumss = await _context.Albumsses
                .FirstOrDefaultAsync(m => m.AlbumId == id);
            if (albumss == null)
            {
                return NotFound();
            }

            return View(albumss);
        }

        // POST: Albumsses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var albumss = await _context.Albumsses.FindAsync(id);
            if (albumss != null)
            {
                _context.Albumsses.Remove(albumss);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlbumssExists(int id)
        {
            return _context.Albumsses.Any(e => e.AlbumId == id);
        }
    }
}
