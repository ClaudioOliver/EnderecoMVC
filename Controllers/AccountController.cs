using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnderecoMVC.Data;
using EnderecoMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnderecoMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Usuario == username && u.Senha == password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                return RedirectToAction("Index", "Addresses");
            }
            ViewBag.Error = "Invalid credentials";
            return View();
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }

}