using DotNetCoreBlog.Data;
using DotNetCoreBlog.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Post()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View(new Post());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {
            _db.Posts.Add(post);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
