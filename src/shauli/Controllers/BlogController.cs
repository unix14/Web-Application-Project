using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using shauli.Models;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;
using shauli.Controllers;
using shauli.ViewModels;
using System.Security.Claims;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace shauli.Controllers
{
    public class BlogController : Controller
    {

        [FromServices]
        public ApplicationDbContext _context { get; set; }

        [FromServices]
        public ILogger<BlogController> Logger { get; set; }

        public BlogController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_context.Posts.Include(p => p.Comments).ToList());


        }
        //GET:/AdminPanel/
        public IActionResult AdminPanel()
        {
            if (User.IsSignedIn())
            {
                List<Post> post = _context.Posts.ToList();
                return View(post);
            }
            return RedirectToAction("Index");
        }
        
        // GET: Blog/CreateAdmin
        public IActionResult CreateAdmin()
        {
            if (User.IsSignedIn())
            {
                 return View();
            }
            return RedirectToAction("Index");
        }
        
        // GET: Post/Details/5
        public async Task<ActionResult> PostDetails(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            CommentsPosts cp = new CommentsPosts();
            cp.posts = await _context.Posts.Include(p => p.Comments).SingleOrDefaultAsync(p => p.PostID == id);

            if (cp.posts == null)
            {
                return HttpNotFound();
            }


            return View(cp);
        }

        // GET: Post/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAdmin(Admin admin)
        {
            if (User.IsSignedIn())
            {
                if (ModelState.IsValid)
                {
                    _context.Admins.Add(admin);
                    _context.SaveChanges();
                    return RedirectToAction("AdminPanel");
                }
                return View(admin);

            }
            return RedirectToAction("Index");
        
        }
        public IActionResult ManageFans()
        {
            if (User.IsSignedIn())
            {
                List<Fan> fans = _context.Fan.ToList();
                return View(fans);
            }
            return RedirectToAction("Index");
        }

        // POST: Post/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Posts.Add(post);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }


        //ManageAdmins
        public IActionResult ManageAdmins()
        {
                if (User.IsSignedIn())
                {
                List<Admin> admins = _context.Admins.ToList();
                return View(admins);
                }
                return RedirectToAction("Index");
        }
        //GET Blog/AdminPanel/EditAdmin
        public IActionResult EditAdmin(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Admin admin = _context.Admins.Single(m => m.ID == id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }
        // GET: Post/Edit/5
        public IActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Post post = _context.Posts.Single(m => m.PostID == id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }
        // POST: Post/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditAdmin(Admin admin)
        {
            if (ModelState.IsValid)
            {
                _context.Update(admin);
                _context.SaveChanges();
                return RedirectToAction("AdminPanel");
            }
            return View(admin);
        }
        //
        // GET: Blog/ManagePost/5
        public IActionResult ManagePost()
        {
            if (User.IsSignedIn())
            {
                return View();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult ManagePost(int? id)
        {
            if (User.IsSignedIn())
            {
                Post post = _context.Posts.Single(s =>s.PostID == id);
                return View(post);
            }
            return RedirectToAction("Index");
        }


        // GET: Post/Delete/5
        [ActionName("DeleteAdmin")]
        public IActionResult DeleteAdmin(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Admin admin = _context.Admins.Single(m => m.ID == id);
            if (admin == null)
            {
                return HttpNotFound();
            }

            return View(admin);
        }

        // POST:Blog/DeleteAdminConfirmed/5
        [HttpPost, ActionName("DeleteAdmin")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAdminConfirmed(int id)
        {
            Admin admin = _context.Admins.Single(m => m.ID == id);
            _context.Admins.Remove(admin);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        // POST: Post/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost(Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Update(post);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Post/Delete/5
        [ActionName("DeletePost")]
        public IActionResult DeletePost(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Post post = _context.Posts.Single(m => m.PostID == id);
            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Post post = _context.Posts.Single(m => m.PostID == id);
            _context.Posts.Remove(post);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        //---Comments---

        // GET: /<controller>/
        public ActionResult GetPostComments(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            List<Comment> comments = _context.Comments.Where(c => c.PostID == id).ToList();

            return View(comments);
        }



        // GET: Comment/Delete/5
        [ActionName("DeleteComment")]
        public IActionResult DeleteComment(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Comment comment = _context.Comments.Single(m => m.CommentID == id);
            if (comment == null)
            {
                return HttpNotFound();
            }

            return View(comment);
        }

        // POST: Comment/Delete/5
        [HttpPost, ActionName("DeleteComment")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCommentConfirmed(int id)
        {
            Comment comment = _context.Comments.Single(m => m.CommentID == id);
            int a = comment.PostID;
            _context.Comments.Remove(comment);
            _context.SaveChanges();
            return RedirectToAction("Comments", a);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(string id, CommentsPosts comment)
        {
            if (ModelState.IsValid)
            {
                comment.comments.PostID = Int32.Parse(id);
                _context.Comments.Add(comment.comments);
                _context.SaveChanges();
                return RedirectToAction("Details", comment.comments.PostID);
            }
            return View(comment);
        }

        //AdminDetails
        // GET: Blog/AdminDetails/5
        public ActionResult AdminDetails(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Admin admin = _context.Admins.Single(p => p.ID == id);

            if (admin == null)
            {
                return HttpNotFound();
            }


            return View(admin);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AdminDetails(string id, Admin admin)
        {
            if (ModelState.IsValid)
            {
                admin.ID = Int32.Parse(id);
                _context.Admins.Add(admin);
                _context.SaveChanges();
                return RedirectToAction("AdminDetails", admin.ID);
            }
            return View(admin);
        }
        // POST: BlogController/AddComment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddComment(Comment comment)
        {
            if (ModelState.IsValid)
            {
                _context.Comments.Add(comment);
                _context.SaveChanges();
            }
                return RedirectToAction("Index");
            //return View(comment);
        }

    }
}

