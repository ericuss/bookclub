using Lanre.Context.Library;
using Lanre.Context.Library.Infrastructure.Database;
using Lanre.Context.Poll;
using Lanre.Context.Poll.Infrastructure.Database;
using Lanre.Infrastructure;
using Lanre.Infrastructure.Contexts;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System.Reflection;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
Dictionary<string, string> apiVersions = new() { { "v1", "Lanre API v1" } };

// Add services to the container.
#pragma warning disable CS8604 // Possible null reference argument.
builder.Services
        .Configure<SqlOptions>(builder.Configuration.GetSection("Sql"))
        .ConfigureInfrastructure()
        .ConfigureLibrary(builder.Configuration)
        .ConfigurePoll(builder.Configuration)
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
        )
        .AddAuthorization()
        .AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.Authority = builder.Configuration["Auth:Authority"];
            options.Audience = builder.Configuration["Auth:Audience"];
            // If the access token does not have a `sub` claim, `User.Identity.Name` will be `null`. Map it to a different claim by setting the NameClaimType below.
            options.TokenValidationParameters = new TokenValidationParameters
            {
                NameClaimType = ClaimTypes.NameIdentifier
            };
        });
;
#pragma warning restore CS8604 // Possible null reference argument.
;


var app = builder.Build();
var scope = app.Services.CreateScope();
var libraryContext = scope.ServiceProvider.GetService<LibraryContext>();
var pollContext = scope.ServiceProvider.GetService<PollContext>();
var sqlOptions = scope.ServiceProvider.GetService<IOptions<SqlOptions>>();
if (app.Environment.IsDevelopment())
{
    ContextInitialize.InitializeDb(sqlOptions.Value, libraryContext).Wait();
    ContextInitialize.InitializeDb(sqlOptions.Value, pollContext).Wait();
}
scope.Dispose();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Hello");
    });
}

app
    .UseCustomSwagger(apiVersions)
    .UseHttpsRedirection()
    .UseStaticFiles()
    .UseRouting()
    .UseAuthentication()
    .UseAuthorization()
    .UseEndpoints(endpoints =>
    {
        //endpoints.UseHealthChecks();
        endpoints.MapControllers();
    });

app.Run();
