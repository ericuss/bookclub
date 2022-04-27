

using Lanre.Context.Library;
using Lanre.Context.Library.Infrastructure.Database;
using Lanre.Infrastructure;
using Lanre.Infrastructure.Contexts;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;

using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
Dictionary<string, string> apiVersions = new() { { "v1", "Lanre API v1" } };

// Add services to the container.
#pragma warning disable CS8604 // Possible null reference argument.
builder.Services
        .Configure<SqlOptions>(builder.Configuration.GetSection("Sql"))
        .ConfigureInfrastructure()
        .ConfigureLibrary(builder.Configuration)
        .AddApiVersioning(options =>
        {
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = false;
            options.ReportApiVersions = true;
        })
        .AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        })
        .AddCustomSwagger(apiVersions)
        //.AddSwaggerGen()
        .AddMvcCore()
            .AddApplicationPart(typeof(Lanre.Context.Library.Configure).Assembly)
        .Services
        .AddMediatR(
            Assembly.GetAssembly(typeof(Lanre.Infrastructure.Configure)),
            Assembly.GetAssembly(typeof(Lanre.Context.Library.Configure))
        );
#pragma warning restore CS8604 // Possible null reference argument.
;


var app = builder.Build();
var scope = app.Services.CreateScope();


var libraryContext = scope.ServiceProvider.GetService<LibraryContext>();
var sqlOptions = scope.ServiceProvider.GetService<IOptions<SqlOptions>>();
if (app.Environment.IsDevelopment())
{
    ContextInitialize.InitializeDb(sqlOptions.Value, libraryContext).Wait();
}
scope.Dispose();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCustomSwagger(apiVersions);
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.UseAuthorization();

app.MapGet("/", () => "Hello World!");
app.UseEndpoints(endpoints =>
{
    //endpoints.UseHealthChecks();
    endpoints.MapControllers();
});

app.Run();
