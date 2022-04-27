using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Microsoft.Extensions.DependencyInjection
{
    public class RemoveVersionParameterFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var versionParameter = operation.Parameters.FirstOrDefault(p => p.Name == "version");
            operation.Parameters.Remove(versionParameter);
        }
    }
    public class ReplaceVersionWithExactValueInPathFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = new OpenApiPaths();
            foreach (var path in swaggerDoc.Paths)
            {
                paths.Add(path.Key.Replace("v{version}", swaggerDoc.Info.Version), path.Value);
            }
            swaggerDoc.Paths = paths;
        }
    }

    public static class SwaggerExtensions
        {
            /// <summary>
            /// Configure Swagger middlewares with versions
            /// </summary>
            /// <param name="versions">Dictionary of "version" and "description" like { "V1", "Api V1"}</param>
            public static IServiceCollection AddCustomSwagger(this IServiceCollection services, Dictionary<string, string> versions)
            {
                services.AddSwaggerGen(options =>
                {
                    options.CustomSchemaIds(x => x.FullName);

                    options.TagActionsBy(api =>
                    {
                        if (api.GroupName != null)
                        {
                            return new[] { api.GroupName };
                        }

                        if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                        {
                            return new[] { controllerActionDescriptor.ControllerName };
                        }

                        throw new InvalidOperationException("Unable to determine tag for endpoint.");
                    });

                    AddAuthentication(options);

                    options.DocInclusionPredicate((docName, api) =>
                    {
                        return api.RelativePath.StartsWith("api");
                    });

                    foreach (var version in versions)
                    {
                        var versionParameter = GetVersion(version.Key);

                        options.SwaggerDoc(
                             versionParameter,
                             new OpenApiInfo
                             {
                                 Title = version.Value,
                                 Version = versionParameter
                             });
                    }

                    options.OperationFilter<RemoveVersionParameterFilter>();
                    options.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();
                });

                return services;
            }

            /// <summary>
            /// Configure Swagger middlewares with versions
            /// </summary>
            /// <param name="versions">Dictionary of "version" and "description" like { "V1", "Api V1"}</param>
            public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app, Dictionary<string, string> versions, string routePrefix = "")
            {
                return app.UseSwagger(c =>
                {
                    if (!string.IsNullOrEmpty(routePrefix))
                    {
                        c.PreSerializeFilters.Add((swagger, request) =>
                            swagger.Servers.Add(new OpenApiServer { Url = routePrefix, }
                        ));
                    }
                }).UseSwaggerUI(c =>
                {
                    foreach (var version in versions)
                    {
                        var versionParameter = GetVersion(version.Key);

                        c.SwaggerEndpoint($"{versionParameter}/swagger.json", version.Value);
                    }

                    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                });
            }

            public static IApplicationBuilder UseCustomRedirectFromHomeToSwagger(this IApplicationBuilder app)
            {
                var option = new RewriteOptions();
                option.AddRedirect("^$", "swagger");

                return app.UseRewriter(option);
            }

            private static string GetVersion(string versionKey)
                   => versionKey.ToLowerInvariant().Contains("v") ? versionKey : $"V{versionKey}";

            private static void AddAuthentication(SwaggerGenOptions options)
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                                Name = "Bearer",
                                In = ParameterLocation.Header
                            },
                            new List<string>()
                        }
                    });
            }

        }
    }

