using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VinylX.Data;
using VinylX.Models;

namespace VinylX.Controllers
{
    [Authorize]
    public class MasterReleasesController : Controller
    {
        private readonly VinylXContext _context;

        public MasterReleasesController(VinylXContext context)
        {
            _context = context;
        }

        // GET: MasterReleases
        public async Task<IActionResult> Index()
        {
            return View(await _context.MasterRelease.ToListAsync());
        }

        // GET: MasterReleases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var masterRelease = await _context.MasterRelease
                .FirstOrDefaultAsync(m => m.MasterReleaseId == id);
            if (masterRelease == null)
            {
                return NotFound();
            }

            return View(masterRelease);
        }

        // GET: MasterReleases/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MasterReleases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MasterReleaseId,AlbumName,BarcodeNumber,CategoryNumber,DiscogMasterReleaseId")] MasterRelease masterRelease)
        {
            if (ModelState.IsValid)
            {
                _context.Add(masterRelease);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(masterRelease);
        }

        // GET: MasterReleases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var masterRelease = await _context.MasterRelease.FindAsync(id);
            if (masterRelease == null)
            {
                return NotFound();
            }
            return View(masterRelease);
        }

        // POST: MasterReleases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MasterReleaseId,AlbumName,BarcodeNumber,CategoryNumber,DiscogMasterReleaseId")] MasterRelease masterRelease)
        {
            if (id != masterRelease.MasterReleaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(masterRelease);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MasterReleaseExists(masterRelease.MasterReleaseId))
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
            return View(masterRelease);
        }

        // GET: MasterReleases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var masterRelease = await _context.MasterRelease
                .FirstOrDefaultAsync(m => m.MasterReleaseId == id);
            if (masterRelease == null)
            {
                return NotFound();
            }

            return View(masterRelease);
        }

        // POST: MasterReleases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var masterRelease = await _context.MasterRelease.FindAsync(id);
            if (masterRelease != null)
            {
                _context.MasterRelease.Remove(masterRelease);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MasterReleaseExists(int id)
        {
            return _context.MasterRelease.Any(e => e.MasterReleaseId == id);
        }
    }
}
