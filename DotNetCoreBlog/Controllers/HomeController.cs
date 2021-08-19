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
        public IActionResult Edit(Post post)
        {
            return RedirectToAction("Index");
        }
    }
}
