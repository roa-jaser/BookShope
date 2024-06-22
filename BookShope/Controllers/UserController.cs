 using BookShope.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShope.Controllers
{
    public class UserController : Controller
    {
        private BookContext context;
        public UserController(BookContext ctx) { context = ctx; }

        private int? GetUserIdFromSession()
        {
            return HttpContext.Session.GetInt32("Uid");
        }

        public IActionResult userBookList()
        {
            if (GetUserIdFromSession == null)
                return RedirectToAction("login", "Home");

            var books = context.Books.OrderBy(b => b.Book_Name).ToList();
            return View(books);
        }
        public IActionResult UserSearch(string searchKey)
        {
            if (GetUserIdFromSession == null)
            {
                return RedirectToAction("login", "Home");
            }

            var BookList = context.Books.Where(b => b.Book_Name.Contains(searchKey)).OrderBy(b => b.Book_Name).ToList();
            return View("SearchUser", BookList);

        }

        [HttpGet]
        public IActionResult UserChangePassword()
        {
            if (GetUserIdFromSession == null)
                return RedirectToAction("login", "Home");

            return View();
        }

        [HttpPost]
        public IActionResult UserChangePassword(string oldPass, string newPass)
        {
            int? Uid = GetUserIdFromSession();
            User user = context.Users.Find(Uid);
            if (user.Password == oldPass)
            {
                user.Password = newPass;
                context.Users.Update(user);
                context.SaveChanges();
                return RedirectToAction("userBookList", "User");
            }
            return View();
        }

        public IActionResult AddFav(int id)
        {
            if (GetUserIdFromSession == null)
            {
                return RedirectToAction("login", "Home");
            }

            var userId = GetUserIdFromSession();
            var book = context.Books.FirstOrDefault(b => b.Book_Id == id);
            if (book == null)
            {
                return RedirectToAction("userBookList", "User");
            }

            var existingFav = context.Favs.FirstOrDefault(f =>
                f.User_Id == userId &&
                f.Book_Name == book.Book_Name &&
                f.Writer == book.Writer &&
                f.price == book.price);

            if (existingFav != null)
            {
                return RedirectToAction("FavList", "User");
            }

            var favoriteBook = new favoriteBook
            {
                Book_Name = book.Book_Name,
                Writer = book.Writer,
                price = book.price,
                User_Id = userId
            };

            context.Favs.Add(favoriteBook);
            context.SaveChanges();

            return RedirectToAction("userBookList", "User");
        }

        public IActionResult FavList()
        {
            if (GetUserIdFromSession == null)
                return RedirectToAction("login", "Home");
            else
            {
                int? UserID = GetUserIdFromSession();
                List<favoriteBook> Fav = context.Favs.Where(f => f.User_Id == UserID).ToList();
                return View(Fav);

            }

        }

        public IActionResult DeleteFav(int id)
        {
            if (GetUserIdFromSession == null)
            {
                return RedirectToAction("login", "Home");
            }

            var userId = GetUserIdFromSession();
            var favoriteBook = context.Favs.FirstOrDefault(f => f.Book_Id == id && f.User_Id == userId);
            if (favoriteBook != null)
            {
                context.Favs.Remove(favoriteBook);
                context.SaveChanges();
            }

            return RedirectToAction("FavList", "User");
        }

    }
}
