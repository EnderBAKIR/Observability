using OpenTelemetry.Shared;
using System.Diagnostics;

namespace Order.API.OrderServices
{
    public class OrderService
	{
        public Task CreateAsync(OrderCreateRequestDto requestDto)
		{
			Activity.Current?.SetTag("Asp.Net Core(instrumentation) Tag1", "Asp.Net Core(instrumentation) tag value");
			using var activity = ActivitySourceProvider.Source.StartActivity();
			activity?.AddEvent(new ActivityEvent("Sipariş süreci başladı"));


			// veri tabanına kayıt yapıldı
			activity.SetTag("Order User Id",requestDto.UserId);
			activity?.AddEvent(new ActivityEvent("Sipariş süreci Tamamlandı"));
			return Task.CompletedTask;
		}
    }
}
