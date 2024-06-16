namespace FoodAPI.Services.Security;

public class Dependency(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        string? authorizationHeader = context.Request.Headers.Authorization;
        if (!string.IsNullOrEmpty(authorizationHeader))
        {
            string? apiKey = authorizationHeader.StartsWith("Apikey ") ? authorizationHeader["Apikey ".Length..].Trim() : authorizationHeader;

            if (apiKey == null || !apiKey.Equals("123"))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return;
            }
        }

        await _next(context);
    }
}