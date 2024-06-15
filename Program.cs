var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("Database")
);

builder.Services.AddSingleton<FoodServices>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
