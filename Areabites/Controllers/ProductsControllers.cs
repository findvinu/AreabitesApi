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
    public class ProductsControllers:Controller
    {
        private IRepository<Products> _repository;
        public ProductsControllers(IRepository<Products> _repository)
        {
            this._repository = _repository;
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
            productsViewModel.ProductCode = $"AR-product{productsViewModel.DateModified}";
            var product = productsViewModel.Create();
             _repository.Insert(product);
            return Ok();

        }

    }
}
