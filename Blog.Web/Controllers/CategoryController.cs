﻿using Blog.Business.Absract;
using Blog.Business.Concrete;
using Blog.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
   // [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _ıcategoryService;

        public CategoryController(ICategoryService ıcategoryService)
        {
            _ıcategoryService = ıcategoryService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var category = _ıcategoryService.GetAll();
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        //Admin Authorize 


        [HttpPost("Add")]
        public IActionResult Add([FromBody] Category category)
        {
            try
            {
                _ıcategoryService.Add(category);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("Update/{id}")]
        public IActionResult Update([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    _ıcategoryService.Update(category);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

        }
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid Id Format");
            }
            try
            {
                var category = _ıcategoryService.GetById(id);
                if (category == null)
                {
                    return NotFound("Category Not Found");
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid Id Format");
            }

            var category = _ıcategoryService.GetById(id);
            if (category == null)
            {
                return NotFound("Category Not Found");
            }

            _ıcategoryService.Delete(id);
            return Ok();
        }

    }
}
