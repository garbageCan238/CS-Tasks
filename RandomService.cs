using System.Text.RegularExpressions;

namespace CS_Tasks
{
    public class RandomService : IDisposable
    {
        private const string baseUrl = "http://www.randomnumberapi.com/";
        private const string pattern = @"\d+";
        private ApiClient client;
        private string endpoint = "api/v1.0/random";

        public RandomService()
        {
            client = new ApiClient(baseUrl);
        }
        public async Task<int> Next()
        {
            return await Next(0, Int32.MaxValue);
        }

        public async Task<int> Next(int maxValue)
        {
            return await Next(0, maxValue);
        }

        public async Task<int> Next(int minValue, int maxValue)
        {
            var queryParams = $"?min={minValue}&max={maxValue}";
            var response = await client.GetAsync(endpoint, queryParams);
            var matches = Regex.Match(response, pattern);
            return Int32.Parse(matches.Groups[0].Value);
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}
