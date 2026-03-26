using BandScope.Common;
using BandScope.DataAccess;
using BandScope.DataAccess.Context;
using BandScope.DataAccess.Interfaces;
using BandScope.Logic;
using BandScope.Logic.Interfaces;
using BandScope.Server.Configuration;
using BandScope.Server.External;
using BandScope.Server.Filter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

namespace BandScope.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            ConfigureServices(builder);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(Const.CorsPolicyAllowClient);

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((ctx, lc) =>
                lc.ReadFrom.Configuration(ctx.Configuration));

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
            });

            builder.Services.AddDbContextFactory<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.Configure<TheAudioDbSettings>(
                builder.Configuration.GetSection(TheAudioDbSettings.SectionName));

            builder.Services.AddHttpClient<ITheAudioDbClient, TheAudioDbClient>((sp, client) =>
            {
                var s = sp.GetRequiredService<IOptions<TheAudioDbSettings>>().Value;

                var baseUrl = s.BaseUrl.TrimEnd('/');
                var apiKey = s.ApiKey.Trim('/');

                client.BaseAddress = new Uri($"{baseUrl}/{apiKey}/");
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(Const.CorsPolicyAllowClient, p =>
                    p.WithOrigins("https://localhost:5001")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                    ValidAudience = builder.Configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            builder.Services.AddScoped<IUserDataAccess, UserDataAccess>();
            builder.Services.AddScoped<IArtistDataAccess, ArtistDataAccess>();
            builder.Services.AddScoped<IReviewDataAccess, ReviewDataAccess>();
            builder.Services.AddScoped<IGenreDataAccess, GenreDataAccess>();
            builder.Services.AddScoped<IStyleDataAccess, StyleDataAccess>();
            builder.Services.AddScoped<IPictureDataAccess, PictureDataAccess>();
            builder.Services.AddScoped<IAlbumDataAccess, AlbumDataAccess>();

            builder.Services.AddScoped<IUserLogic, UserLogic>();
            builder.Services.AddScoped<IArtistLogic, ArtistLogic>();
            builder.Services.AddScoped<IReviewLogic, ReviewLogic>();
            builder.Services.AddScoped<IGenreLogic, GenreLogic>();
            builder.Services.AddScoped<IStyleLogic, StyleLogic>();
            builder.Services.AddScoped<IPictureLogic, PictureLogic>();
            builder.Services.AddScoped<IAlbumLogic, AlbumLogic>();
        }
    }
}
