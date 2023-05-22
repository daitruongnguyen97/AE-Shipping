using Shipping.Application;
using Shipping.Infrastructure;
using Shipping.Infrastructure.Data;
using Shipping.API;
using Shipping.Application.Middleware;
using Shipping.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddControllers().AddJsonOptions(x =>
{
    // serialize enums as strings in api responses (e.g. Role)
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

    // ignore omitted parameters on models to enable optional params (e.g. User update)
    //x.JsonSerializerOptions.IgnoreNullValues = true;
    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
services
    .AddMemoryCache()
    .AddEndpointsApiExplorer()
    .AddInfrastructureServices(builder.Configuration)
    .AddSwaggerDocumentation(builder.Configuration)
    .AddHttpContextAccessor()
    .AddApplicationServices(builder.Configuration)
    .AddCustomMvc(builder.Configuration)
    .AddHealthChecks();
var app = builder.Build();
app.MapHealthChecks("/manage/alive");
if (builder.Configuration.GetValue<bool>("Swagger:Enabled", false))
{
    app.UseSwagger(setup =>
    {
    });
    app.UseSwaggerUI(setup =>
    {
        setup.SwaggerEndpoint($"{builder.Configuration.GetValue("Swagger:SubDomain", "")}/swagger/v2/swagger.json", "Shipping v2");
        setup.DocumentTitle = "Shipping Resource";
        setup.DocExpansion(DocExpansion.List);
    });
}
app.UseExceptionHandlingMiddleware(exception => exception switch
{
    AggregateNotFoundException _ => HttpStatusCode.NotFound,
    //ConcurrencyException => HttpStatusCode.PreconditionFailed,
    _ => HttpStatusCode.InternalServerError
});
app.UseRouting();
app.UseCors("CorsPolicy2");
app.UseAuthentication();

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
    endpoints.MapControllers();
});

//Migrate app db
using var scope = app.Services.CreateScope();
var initialiser = scope.ServiceProvider.GetRequiredService<AppDbContextInitialiser>();
await initialiser.InitialiseAsync();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.Run();
