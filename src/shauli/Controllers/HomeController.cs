using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using shauli.Models;
using Microsoft.Data.Entity;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace shauli.Controllers
{
    public class HomeController : Controller
    {
        [FromServices]
        public ApplicationDbContext _context { get; set; }

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_context.Posts.Include(p => p.Comments).ToList());

        }
        public IActionResult Login()
        {
            return View();

        }
        public IActionResult About()
        {
            return View();

        }
        [HttpPost]
        public IActionResult Login(Admin admin)
        {
            if (_context.Admins.First().Username == admin.Username && _context.Admins.First().Password == admin.Password)
            {
                return RedirectToAction("AdminPanel", "Blog");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public IActionResult Statistics()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
