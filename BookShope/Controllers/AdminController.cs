 using BookShope.Models; 
using Microsoft.AspNetCore.Mvc;

namespace BookShope.Controllers
{
    public class AdminController : Controller
    {
        private BookContext context { get; set; }
        public AdminController(BookContext ctx)  
        {
            context = ctx;
        }
        public IActionResult AdminBookList()
        {
            if (HttpContext.Session.GetInt32("Aid") == null)
                return View("login");

            var books = context.Books.OrderBy(b => b.Book_Name).ToList();
            return View(books);
        }
        [HttpGet]
        public IActionResult Add()
        {
            if (HttpContext.Session.GetInt32("Aid") == null)
                return RedirectToAction("Login", "Home");

            return View();
        }

        [HttpPost]
        public IActionResult Add(Book B)
        {
            if (HttpContext.Session.GetInt32("Aid") == null)
                return RedirectToAction("Login", "Home");

            if (ModelState.IsValid)
            {

                context.Books.Add(B);
                context.SaveChanges();
                return RedirectToAction("adminBookList", "Admin");
            }
            return View();
        }
        public IActionResult AdminSearch(string searchKey)
        {

            if (HttpContext.Session.GetInt32("Aid") != null)
            {
                var BookList = context.Books.Where(b => b.Book_Name.Contains(searchKey)).OrderBy(b => b.Book_Name).ToList();
                return View("SearchAdmin", BookList);

            }

            return RedirectToAction("Login", "Home");

        }

        [HttpGet]
        public IActionResult AdminChangePassword()
        {
            if (HttpContext.Session.GetInt32("Aid") == null)
                return RedirectToAction("Login", "Home");

            return View("AdminChangepassword");
        }

        [HttpPost]
        public IActionResult AdminChangePassword(string oldPass, string newPass)
        {
            int? Aid = HttpContext.Session.GetInt32("Aid");
            Admin admin = context.Admin.Find(Aid);
            if (admin.Password == oldPass)
            {
                admin.Password = newPass;
                ViewBag.Error = null;
                context.Admin.Update(admin);
                context.SaveChanges();
                return RedirectToAction("adminBookList", "Admin");
            }
            ViewBag.Error = "The Old Password is Wrong";
            return View("AdminChangepassword");

        }
        [HttpGet]
        public IActionResult EditBook(int id)
        {
            if (HttpContext.Session.GetInt32("Aid") == null)
                return RedirectToAction("Login", "Home");


            var book = context.Books.Find(id);
            return View(book);
        }

        [HttpPost]
        public IActionResult EditBook(Book book)
        {
            if (ModelState.IsValid)
            {

                context.Books.Update(book);
                context.SaveChanges();
                return RedirectToAction("adminBookList", "Admin");


            }
            return View();
        }
        public IActionResult DeleteBook(int id)
        {
            if (HttpContext.Session.GetInt32("Aid") == null)
                return RedirectToAction("Login", "Home");

            Book book = context.Books.Where(b => b.Book_Id == id).FirstOrDefault();
            context.Remove(book);
            context.SaveChanges();
            return RedirectToAction("adminBookList", "Admin");
        }
       
    }
}
