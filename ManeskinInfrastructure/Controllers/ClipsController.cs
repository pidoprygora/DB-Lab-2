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
    public class ClipsController : Controller
    {
        private readonly ManeskinWContext _context;

        public ClipsController(ManeskinWContext context)
        {
            _context = context;
        }

        // GET: Clips
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Songs", "Index");
            //знаходження за катнгорією
            ViewBag.SongId = id;
            ViewBag.Tittle = name;

            var maneskinWContext = _context.Clips.Where(a => a.SongId == id).Include(a => a.Song);
            return View(await maneskinWContext.ToListAsync());
        }

        // GET: Clips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clip = await _context.Clips
                .Include(c => c.Song)
                .FirstOrDefaultAsync(m => m.VideoId == id);
            if (clip == null)
            {
                return NotFound();
            }

            return View(clip);
        }

        // GET: Clips/Create
        public IActionResult Create(int songId)
        {
            //ViewData["SongId"] = new SelectList(_context.Songs, "SongId", "Tittle");
            ViewBag.SongId = songId;
            ViewBag.SongName = _context.Songs.Where(c => c.SongId == songId).FirstOrDefault().Tittle;
            return View();
        }

        // POST: Clips/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int songId, [Bind("VideoId,SongId,DataRelease,MadeBy")] Clip clip)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clip);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Clips", new { id = songId, name = _context.Songs.Where(c => c.SongId == songId).FirstOrDefault().Tittle });
            }
            //ViewData["SongId"] = new SelectList(_context.Songs, "SongId", "Tittle", clip.SongId);
            return RedirectToAction("Index", "Clips", new { id = songId, name = _context.Songs.Where(c => c.SongId == songId).FirstOrDefault().Tittle });
            //return View(clip);
        }

        // GET: Clips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clip = await _context.Clips.FindAsync(id);
            if (clip == null)
            {
                return NotFound();
            }
            ViewData["SongId"] = new SelectList(_context.Songs, "SongId", "Tittle", clip.SongId);
            return View(clip);
        }

        // POST: Clips/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VideoId,SongId,DataRelease,MadeBy")] Clip clip)
        {
            if (id != clip.VideoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClipExists(clip.VideoId))
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
            ViewData["SongId"] = new SelectList(_context.Songs, "SongId", "Tittle", clip.SongId);
            return View(clip);
        }

        // GET: Clips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clip = await _context.Clips
                .Include(c => c.Song)
                .FirstOrDefaultAsync(m => m.VideoId == id);
            if (clip == null)
            {
                return NotFound();
            }

            return View(clip);
        }

        // POST: Clips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clip = await _context.Clips.FindAsync(id);
            if (clip != null)
            {
                _context.Clips.Remove(clip);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClipExists(int id)
        {
            return _context.Clips.Any(e => e.VideoId == id);
        }
    }
}
