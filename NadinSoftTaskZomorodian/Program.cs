
using Application.NadinSoft.Behaviors;
using Application.NadinSoft.Command.ProductCommand;
using Application.NadinSoft.Command.User;
using Application.NadinSoft.CommandHandler.ProductCommandHandler;
using Application.NadinSoft.CommandHandler.UserCommandHandler;
using Application.NadinSoft.Query;
using Application.NadinSoft.QueryHandler;
using Application.NadinSoft.Validators;
using Domain.NadinSoft.Interface;
using Domain.NadinSoft.Model;
using FluentValidation;
using Infrustructure.NadinSoft.Context;
using Infrustructure.NadinSoft.Middlewares;
using Infrustructure.NadinSoft.Repository;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace NadinSoftTaskZomorodian
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            //------------------------
            builder.Services.AddDbContext<NadinSoftDbcontext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("NadinSoftDB"))
                 );

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<NadinSoftDbcontext>()
            .AddDefaultTokenProviders();

            // بررسی تنظیمات JWT
            var jwtKey = builder.Configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey))
                throw new InvalidOperationException("JWT Key not found in configuration.");

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = builder.Environment.IsDevelopment() ? false : true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                };
            });

            builder.Services.AddAuthorization();

            // DI Mapster
            builder.Services.AddMapster();
            // ثبت MediatR
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommandHandler).Assembly));

            // Command and commandHandler
            builder.Services.AddScoped<IRequestHandler<RegisterUserCommand, string>, RegisterUserCommandHandler>();
            builder.Services.AddScoped<IRequestHandler<LoginUserCommand, string>, LoginUserCommandHandler>();
            builder.Services.AddScoped<IRequestHandler<AddProductCommand, string>, AddProductCommandHandler>();
            builder.Services.AddScoped<IRequestHandler<ShowAllProductQuery, List<Product>>, ShowAllProductQueryHandler>();
            builder.Services.AddScoped<IRequestHandler<ShowProductWhitUserIdQuery, List<Product>>, ShowProductWhitUserIdQueryHandler>();

            // Repository
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IUserReadonlyRepository, UserReadonlyRepository>();
            builder.Services.AddScoped<IProductReadonlyRepository, GetProductReadonlyRepository>();


            // ثبت Validatorها
            builder.Services.AddValidatorsFromAssemblyContaining<RegisterUserCommandValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<LoginUserCommandValidator>();

            builder.Services.AddValidatorsFromAssemblyContaining<AddProductCommandValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UpdateProductCommandValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<DeletedProductCommandValidator>();

            builder.Services.AddValidatorsFromAssemblyContaining<ShowProductWhitUserIdQueryValidator>();



            // ثبت Behavior برای اعتبارسنجی خودکار
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            builder.Services.AddHttpContextAccessor();

            //------------------------
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Please enter JWT with Bearer prefix. Example: \"Bearer {token}\""
                });

                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });



            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<NadinSoftDbcontext>();
                dbContext.Database.Migrate();  
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
