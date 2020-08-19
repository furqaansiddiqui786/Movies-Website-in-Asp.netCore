using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieDB.Data;
using MovieDB.Models;
using MovieDB.Utility;

namespace MovieDB.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.Admin)]
    [Area("Admin")]
    public class Dashboard : Controller
    {
        private readonly ApplicationDbContext _db;

        public Dashboard(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public MoviesDB MovieDB { get; set; }




        public async Task<IActionResult> Index(MoviesDB result)
        {
            var movies = await _db.MoviesDb.ToListAsync();
            return View(movies);
        }

       

 
        //create button action
        public IActionResult Add()
        {
            return View();
        }

        //saving in db
        [HttpPost, ActionName("Add")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Added(MoviesDB movie)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if(files.Count() > 0)
                {
                    byte[] p1 = null;
                    using(var fs1 = files[0].OpenReadStream())
                    {
                        using(var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    }
                    movie.Banner = p1;
                }
                _db.MoviesDb.Add(movie);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        //detail view
        public async Task<IActionResult> Movie(int? id, MoviesDB cat)
        {
            if(id == null)
            {
                return NotFound();
            }
            
            var movie = await _db.MoviesDb.FindAsync(id);
            return View(movie);
        }

        //edit page
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var movie = await _db.MoviesDb.FindAsync(id);
            return View(movie);
        }

        //edit and save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MoviesDB mov)
        {
            if(mov.Id == null)
            {
                return NotFound();
            }
            var movieFromDb = await _db.MoviesDb.Where(c => c.Id == mov.Id).FirstOrDefaultAsync();

            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    }
                    movieFromDb.Banner = p1;
                }
                movieFromDb.Name = mov.Name;
                movieFromDb.About = mov.About;
                movieFromDb.Category = mov.Category;
                movieFromDb.Director = mov.Director;
                movieFromDb.Actors = mov.Actors;
                movieFromDb.Producer = mov.Producer;
                movieFromDb.Watchtime = mov.Watchtime;
                movieFromDb.Release = mov.Release;
                movieFromDb.Ratings = mov.Ratings;
                movieFromDb.MovieLink = mov.MovieLink;

                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mov);
        }

        public async Task<IActionResult> Delete(MoviesDB mov)
        {
            if(mov.Id == null)
            {
                return NotFound();
            }
            _db.MoviesDb.Remove(mov);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
