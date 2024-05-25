using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EnderecoMVC.Data;
using EnderecoMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EnderecoMVC.Controllers
{
    [Route("[controller]")]
    public class EnderecoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnderecoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId"));
            var addresses = _context.Enderecos.Where(a => a.UsuarioId == userId).ToList();
            return View(addresses);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Endereco address)
        {
            address.UsuarioId = int.Parse(HttpContext.Session.GetString("UserId"));
            _context.Add(address);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var address = _context.Enderecos.Find(id);
            return View(address);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Endereco address)
        {
            _context.Update(address);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var address = _context.Enderecos.Find(id);
            return View(address);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var address = _context.Enderecos.Find(id);
            _context.Enderecos.Remove(address);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}