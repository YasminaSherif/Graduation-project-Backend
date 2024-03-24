using Graduation_project.Repository.Auth;
using Graduation_project.Repository.CustomerRepo;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "JwtToken",
                Version = "v1",
            });

            // Add security definition for JWT bearer token
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter JWT Token Like 'Bearer {your token}'"
            });

            // Add security requirement for JWT bearer token
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
        });

        builder.Services.AddDbContext<ApplicationContext>(OptionsBuilder =>
        {
            OptionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
        });

        builder.Services.AddScoped<IAuthentication, AuthenticationRepo>();
        builder.Services.AddScoped<ICustomer, CustomerRepo>();

        // Add JWT authentication
        var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JWToptions>();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // Specify default authentication scheme
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey))
                };
            });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthentication(); // Use authentication middleware
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

}
