﻿using Blog.Business.Absract;
using Blog.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: Admin/Category
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // GET: Admin/Category/GetAllCategories
        [HttpGet("GetAllCategories")]
        public IActionResult GetAllCategories()
        {
            try
            {
                var categories = _categoryService.GetAll();
                return Json(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: Admin/Category/Add
        [HttpPost("Add")]
        public IActionResult Add([FromBody] Category category)
        {
            try
            {
                _categoryService.Add(category);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: Admin/Category/Update
        [HttpPut("Update")]
        public IActionResult Update([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest();
            }
            try
            {
                _categoryService.Update(category);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: Admin/Category/GetById/{id}
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            if (id == 0) // 'id == null' olmaz, çünkü id bir integer.
            {
                return BadRequest("Invalid Id Format");
            }
            try
            {
                var category = _categoryService.GetById(id);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: Admin/Category/Delete/{id}
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid Id Format");
            }
            try
            {
                _categoryService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
