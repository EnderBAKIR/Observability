using Common.Shared.DTOs;
using System.Net;
using System.Net.WebSockets;

namespace Stock.API
{
    public class StockService
    {
        public static Dictionary<int, int> GetProductStockList()
        {

            Dictionary<int, int> productStockList = new();
            productStockList.Add(1, 10);
            productStockList.Add(1, 20);
            productStockList.Add(1, 30);

            return productStockList;
        }

        public async Task<ResponseDto<StockCheckAndPaymenProcessResponseDto>> CheckAndPaymentProcess(StockCheckAndPaymenProcessRequestDto request)
        {
            var productStockList = GetProductStockList();

            var stockStatus = new List<(int productId, bool hasStockExist)>();

            foreach (var orderItem in request.OrderItems)
            {
                var hasExistStock = productStockList.Any(x => x.Key == orderItem.ProductId && x.Value >= orderItem.Count);

                stockStatus.Add((orderItem.ProductId, hasExistStock));

            }

            if (stockStatus.Any(x=>x.hasStockExist== false))
            {
                return ResponseDto<StockCheckAndPaymenProcessResponseDto>.Fail(HttpStatusCode.BadRequest.GetHashCode(), "stock yetersiz");
            }
            return ResponseDto<StockCheckAndPaymenProcessResponseDto>.Success(HttpStatusCode.OK.GetHashCode(), 
                new StockCheckAndPaymenProcessResponseDto() { Description="Stock Ayrıldı"});
           
            // payment süreci başyalacak

        }
    }
}
