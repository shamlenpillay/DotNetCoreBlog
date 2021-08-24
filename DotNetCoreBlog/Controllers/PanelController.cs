using DotNetCoreBlog.Data.FileManager;
using DotNetCoreBlog.Data.Repository;
using DotNetCoreBlog.Models;
using DotNetCoreBlog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreBlog.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PanelController : Controller
    {
        private readonly IRepository _repo;
        private IFileManager _fileManager;

        public PanelController(IRepository repo, IFileManager fileManager)
        {
            _repo = repo;
            _fileManager = fileManager;
        }

        public IActionResult Index()
        {
            var posts = _repo.GetAllPosts();
            return View(posts);
        }

        public IActionResult Post(int id)
        {
            var post = _repo.GetPost(id);
            return View(post);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View(new PostViewModel());
            }
            else
            {
                var post = _repo.GetPost((int)id);
                return View(new PostViewModel
                {
                    Id = post.Id,
                    Title = post.Title,
                    Body = post.Body,
                    CurrentImage = post.Image,
                    Description = post.Description,
                    Tags = post.Tags,
                    Category = post.Category
                });
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel postVM)
        {
            var post = new Post
            {
                Id = postVM.Id,
                Title = postVM.Title,
                Body = postVM.Body,
                Description = postVM.Description,
                Tags = postVM.Tags,
                Category = postVM.Category
            };

            if (postVM.Image == null)
            {
                post.Image = postVM.CurrentImage;
            }
            else
            {
                post.Image = await _fileManager.SaveImage(postVM.Image);
            }

            if (post.Id > 0)
                _repo.UpdatePost(post);
            else
                _repo.AddPost(post);

            if (await _repo.SaveChangesAsync())
                return RedirectToAction("Index");
            else
                return View(post);
        }

        public async Task<IActionResult> Remove(int id)
        {
            _repo.RemovePost(id);

            await _repo.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
