using DotNetCoreBlog.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreBlog.Data.Repository
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _db;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void AddPost(Post post)
        {
            _db.Posts.Add(post);
        }

        public List<Post> GetAllPosts()
        {
            return _db.Posts.ToList();
        }

        public Post GetPost(int id)
        {
            return _db.Posts.FirstOrDefault(p => p.Id == id);
        }

        public void RemovePost(int id)
        {
            Post post = GetPost(id);

            _db.Posts.Remove(post);
        }

        public void UpdatePost(Post post)
        {
            _db.Posts.Update(post);
        }

        public async Task<bool> SaveChangesAsync()
        {
            if(await _db.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        public List<Post> GetAllPosts(string category)
        {
            return _db.Posts
                .Where(p => p.Category.ToLower().Equals(category.ToLower()))
                .ToList();

            //Functional Programming
            //Func<Post, bool> InCategory = (post) => { return post.Category.ToLower().Equals(category.ToLower()); };

            //return _db.Posts.Where(post => InCategory(post)).ToList();
        }
    }
}
