using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieDB.Data;

namespace MovieDB.Controllers
{
    [Area("Users")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _db.MoviesDb.ToListAsync();
            return View(movies);
        }

        [HttpGet]
        public async Task<IActionResult> Index(string MovieSearch)
        {
            ViewData["GetMovies"] = MovieSearch;
            var moviequery = from x in _db.MoviesDb select x;
            if (!String.IsNullOrEmpty(MovieSearch))
            {
                moviequery = moviequery.Where(x => x.Name.Contains(MovieSearch));
            }
            return View(await moviequery.AsNoTracking().ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> Detail(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var movie = await _db.MoviesDb.FindAsync(id);
            return View(movie);
        }

        //for categories
        public async Task<IActionResult> ActionMovies()
        {
            var movies = await _db.MoviesDb.Where(c => c.Category == "0").ToListAsync();
            return View(movies);
        }

        public async Task<IActionResult> AdventureMovies()
        {
            var movies = await _db.MoviesDb.Where(c => c.Category == "1").ToListAsync();
            return View(movies);
        }

        public async Task<IActionResult> HorrorMovies()
        {
            var movies = await _db.MoviesDb.Where(c => c.Category == "2").ToListAsync();
            return View(movies);
        }

        public async Task<IActionResult> DramaMovies()
        {
            var movies = await _db.MoviesDb.Where(c => c.Category == "3").ToListAsync();
            return View(movies);
        }

        public async Task<IActionResult> RomanceMovies()
        {
            var movies = await _db.MoviesDb.Where(c => c.Category == "4").ToListAsync();
            return View(movies);
        }

        public async Task<IActionResult> ComedyMovies()
        {
            var movies = await _db.MoviesDb.Where(c => c.Category == "5").ToListAsync();
            return View(movies);
        }
    }
}
