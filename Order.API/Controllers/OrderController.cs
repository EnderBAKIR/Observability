using Microsoft.AspNetCore.Mvc;
using Order.API.OrderServices;

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

			await _orderService.CreateAsync(orderCreateRequestDto);
			return Ok(new OrderCreateResponseDto() {Id = new Random().Next(1, 500) });
		}
	}
}
