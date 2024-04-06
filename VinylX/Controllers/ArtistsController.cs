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
using VinylX.Repositories;

namespace VinylX.Controllers
{
    [Authorize]
    public class ArtistsController : Controller
    {
        //private readonly VinylXContext _context;
        private readonly IRepositoryFoundation repositoryFoundation;
        private readonly IRepository<Artist> artistRepository;

        //public ArtistsController(VinylXContext context)
        //{
        //    _context = context;
        //}
        public ArtistsController(IRepositoryFoundation repositoryFoundation, IRepository<Artist> artistRepository)
        {
            this.repositoryFoundation = repositoryFoundation;
            this.artistRepository = artistRepository;
        }

        // GET: Artists
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Artist.ToListAsync());
            return View(await artistRepository.Queryable.ToListAsync());
        }

        // GET: Artists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var artist = await _context.Artist
            var artist = await artistRepository.Queryable
                .FirstOrDefaultAsync(m => m.ArtistId == id);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        // GET: Artists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Artists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArtistId,ArtistName,DiscogArtistId")] Artist artist)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(artist);
                artistRepository.Add(artist);
                //await _context.SaveChangesAsync();
                await repositoryFoundation.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(artist);
        }

        // GET: Artists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var artist = await _context.Artist.FindAsync(id);
            var artist = await artistRepository.FindAsync(id);
            if (artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }

        // POST: Artists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArtistId,ArtistName,DiscogArtistId")] Artist artist)
        {
            if (id != artist.ArtistId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(artist);
                    artistRepository.Update(artist);
                    //await _context.SaveChangesAsync();
                    await repositoryFoundation.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistExists(artist.ArtistId))
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
            return View(artist);
        }

        // GET: Artists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var artist = await _context.Artist
            var artist = await artistRepository.Queryable
                .FirstOrDefaultAsync(m => m.ArtistId == id);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        // POST: Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var artist = await _context.Artist.FindAsync(id);
            var artist = await artistRepository.FindAsync(id);
            if (artist != null)
            {
                //_context.Artist.Remove(artist);
                artistRepository.Remove(artist);
            }

            //await _context.SaveChangesAsync();
            await repositoryFoundation.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtistExists(int id)
        {
            //return _context.Artist.Any(e => e.ArtistId == id);
            return artistRepository.Queryable.Any(e => e.ArtistId == id);
        }
    }
}
