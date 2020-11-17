using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class BikesController : Controller
    {
        private ApplicationDbContext _context;

        public BikesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Bikes
        public ViewResult Index()
        {
            var bikes = _context.Bike.ToList(); ;
            return View(bikes);
        }
        //private IEnumerable<Bike> GetBikes()
        //{
        //    return new List<Bike> 
        //    { new Bike { Brand = "Trek", Model = "Emonda", Price=150},
        //      new Bike { Brand = "Giant", Model = "TCR", Price=140}
        //    };
        //}
    }
}