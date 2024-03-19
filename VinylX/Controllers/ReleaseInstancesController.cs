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
    public class ReleaseInstancesController : Controller
    {
        private readonly VinylXContext _context;

        public ReleaseInstancesController(VinylXContext context)
        {
            _context = context;
        }

        // GET: ReleaseInstances
        public async Task<IActionResult> Index()
        {
            return View(await _context.ReleaseInstance.ToListAsync());
        }

        // GET: ReleaseInstances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var releaseInstance = await _context.ReleaseInstance
                .FirstOrDefaultAsync(m => m.ReleaseInstanceId == id);
            if (releaseInstance == null)
            {
                return NotFound();
            }

            return View(releaseInstance);
        }

        // GET: ReleaseInstances/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReleaseInstances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReleaseInstanceId,Quality")] ReleaseInstance releaseInstance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(releaseInstance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(releaseInstance);
        }

        // GET: ReleaseInstances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var releaseInstance = await _context.ReleaseInstance.FindAsync(id);
            if (releaseInstance == null)
            {
                return NotFound();
            }
            return View(releaseInstance);
        }

        // POST: ReleaseInstances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReleaseInstanceId,Quality")] ReleaseInstance releaseInstance)
        {
            if (id != releaseInstance.ReleaseInstanceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(releaseInstance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReleaseInstanceExists(releaseInstance.ReleaseInstanceId))
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
            return View(releaseInstance);
        }

        // GET: ReleaseInstances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var releaseInstance = await _context.ReleaseInstance
                .FirstOrDefaultAsync(m => m.ReleaseInstanceId == id);
            if (releaseInstance == null)
            {
                return NotFound();
            }

            return View(releaseInstance);
        }

        // POST: ReleaseInstances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var releaseInstance = await _context.ReleaseInstance.FindAsync(id);
            if (releaseInstance != null)
            {
                _context.ReleaseInstance.Remove(releaseInstance);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReleaseInstanceExists(int id)
        {
            return _context.ReleaseInstance.Any(e => e.ReleaseInstanceId == id);
        }
    }
}
