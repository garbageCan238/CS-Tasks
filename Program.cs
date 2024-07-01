using CS_Tasks;
using Microsoft.AspNetCore.Rewrite;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Services.AddScoped<IRandomService, RandomService>(sp =>
{
    string url = builder.Configuration.GetValue<string>("RandomApi");
    return new RandomService(url);
});
builder.Services.AddScoped<IProcessedStringService, ProcessedStringService>(sp =>
{
    var blackList = builder.Configuration.GetSection("Settings:BlackList").Get<List<string>>(); ;
    var randomService = sp.GetRequiredService<IRandomService>();
    return new ProcessedStringService(randomService, blackList);
});
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Processed string data API");
    });
}

app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger"));
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<RequestTrackingMiddleware>(builder.Configuration.GetValue<int>("ParallelLimit"));
app.Run();