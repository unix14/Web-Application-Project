using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using shauli.Models;

namespace shauli.Controllers
{
    public class FansController : Controller
    {
        private ApplicationDbContext _context;

        public FansController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Fans
        public IActionResult Index()
        {
            return View(_context.Fan.ToList());
        }

        // GET: Fans/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Fan fan = _context.Fan.SingleOrDefault(m => m.ID == id);
            if (fan == null)
            {
                return HttpNotFound();
            }

            return View(fan);
        }

        // GET: Fans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fans/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Fan fan)
        {
            if (ModelState.IsValid)
            {
                _context.Fan.Add(fan);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fan);
        }

        // GET: Fans/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Fan fan = _context.Fan.SingleOrDefault(m => m.ID == id);
            if (fan == null)
            {
                return HttpNotFound();
            }
            return View(fan);
        }

        // POST: Fans/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Fan fan)
        {
            if (ModelState.IsValid)
            {
                _context.Update(fan);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fan);
        }

        // GET: Fans/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Fan fan = _context.Fan.SingleOrDefault(m => m.ID == id);
            if (fan == null)
            {
                return HttpNotFound();
            }

            return View(fan);
        }

        // POST: Fans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Fan fan = _context.Fan.SingleOrDefault(m => m.ID == id);
            _context.Fan.Remove(fan);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
