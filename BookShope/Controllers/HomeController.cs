using BookShope.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BookApp.Models;

namespace BookShope.Controllers
{
    public class HomeController : Controller

    {

        private BookContext context { get; set; }
        private readonly ILogger<HomeController> _logger;

        public HomeController(BookContext ctx, ILogger<HomeController> logger)
        {
            context = ctx;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetInt32("Uid") != null)
            { return RedirectToAction("userBookList", "User"); }

            else if (HttpContext.Session.GetInt32("Aid") != null)
            { return RedirectToAction("adminBookList", "Admin"); }

            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password, int flag)
        {
            if (flag == 1) //This is Admin
            {
                Admin admin = context.Admin.Where(a => a.User_Name == username && a.Password == password).FirstOrDefault();
                if (admin != null)
                {
                    //successful login
                    HttpContext.Session.SetInt32("Aid", admin.Admin_Id);
                    return RedirectToAction("adminBookList", "Admin");
                }
                return View("Login");


            }
            else if (flag == 0) //This is user
            {
                User user = context.Users.Where(u => u.User_Name == username && u.Password == password).FirstOrDefault();
                if (user != null)
                {
                    //successful login
                    HttpContext.Session.SetInt32("Uid", user.User_Id);
                    ViewBag.UserName = user.User_Name;
                    return RedirectToAction("userBookList", "User");
                }

                return View("Login");

            }

            else
                return View("Login");

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(User user)
        {
            if (context.Users.Any(u => u.User_Name == user.User_Name
                           && u.Email == user.Email
                           && u.Password == user.Password))
            {
                return View(user);
            }
            else
            {
                context.Users.Add(user);
                context.SaveChanges();
                HttpContext.Session.SetInt32("Uid", user.User_Id);
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
