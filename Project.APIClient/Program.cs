
var builder = WebApplication.CreateBuilder(args);
string myAllowSpecificOrigins = "_myAllowSpecificOrigins";
IConfiguration config = builder.Configuration;

builder.Services.AddSingleton(config);

Project.APIClient.Extensions.ServiceExtension.RegisterServices(builder.Services, config, myAllowSpecificOrigins);

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.Use((ctx, next) =>
{
    var headers = ctx.Response.Headers;

    headers.Append("X-Frame-Options", "DENY");
    headers.Append("X-XSS-Protection", "1; mode=block");
    headers.Append("X-Content-Type-Options", "nosniff");
    headers.Append("Strict-Transport-Security", "max-age=31536000; includeSubDomains; preload");
    headers.Append("Cache-Control", "no-cache, no-store, must-revalidate");
    headers.Append("Pragma", "no-cache");

    headers.Remove("X-Powered-By");
    headers.Remove("x-aspnet-version");

    headers.Remove("Server");

    return next();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Program Explorer Service v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors(myAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
