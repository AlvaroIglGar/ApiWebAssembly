using ApiRestDespliegue.Interfaces;
using ApiRestDespliegue.Interfaces.Pajaros;
using ApiRestDespliegue.Services;
using ApiRestDespliegue.Services.Pajaros;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApiRestDespliegue
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 🔹 Render usa un puerto dinámico → debemos enlazarlo
            var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
            builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

            // 🔹 Controllers
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            // 🔹 Política CORS universal (Render-friendly)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("*");
                });
            });

            // 🔹 MongoDB config
            var mongoSettings = new MongoDbSettings
            {
                ConnectionString = Environment.GetEnvironmentVariable("MONGO_URI") ?? "",
                DatabaseName = Environment.GetEnvironmentVariable("MONGO_DB_NAME") ?? "AppPruebasDB",
                UsersCollectionName = Environment.GetEnvironmentVariable("MONGO_USERS_COLLECTION") ?? "Users"
            };

            builder.Services.AddSingleton(mongoSettings);
            builder.Services.AddSingleton<MongoDbService>();

            builder.Services.AddSingleton<IMongoHistoricoPajaroRepositoryService, MongoHistoricoPajaroRepositoryService>();
            builder.Services.AddSingleton<IMongoPajaroRepositoryService, MongoPajaroRepositoryService>();
            builder.Services.AddScoped<IMongoTipoComidaRepositoryService, MongoTipoComidaRepositoryService>();
            builder.Services.AddScoped<IMongoActividadRepositoryService, MongoActividadRepositoryService>();

            // 🔹 Repositorio Users
            var useInMemory = builder.Configuration.GetValue<bool>("UseInMemoryRepository");
            if (useInMemory)
                builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();
            else
                builder.Services.AddSingleton<IUserRepository, MongoUserRepository>();

            // 🔹 JWT
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

            var app = builder.Build();

            // 🔹 OpenAPI solo en Development
            if (app.Environment.IsDevelopment())
                app.MapOpenApi();

            // 🔥 Middleware para forzar CORS MANUALMENTE (Render a veces bloquea OPTIONS)
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
                context.Response.Headers.Append("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
                context.Response.Headers.Append("Access-Control-Allow-Headers", "*");

                // Responder preflight sin pasar al pipeline
                if (context.Request.Method == "OPTIONS")
                {
                    context.Response.StatusCode = 200;
                    return;
                }

                await next();
            });

            // 🔹 Aplicar política CORS
            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
