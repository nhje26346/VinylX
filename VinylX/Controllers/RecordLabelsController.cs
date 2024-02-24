using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VinylX.Data;
using VinylX.Models;

namespace VinylX.Controllers
{
    public class RecordLabelsController : Controller
    {
        private readonly VinylXContext _context;

        public RecordLabelsController(VinylXContext context)
        {
            _context = context;
        }

        // GET: RecordLabels
        public async Task<IActionResult> Index()
        {
            return View(await _context.RecordLabel.ToListAsync());
        }

        // GET: RecordLabels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recordLabel = await _context.RecordLabel
                .FirstOrDefaultAsync(m => m.RecordLabelId == id);
            if (recordLabel == null)
            {
                return NotFound();
            }

            return View(recordLabel);
        }

        // GET: RecordLabels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RecordLabels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecordLabelId,LabelName,LabelSubdivision")] RecordLabel recordLabel)
        {
            if (ModelState.IsValid)
            {
                recordLabel.RecordLabelId = Guid.NewGuid();
                _context.Add(recordLabel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recordLabel);
        }

        // GET: RecordLabels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recordLabel = await _context.RecordLabel.FindAsync(id);
            if (recordLabel == null)
            {
                return NotFound();
            }
            return View(recordLabel);
        }

        // POST: RecordLabels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RecordLabelId,LabelName,LabelSubdivision")] RecordLabel recordLabel)
        {
            if (id != recordLabel.RecordLabelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recordLabel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecordLabelExists(recordLabel.RecordLabelId))
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
            return View(recordLabel);
        }

        // GET: RecordLabels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recordLabel = await _context.RecordLabel
                .FirstOrDefaultAsync(m => m.RecordLabelId == id);
            if (recordLabel == null)
            {
                return NotFound();
            }

            return View(recordLabel);
        }

        // POST: RecordLabels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var recordLabel = await _context.RecordLabel.FindAsync(id);
            if (recordLabel != null)
            {
                _context.RecordLabel.Remove(recordLabel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecordLabelExists(Guid id)
        {
            return _context.RecordLabel.Any(e => e.RecordLabelId == id);
        }
    }
}
