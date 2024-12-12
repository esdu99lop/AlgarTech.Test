using AlgarTech.Test.Core.Interfaces;
using AlgarTech.Test.Core.Services;
using AlgarTech.Test.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace AlgarTech.Test.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;
        public OrdersController(IOrdersService ordersService) 
        {
            _ordersService = ordersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var result = await _ordersService.GetAllOrdersAsync();
                IEnumerable<dynamic> data = result.AsEnumerable().Select(row => new {
                    IDOrder = row["IDOrder"],
                    ClientIdentification = row["ClientIdentification"], 
                    ClientAddress = row["ClientAddress"], 
                    OrderDate = row["OrderDate"], 
                    Total = row["Total"] });
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrdersDto request)
        {
            try
            {
                await _ordersService.InsertOrderAsync(request);
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
                await _ordersService.DeleteOrderAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
