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
        private IRepository<Categories> _repository;
        public CategoryController(IRepository<Categories> _repository)
        {
            this._repository = _repository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productsViewModel"></param>
        /// <returns></returns>
        [HttpPost(), Route("api/products/addProducts")]
        public async Task<ActionResult> AddCategories(CategoriesViewModel categoriesViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var exists= _repository.Table.FirstOrDefault(pl => pl.Name == categoriesViewModel.Name);
            if (exists != null)
            {
                return BadRequest("Category with name already exists");
            }
            categoriesViewModel.CategoryCode = $"AR-category{categoriesViewModel.DateModified}";
            var category = categoriesViewModel.Create();
            _repository.Insert(category);
            return Ok();

        }

    }
}
