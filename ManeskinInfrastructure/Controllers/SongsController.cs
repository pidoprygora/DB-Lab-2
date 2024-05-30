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
    public class SongsController : Controller
    {
        private readonly ManeskinWContext _context;

        public SongsController(ManeskinWContext context)
        {
            _context = context;
        }

        // GET: Songs
        public async Task<IActionResult> Index(int? id, int? name)
        {
            if(id == null) return RedirectToAction("Albums", "Index");
            //знаходження за катнгорією
            ViewBag.AlbumId = id;
           

            var maneskinWContext = _context.Songs.Where(a => a.AlbumId == id).Include(a => a.Album);
            return View(await maneskinWContext.ToListAsync());
        }

        // GET: Songs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .Include(s => s.Album)
                .FirstOrDefaultAsync(m => m.SongId == id);
            if (song == null)
            {
                return NotFound();
            }

            //return View(song);
            return RedirectToAction("Index", "Clips", new { id = song.SongId, name = song.Tittle });
        }

        // GET: Songs/Create
        public IActionResult Create(int albumId)
        {
            //ViewData["AlbumId"] = new SelectList(_context.Albumsses, "AlbumId", "Tittle");
            ViewBag.AlbumId = albumId;
            ViewBag.AlbumName = _context.Albumsses.Where(c => c.AlbumId == albumId).FirstOrDefault().Tittle;

            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int albumId, [Bind("SongId,Tittle,AlbumId,Duration,DataRelease")] Song song)
        {
            song.AlbumId = albumId;
            if (ModelState.IsValid)
            {
                _context.Add(song);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Songs", new { id = albumId, name = _context.Albumsses.Where(c => c.AlbumId == albumId).FirstOrDefault().Tittle });
            }
            //ViewData["AlbumId"] = new SelectList(_context.Albumsses, "AlbumId", "Tittle", song.AlbumId);
            //return View(song);
            return RedirectToAction("Index", "Songs", new { id = albumId, name = _context.Albumsses.Where(c => c.AlbumId == albumId).FirstOrDefault().Tittle });
        }

        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            ViewData["AlbumId"] = new SelectList(_context.Albumsses, "AlbumId", "Tittle", song.AlbumId);
            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SongId,Tittle,AlbumId,Duration,DataRelease")] Song song)
        {
            if (id != song.SongId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(song);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(song.SongId))
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
            ViewData["AlbumId"] = new SelectList(_context.Albumsses, "AlbumId", "Tittle", song.AlbumId);
            return View(song);
        }

        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .Include(s => s.Album)
                .FirstOrDefaultAsync(m => m.SongId == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song != null)
            {
                _context.Songs.Remove(song);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongExists(int id)
        {
            return _context.Songs.Any(e => e.SongId == id);
        }
    }
}
