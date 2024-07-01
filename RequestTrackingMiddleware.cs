namespace CS_Tasks
{
    public class RequestTrackingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly SemaphoreSlim _semaphore;

        public RequestTrackingMiddleware(RequestDelegate next, int maxConcurrentRequests)
        {
            _next = next;
            _semaphore = new SemaphoreSlim(maxConcurrentRequests);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (await _semaphore.WaitAsync(0))
            {
                try
                {
                    await _next(context);
                }
                finally
                {
                    _semaphore.Release();
                }
            }
            else
            {
                context.Response.StatusCode = 503;
                await context.Response.WriteAsync("Service Unavailable");
            }
        }
    }
}
