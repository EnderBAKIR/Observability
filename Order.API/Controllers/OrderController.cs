using Microsoft.AspNetCore.Mvc;
using Order.API.OrderServices;
using System.Security.AccessControl;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly OrderService _orderService;

		public OrderController(OrderService orderService)
		{
			_orderService = orderService;
		}

		[HttpPost]
		public async Task<IActionResult> Create(OrderCreateRequestDto orderCreateRequestDto)
		{
			#region Exception Hatası Testi
			//var a = 10; exception fırlatılma durumu örneği
			//var b = 0;
			//var c = a / b; 
			#endregion

			var result = await _orderService.CreateAsync(orderCreateRequestDto);


            return new ObjectResult(result) { StatusCode = result.StatusCode };

        }
	}
}
