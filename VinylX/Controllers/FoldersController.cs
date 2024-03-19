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
using VinylX.Services;

namespace VinylX.Controllers
{
    [Authorize]
    public class FoldersController : Controller
    {
        private readonly VinylXContext _context;
        private readonly IUserService userService;

        public FoldersController(VinylXContext context, IUserService userService)
        {
            _context = context;
            this.userService = userService;
        }

        // GET: Folders
        public async Task<IActionResult> Index()
        {
            var user = await userService.GetLoggedInUser()!;

            return View(_context.Folder.Where(f => f.User.UserId == user.UserId));
        }

        // GET: Folders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var folder = await _context.Folder
                .FirstOrDefaultAsync(m => m.FolderId == id);

            if (folder == null)
            {
                return NotFound();
            }

            var releaseIntances = _context.ReleaseInstance
                .Include(r => r.Release)
                .Include(r => r.Release.MasterRelease)
                .Include(r => r.Release.MasterRelease.Artist)
                .Where(r => r.Folder.FolderId == folder.FolderId);

            var folderAndRelaesesModel = new FolderAndReleases(folder, releaseIntances);

            return View(folderAndRelaesesModel);
        }

        // GET: Folders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Folders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FolderId,FolderName,User")] Folder folder)
        {
            var user = await userService.GetLoggedInUser();
            if (user == null)
            {
                throw new Exception("User not logged in.");
            }
            folder.User = user;


            //if (ModelState.IsValid)
            //{
                

                _context.Add(folder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //return View(folder);
        }

        // GET: Folders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var folder = await _context.Folder.FindAsync(id);
            if (folder == null)
            {
                return NotFound();
            }
            return View(folder);
        }

        // POST: Folders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FolderId,FolderName")] Folder folder)
        {
            if (id != folder.FolderId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(folder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FolderExists(folder.FolderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            //return View(folder);
        }

        // GET: Folders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var folder = await _context.Folder
                .FirstOrDefaultAsync(m => m.FolderId == id);
            if (folder == null)
            {
                return NotFound();
            }

            return View(folder);
        }

        // POST: Folders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var folder = await _context.Folder.FindAsync(id);
            if (folder != null)
            {
                _context.Folder.Remove(folder);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FolderExists(int id)
        {
            return _context.Folder.Any(e => e.FolderId == id);
        }

        public class FolderAndReleases
        {
            public Folder Folder { get; set; }
            public IEnumerable<ReleaseInstance> ReleaseInstances { get; set; }  

            public FolderAndReleases(Folder folder, IEnumerable<ReleaseInstance> releaseInstances)
            {
                Folder = folder;
                ReleaseInstances = releaseInstances;
            }
        }
    }
}
