
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

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy
                        .AllowAnyOrigin() 
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });

            //        builder.Services.Configure<MongoDbSettings>(
            //builder.Configuration.GetSection("MongoDbSettings"));

            // 🔹 MongoDbSettings desde variables de entorno
            var mongoSettings = new MongoDbSettings
            {
                ConnectionString = Environment.GetEnvironmentVariable("MONGO_URI")
                    ?? "",
                DatabaseName = Environment.GetEnvironmentVariable("MONGO_DB_NAME") ?? "AppPruebasDB",
                UsersCollectionName = Environment.GetEnvironmentVariable("MONGO_USERS_COLLECTION") ?? "Users"
            };

            // 🔹 Registrar MongoDbSettings y MongoDbService
            builder.Services.AddSingleton(mongoSettings);
            builder.Services.AddSingleton<MongoDbService>();


            builder.Services.AddSingleton<MongoDbService>();
            builder.Services.AddSingleton<IMongoHistoricoPajaroRepositoryService, MongoHistoricoPajaroRepositoryService>();
            builder.Services.AddSingleton<IMongoPajaroRepositoryService, MongoPajaroRepositoryService>();
            builder.Services.AddScoped<IMongoTipoComidaRepositoryService, MongoTipoComidaRepositoryService>();


            // Registrar IUserRepository según configuración
            var useInMemory = builder.Configuration.GetValue<bool>("UseInMemoryRepository");
            if (useInMemory)
            {
                builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();
           
            }
            else
            {
                // Mongo implementation (requiere MongoDbService ya registrado)
                builder.Services.AddSingleton<IUserRepository, MongoUserRepository>();
            }

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

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            //app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            // 🔹 Primero autenticación
            app.UseAuthentication();

            // 🔹 Luego autorización
            app.UseAuthorization();

            // 🔹 Finalmente mapeas los controladores
            app.MapControllers();          

            app.Run();

        }
    }
}
