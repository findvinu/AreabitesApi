using Domain.Entity;
using Infrastructure.Data;
using Infrastructure.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Areabites.Controllers
{
    public class CategoryController:Controller
    {
        private IRepository<Categories> _categoryRepository;
        public CategoryController(IRepository<Categories> _categoryRepository)
        {
            this._categoryRepository = _categoryRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productsViewModel"></param>
        /// <returns></returns>
        [HttpPost(), Route("api/products/addCategory")]
        public async Task<ActionResult> AddCategories(CategoriesViewModel categoriesViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var exists= _categoryRepository.Table.FirstOrDefault(pl => pl.Name == categoriesViewModel.Name);
            if (exists != null)
            {
                return BadRequest("Category with name already exists");
            }
            categoriesViewModel.CategoryCode = $"AR-category{categoriesViewModel.DateModified.ToString("yyyyMMddhhmmssfff")}";
            var category = categoriesViewModel.Create();
            _categoryRepository.Insert(category);
            return Ok();
        }

        [HttpPost(), Route("api/products/deleteCategory")]
        public async Task<ActionResult> DeleteCategory(int ID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var product = _categoryRepository.GetEntityById(ID);
            if (product == null) return BadRequest("Category with ID not found");
            _categoryRepository.Remove(product);
            return Ok();
        }

    }
}
