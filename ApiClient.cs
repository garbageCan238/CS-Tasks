namespace CS_Tasks
{
    public class ApiClient : IDisposable
    {
        private readonly HttpClient _httpClient;

        public ApiClient(string baseUrl)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
        }

        public async Task<string> GetAsync(string endpoint, string queryParams = "")
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{endpoint}/{queryParams}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PostAsync(string endpoint, HttpContent content, string queryParams = "")
        {
            HttpResponseMessage response = await _httpClient.PostAsync($"{endpoint}/{queryParams}", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}