using FoodAPI.Config;
using FoodAPI.Database;
using FoodAPI.Services.FoodService;
using FoodAPI.Services.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("Database")
);

builder.Services.AddSingleton<MongoDatabase>();
builder.Services.AddSingleton<FoodServices>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {

    }).AddMvcOptions(options =>
    {
        options.OutputFormatters.RemoveType<Microsoft.AspNetCore.Mvc.Formatters.StringOutputFormatter>();
        options.OutputFormatters.RemoveType<Microsoft.AspNetCore.Mvc.Formatters.HttpNoContentOutputFormatter>();
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<Dependency>();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", (HttpContext httpContext) =>
{
    return "Hello world";
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
