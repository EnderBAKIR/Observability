using Azure.Core;
using Common.Shared.DTOs;
using OpenTelemetry.Shared;
using Order.API.Models;
using Order.API.StockServices;
using System.Diagnostics;
using System.Net;
using System.Net.WebSockets;

namespace Order.API.OrderServices
{
    public class OrderService
    {
        private readonly AppDbContext _context;
        private readonly StockService _stockServices;

        public OrderService(AppDbContext context, StockService stockServices)
        {
            _context = context;
            _stockServices = stockServices;
        }

        public async Task<ResponseDto<OrderCreateResponseDto>> CreateAsync(OrderCreateRequestDto request)
        {
            Activity.Current?.SetTag("Asp.Net Core(instrumentation) Tag1", "Asp.Net Core(instrumentation) tag value");
            using var activity = ActivitySourceProvider.Source.StartActivity();
            activity?.AddEvent(new ActivityEvent("Sipariş süreci başladı"));

            var newOrder = new Order()
            {
                Created = DateTime.Now,
                OrderCode = Guid.NewGuid().ToString(),
                Status = OrderStatus.Succes,
                UserId = request.UserId,
                Items = request.Items.Select(x => new OrderItem()
                {
                    Count = x.Count,
                    ProductId = x.ProductId,
                    UnitPrice = x.UnitPrice
                }).ToList()
            };
            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            StockCheckAndPaymenProcessRequestDto stockRequest = new();

            stockRequest.OrderCode = newOrder.OrderCode;
            stockRequest.OrderItems = request.Items;

            var (isSuccess, failMessage) = await _stockServices.CheckStockAndPaymentStartAsync(stockRequest);


            if (!isSuccess)
            {
                return ResponseDto<OrderCreateResponseDto>.Fail(HttpStatusCode.InternalServerError.GetHashCode(), failMessage!);

            }

            activity?.AddEvent(new("Sipariş süreci tamamlandı."));

            return ResponseDto<OrderCreateResponseDto>.Success(HttpStatusCode.OK.GetHashCode(), new OrderCreateResponseDto() { Id = newOrder.Id });






        }
    }
}
