
using Application.NadinSoft.Behaviors;
using Application.NadinSoft.Command;
using Application.NadinSoft.CommandHandler;
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
            builder.Services.AddDbContext<APPDbcontext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("NadinSoftDB"))
                 );

            builder.Services
            .AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<APPDbcontext>()
            .AddDefaultTokenProviders();



            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

            builder.Services.AddAuthorization();

            // DI Mapster
            builder.Services.AddMapster();
            // ثبت MediatR
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommandHandler).Assembly));
            // Command and commandHandler
            builder.Services.AddScoped<IRequestHandler<RegisterUserCommand, string>, RegisterUserCommandHandler>();
            // Repository
            builder.Services.AddScoped(typeof(ICrudRepository<>), typeof(CrudRepository<>));

            builder.Services.AddValidatorsFromAssemblyContaining<RegisterUserCommandValidator>();

            // ثبت Validatorها
            builder.Services.AddValidatorsFromAssemblyContaining<RegisterUserCommandValidator>();

            // ثبت Behavior برای اعتبارسنجی خودکار
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            //------------------------
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
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
