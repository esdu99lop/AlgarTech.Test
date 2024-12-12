using AlgarTech.Test.Core.Interfaces;
using AlgarTech.Test.Core.Services;
using AlgarTech.Test.Data.Models;
using AlgarTech.Test.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace AlgarTech.Test.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var result = await _productsService.GetAllProductsAsync();
                IEnumerable<dynamic> data = result.AsEnumerable().Select(row => new { IDProduct = row["IDProduct"], ProductName = row["ProductName"], Price = row["Price"]});
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                var result = await _productsService.GetProductByIdAsync(id);
                return result.Rows.Count > 0 ? Ok() : NotFound();
            }
            catch (Exception ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductsDto request)
        {
            try
            {
                await _productsService.InsertProductAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductsDto request)
        {
            try
            {
                await _productsService.UpdateProductAsync(id, request);
                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _productsService.DeleteProductAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
