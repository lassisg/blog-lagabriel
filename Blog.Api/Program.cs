using Blog.Api;
using Blog.Api.Endpoints;
using Data.Models.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddOptions<BlogApiJsonDirectAccessSetting>()
    .Configure(options =>
    {
        options.DataPath = builder.Configuration.GetSection("DataPath").Value ?? "./data";
        options.BlogPostsFolder = "BlogPosts";
        options.TagsFolder = "Tags";
        options.CategoriesFolder = "Categories";
    });

builder.Services.AddScoped<IBlogApi, BlogApiJsonDirectAccess>();

builder.Services.AddCors(
    options => options.AddPolicy(
        builder.Configuration["CorsPolicyName"],
        policy => policy
            .WithOrigins([builder.Configuration["BackendUrl"], builder.Configuration["FrontendUrl"]])
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()));

var app = builder.Build();

app.UseCors(builder.Configuration["CorsPolicyName"]);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };
//
// app.MapGet("/weatherforecast", () =>
//     {
//         var forecast = Enumerable.Range(1, 5).Select(index =>
//                                                          new WeatherForecast
//                                                          (
//                                                              DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//                                                              Random.Shared.Next(-20, 55),
//                                                              summaries[Random.Shared.Next(summaries.Length)]
//                                                          ))
//             .ToArray();
//         return forecast;
//     })
//     .WithName("GetWeatherForecast");

app.MapBlogPostApi();
app.MapCategoryApi();
app.MapTagApi();

app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }