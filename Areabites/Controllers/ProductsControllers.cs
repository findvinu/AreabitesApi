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
    public class ProductsControllers<TEntity>:Controller 
    {
        private IRepository<Products> _productRepository;
        private IRepository<Categories> _categoryRepository;
        public ProductsControllers(IRepository<Products> _productRepository, IRepository<Categories> _categoryRepository)
        {
            this._productRepository = _productRepository;
            this._categoryRepository = _categoryRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productsViewModel"></param>
        /// <returns></returns>
        [HttpPost(), Route("api/products/addProducts")]
       public async Task<ActionResult> AddProducts(ProductsViewModel productsViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            productsViewModel.ProductCode = $"AR-product{productsViewModel.DateModified.ToString("yyyyMMddhhmmssfff")}";
            productsViewModel.Category= _categoryRepository.Table.FirstOrDefault(pl => pl.Name == productsViewModel.CategoryName);
            var product = productsViewModel.Create();
            _productRepository.Insert(product);
            return Ok();
        }

        [HttpPost(), Route("api/products/deleteProducts")]
        public async Task<ActionResult> DeleteProducts(int ID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var product = _productRepository.GetEntityById(ID);
            if (product == null) return BadRequest("Product with ID not found");
            _productRepository.Remove(product);
            return Ok();
        }
    }
}
