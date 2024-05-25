using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Threading.Tasks;
using EnderecoMVC.Data;
using EnderecoMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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
            var addresses = _context.Enderecos.Where(a => a.UserId == userId).ToList();
            return View(addresses);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Enderecos enderecos)
        {
            enderecos.UserId = int.Parse(HttpContext.Session.GetString("UserId"));
            _context.Add(enderecos);
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
        public async Task<IActionResult> Edit(Enderecos enderecos)
        {
            _context.Update(enderecos);
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

        [HttpPost]
        public async Task<IActionResult> SearchCep(string cep)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var enderecos = JsonConvert.DeserializeObject<Enderecos>(jsonString);
                return Json(enderecos);
            }
            return Json(null);
        }
    }
}