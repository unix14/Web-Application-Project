using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using shauli.Models;
using Microsoft.Data.Entity;
using shauli.ViewModels;
using Microsoft.Extensions.Logging;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace shauli.Controllers
{
    public class SearchController : Controller
    {
        [FromServices]
        public ApplicationDbContext _context { get; set; }
        [FromServices]
        public ILogger<SearchController> Logger { get; set; }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }


        //Post
        [HttpPost,ActionName("Search")]
        public IActionResult Search(string searchAuthor, string searchTitle, string searchAddress, string sortOrder)
        {
            bool authorAlive = !String.IsNullOrEmpty(searchAuthor);
            bool titleAlive = !String.IsNullOrEmpty(searchTitle);
            bool addressAlive = !String.IsNullOrEmpty(searchAddress);
            bool sortAlive = !String.IsNullOrEmpty(sortOrder);

            var posts = _context.Posts.ToList();
            //var posts select author from _context.Posts where Author == search
            if (authorAlive && titleAlive && addressAlive) //author + title + address
            {
                posts = _context.Posts.Where(s => s.PostAuthorName == searchAuthor && s.PostTitle == searchTitle && s.PostAuthorWebAddress == searchAddress).ToList();
            }
            else if (authorAlive && titleAlive && !addressAlive)//author + title
            {
                posts = _context.Posts.Where(s => s.PostAuthorName == searchAuthor && s.PostTitle == searchTitle).ToList();
            }
            else if (authorAlive && !titleAlive && addressAlive)//author + address
            {
                posts = _context.Posts.Where(s => s.PostAuthorName == searchAuthor && s.PostAuthorWebAddress == searchAddress).ToList();
            }
            else if (!authorAlive && titleAlive && addressAlive)//title + address
            {
                posts = _context.Posts.Where(s => s.PostAuthorWebAddress == searchAddress && s.PostTitle == searchTitle).ToList();
            }
            else if (authorAlive && !titleAlive && !addressAlive)//author
            {
                posts = _context.Posts.Where(s => s.PostAuthorName == searchAuthor).ToList();
            }
            else if (!authorAlive && titleAlive && !addressAlive) //title
            {
                posts = _context.Posts.Where(s => s.PostTitle == searchTitle).ToList();
            }
            else if (!authorAlive && !titleAlive && addressAlive) //address
            {
                posts = _context.Posts.Where(s => s.PostAuthorWebAddress == searchAddress).ToList();
            }
            else
            {
                posts = null;
                //return View(posts);
            }
            if (sortAlive)
            {
                if (sortOrder == "author") //sort by author
                {
                    _context.Posts.GroupBy(s => s.PostAuthorName).ToList();
                    posts = _context.Posts.OrderBy(s => s.PostAuthorName).ToList();
                }
                else if (sortOrder == "address")//sort by address
                {
                    _context.Posts.GroupBy(s => s.PostAuthorWebAddress).ToList();
                    posts = _context.Posts.OrderBy(s => s.PostAuthorWebAddress).ToList();
                }
                else if (sortOrder == "title") // sort by title
                {
                    _context.Posts.GroupBy(s => s.PostTitle).ToList();
                    posts = _context.Posts.OrderBy(s => s.PostTitle).ToList();
                }
            }
            return View(posts);
        }

        [HttpPost]
        public IActionResult Search(Post post)
        {
            var commnets = from p in _context.Posts
                           join c in _context.Comments
                           on p.PostID equals c.PostID
                           where post.PostID == c.PostID
                           select c;
            return View(commnets.ToList());
        }

        [HttpPost]
        public IActionResult Search(Fan fan)
        {
            var commnets = from p in _context.Posts
                           join c in _context.Comments
                           on p.PostID equals c.PostID
                           where fan.FName == c.CommentAuthorName
                           select c;
            return View(commnets.ToList());
        }
    }   
}
