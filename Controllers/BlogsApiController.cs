﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFGetStarted.AspNetCore.ExistingDb.Models;
using Microsoft.AspNetCore.Authorization;

namespace EFGetStarted.AspNetCore.ExistingDb.Controllers
{
    [Produces("application/json")]
    [Route("api/BlogsApi")]
    public class BlogsApiController : Controller
    {
        private readonly BloggingContext _context;

        public BlogsApiController(BloggingContext context)
        {
            _context = context;
        }

        // GET: api/BlogsApi
        [HttpGet]
        public IEnumerable<Blog> GetBlog()
        {
            return _context.Blog.Include (p=>p.Post);
        }

        // GET: api/BlogsApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlog([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var blog = await _context.Blog.SingleOrDefaultAsync(m => m.BlogId == id);

            if (blog == null)
            {
                return NotFound();
            }

            return Ok(blog);
        }

        // PUT: api/BlogsApi/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlog([FromRoute] int id, [FromBody] Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != blog.BlogId)
            {
                return BadRequest();
            }

            _context.Entry(blog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BlogsApi
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostBlog([FromBody] Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Blog.Add(blog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlog", new { id = blog.BlogId }, blog);
        }

        // DELETE: api/BlogsApi/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var blog = await _context.Blog.SingleOrDefaultAsync(m => m.BlogId == id);
            if (blog == null)
            {
                return NotFound();
            }

            _context.Blog.Remove(blog);
            await _context.SaveChangesAsync();

            return Ok(blog);
        }

        private bool BlogExists(int id)
        {
            return _context.Blog.Any(e => e.BlogId == id);
        }
    }
}