using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XConverter.ApiHelperServices;
using XConverter.Models;

namespace XConverter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConvertController : ControllerBase
    {
        private readonly ApiHelper _apiHelper;

        public ConvertController(ApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }
        
        [HttpGet]
        [Route("/api/convert/test")]
        public async Task<IActionResult> GetTest()
        {
            return Ok(_apiHelper.GetApiUrl());
        }

        [HttpGet]
        [Route("/api/convert/currencies")]
        public async Task<IActionResult> GetCurrenciesAsync()
        {
            var url = _apiHelper.GetApiUrl();
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            var result = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            return Ok(result);
        }
        
        [HttpGet]
        [Route("/api/convert/start")]
        public async Task<IActionResult> StartAsync(string from, string to, decimal amount)
        {
            var url = _apiHelper.GetLatestDataUrl(from, to, amount);
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            var result = await response.Content.ReadFromJsonAsync<LatestCurrenciesData>();

            var returnObj = new CalculatedData
            {
                Rate = result.Rates[to] / result.Rates[from],
                Amount = amount / result.Rates[from] * result.Rates[to]
            };
            
            return Ok(returnObj);
        }
    }
}
