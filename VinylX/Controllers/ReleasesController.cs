using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VinylX.Data;
using VinylX.Models;
using VinylX.Services;
using X.PagedList;


namespace VinylX.Controllers
{
    [Authorize]
    public class ReleasesController : Controller
    {
        private readonly VinylXContext _context;
        private readonly IUserService userService;

        public ReleasesController(VinylXContext context, IUserService userService)
        {
            _context = context;
            this.userService = userService;
        }

        // GET: Releases
        public async Task<IActionResult> Index(int? page, bool resetSearch, string? search)
        {
            if (resetSearch)
            {
                if (string.IsNullOrEmpty(search))
                {
                    HttpContext.Session.Remove("search");
                }
                else
                {
                    HttpContext.Session.SetString("search", search);
                }
            }

            var searchString = HttpContext.Session.GetString("search");

            var releases = _context.Release
                .Include(r => r.MasterRelease)
                .Include(r => r.MasterRelease.Artist)
                .Where(r => string.IsNullOrEmpty(searchString)
                    || EF.Functions.Like(r.MasterRelease.Artist.ArtistName, $"%{searchString}%")); 

            var pageNumber = page ?? 1;
            var onePageOfItems = releases.ToPagedList(pageNumber, 25);

            ViewBag.OnePageOfItems = onePageOfItems;

            //return View(await _context.Release.ToListAsync());
            return View();
        }

        // GET: Releases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var release = await _context.Release
                .FirstOrDefaultAsync(m => m.ReleaseId == id);
            if (release == null)
            {
                return NotFound();
            }

            return View(release);
        }

        // GET: Releases/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Releases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReleaseId,ReleaseDate,CategoryNumber,Edition,Genre,DiscogReleaseId")] Release release)
        {
            if (ModelState.IsValid)
            {
                _context.Add(release);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(release);
        }

        // GET: Releases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var release = await _context.Release.FindAsync(id);
            if (release == null)
            {
                return NotFound();
            }
            return View(release);
        }

        // POST: Releases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReleaseId,ReleaseDate,CategoryNumber,Edition,Genre,DiscogReleaseId")] Release release)
        {
            if (id != release.ReleaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(release);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReleaseExists(release.ReleaseId))
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
            return View(release);
        }

        // GET: Releases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var release = await _context.Release
                .FirstOrDefaultAsync(m => m.ReleaseId == id);
            if (release == null)
            {
                return NotFound();
            }

            return View(release);
        }

        // POST: Releases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var release = await _context.Release.FindAsync(id);
            if (release != null)
            {
                _context.Release.Remove(release);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReleaseExists(int id)
        {
            return _context.Release.Any(e => e.ReleaseId == id);
        }

        public async Task<IActionResult> AddToFolderView(int id)
        {
            var user = await userService.GetLoggedInUser();

            var release = await _context.Release
                .Include(r => r.MasterRelease)
                .Include(r => r.MasterRelease.Artist)
                .SingleAsync(r => r.ReleaseId == id);

            var folders = _context.Folder.Where(f => f.User.UserId == user.UserId);

            var addToFolderModel = new AddToFolderModel(release, folders);

            return View(addToFolderModel);
        }

        public async Task<IActionResult> AddToFolder(int folderId, int releaseId, string quality)
        {
            var folder = _context.Folder.Single(f => f.FolderId == folderId);
            var release = _context.Release.Single(r => r.ReleaseId == releaseId);
            _context.ReleaseInstance.Add(new ReleaseInstance
            {
                Folder = folder,
                Release = release,
                Quality = quality
            });
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public class AddToFolderModel
        {
            public Release Release { get; set; }
            public IEnumerable<Folder> Folders { get; set; }

            public AddToFolderModel(Release release, IEnumerable<Folder> folders)
            {
                Release = release;
                Folders = folders;
            }
        }
    }
}
