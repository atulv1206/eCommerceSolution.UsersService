using eCommerce.Infrastructure;
using eCommerce.API.Middleware;
using eCommerce.Core;
using System.Text.Json.Serialization;
using eCommerce.Core.Mappers;
using FluentValidation.AspNetCore;


var builder = WebApplication.CreateBuilder(args);
//Add Infrastructure Services
builder.Services.AddInfrastructure();
builder.Services.AddCore();

//Add Controller
builder.Services.AddControllers().AddJsonOptions(option =>
{
    option.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddAutoMapper(typeof(ApplicationUserMappingProfile).Assembly);
builder.Services.AddFluentValidationAutoValidation();

//build the web application
var app = builder.Build();
app.UseExceptionHandlingMiddleware();
//Routing
app.UseRouting();

//Auth
app.UseAuthentication();
app.UseAuthorization();

//Controller route
app.MapControllers();

app.Run();
