using Newtonsoft.Json;

public class ErrorHandlingMiddleware
{
    readonly RequestDelegate _next;
    
    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (ApplicationException ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 400;
            var body = JsonConvert.SerializeObject(new {Message=ex.Message});
            await context.Response.WriteAsync(body);
        }
    }
}