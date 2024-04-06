using Common.Shared.DTOs;

namespace Order.API.StockServices
{
    public class StockService
    {
        private readonly HttpClient _client;

        public StockService(HttpClient client)
        {
            _client = client;
        }


        public async Task<(bool isSuccess, string? failMessage)> CheckStockAndPaymentStartAsync(StockCheckAndPaymenProcessRequestDto requestBody)
        {

            var response = await _client.PostAsJsonAsync<StockCheckAndPaymenProcessRequestDto>("api/Stock/CheckAndPaymentStart", requestBody);

            var responseContent = await response.Content.ReadFromJsonAsync<ResponseDto<StockCheckAndPaymenProcessRequestDto>>();


            return response.IsSuccessStatusCode ? (true, null) : (false, responseContent!.Errors!.First());

        }
    }
}
