using System.Collections.Generic;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;
using System.Linq;
using System;
//using Vidly.Migrations;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ViewResult Index()
        {
            var movies = _context.Movie.Include(n => n.Genre).ToList();

            return View(movies);    
        }
        public ActionResult Details(int id)
        {
            var movies = _context.Movie.Include(n => n.Genre).SingleOrDefault(c => c.Id == id);

            if (movies == null)
                return HttpNotFound();

            return View(movies);
        }
        public ActionResult New()
        {
            var genreTypes = _context.Genre.ToList();
            var movieModel = new MovieFormViewModel()
            {
                Genres = genreTypes
            };
            return View("MovieForm", movieModel);
        }
        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            if(movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movie.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movie.Single(n => n.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }
        public ActionResult Edit(int id)
        {
            var movie = _context.Movie.SingleOrDefault(x => x.Id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            var movieModel = new MovieFormViewModel()
            {
                Movie = movie,
                Genres = _context.Genre.ToList()
            };

            return View("MovieForm", movieModel);
        }

    }
}