using Shipping.WebApi.Swagger;
using Newtonsoft.Json.Converters;
using Microsoft.IdentityModel.Tokens;

namespace Shipping.API
{
    public static class StartupService
    {
        public const string JwtSchema = "JWTBearer";
        public const string IntrospectionSchema = "Introspection"; 
        public static readonly string DATETIME_FORMAT = "yyyy-MM-ddTHH:mm:ss.fffzzz";

        public static IServiceCollection AddCustomMvc(this IServiceCollection services, IConfiguration configuration)
        {
            // Add framework services.
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
                options.SerializerSettings.DateFormatString = DATETIME_FORMAT;
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy1",
                    builder => builder
                        .SetIsOriginAllowed((host) => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                    );
                options.AddPolicy("CorsPolicy2",
                    builder => builder
                        .SetIsOriginAllowed((host) => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding", "authorization")
                    );
                //options.AddPolicy("CorsPolicy1",
                //    builder => builder
                //        .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding", "authorization")
                //        .SetIsOriginAllowed((host) => true)
                //        .AllowAnyMethod()
                //        .AllowAnyHeader()
                //        .AllowCredentials()
                //    );
                //options.AddPolicy("CorsPolicy",
                //    builder => builder
                //        .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding", "authorization")
                //        //.SetIsOriginAllowed((host) => true)
                //        .WithOrigins(configuration.GetValue<string[]>("Origin", new string[] { "localhot" }))
                //        .SetIsOriginAllowedToAllowWildcardSubdomains()
                //        .AllowAnyMethod()
                //        .AllowAnyHeader()
                //        .AllowCredentials()
                //    );
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding", "authorization")
                        //.SetIsOriginAllowed((host) => true)
                        .WithOrigins(configuration.GetValue<string[]>("Origin", new string[] { "localhot" }))
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithMethods("GET", "PUT", "POST", "DELETE", "OPTIONS")
                        .SetPreflightMaxAge(TimeSpan.FromSeconds(3600))
                    );
            });

            return services;
        }

        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "Shipping Api", Version = "v2" });
                var security = new Dictionary<string, IEnumerable<string>>
                {
                {"Bearer", Array.Empty<string>()},
                };
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
                });
                c.OperationFilter<FileOperationFilter>();
            });

            services.AddAuthentication("token")
                // JWT tokens
                .AddJwtBearer("token", options =>
                {
                    // if token does not contain a dot, it is a reference token
                    options.ForwardDefaultSelector = ForwardReferenceToken(IntrospectionSchema, JwtSchema);
                })
                .AddJwtBearer(JwtSchema, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false
                    };

                    options.Authority = configuration.GetValue<string>("ServiceUrl:Authorization", "");
                    options.RequireHttpsMetadata = false;
                })
                // reference tokens
                .AddOAuth2Introspection(IntrospectionSchema, options =>
                {
                    options.Authority = configuration.GetValue<string>("ServiceUrl:Authorization", "");

                    options.ClientId = "pro5Cli3nt";
                    options.ClientSecret = "profile5ecret";
                });


            return services;
        }

        /// <summary>
        /// Provides a forwarding func for JWT vs reference tokens (based on existence of dot in token)
        /// </summary>
        /// <param name="introspectionScheme">Scheme name of the introspection handler</param>
        /// <returns></returns>
        public static Func<HttpContext, string> ForwardReferenceToken(string introspectionScheme, string jwtScheme)
        {
            string Select(HttpContext context)
            {
                var (scheme, credential) = GetSchemeAndCredential(context);

                if (scheme.Equals("Bearer", StringComparison.OrdinalIgnoreCase) &&
                    !credential.Contains('.', StringComparison.InvariantCulture))
                {
                    return introspectionScheme;
                }

                return jwtScheme;
            }

            return Select;
        }

        /// <summary>
        /// Extracts scheme and credential from Authorization header (if present)
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static (string, string) GetSchemeAndCredential(HttpContext context)
        {
            var header = context.Request.Headers["Authorization"].FirstOrDefault();

            if (string.IsNullOrEmpty(header))
            {
                return ("", "");
            }

            var parts = header.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2)
            {
                return ("", "");
            }

            return (parts[0], parts[1]);
        }
    }
}
