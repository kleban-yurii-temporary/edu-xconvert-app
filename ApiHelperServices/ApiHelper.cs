namespace XConverter.ApiHelperServices
{
    public class ApiHelper
    {
        private readonly IConfiguration _configuration;
        public ApiHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetApiUrl()
        {
            return $"{_configuration["ConverterApi:ApiUrl"]}currencies.json";
        }

        public string GetLatestDataUrl(string from, string to, decimal amount)
        {
            var apiKey = _configuration["ConverterApi:ApiKey"];
            var @base = _configuration["ConverterApi:Base"];
            
            return $"{_configuration["ConverterApi:ApiUrl"]}latest.json?app_id={apiKey}&base={@base}&symbols={from},{to}";
        }
    }
}
