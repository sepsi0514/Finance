using DAO.DBModels;
using Firebase.Auth.Providers;
using Firebase.Auth;
using FirebaseAdmin;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Interfaces;
using System.Data.SqlClient;
using System.Net;
using UnitOfWork.Interfaces;
using WebFinance.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Authentication.Models;

namespace WebFinance
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var firebaseCredentials = new SetUpFirebaseService().LoadFireBaseCredentials(builder.Configuration.GetConnectionString("firebase"));

            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", firebaseCredentials.GOOGLE_APPLICATION_CREDENTIALS);
            builder.Services.AddSingleton(FirebaseApp.Create());
            builder.Services.AddSession();

            SetUpAuthentication(builder, firebaseCredentials);
            SetUpDatabase(builder);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            builder.Services.AddScoped<IWalletService<Wallet>, WalletService>();

            var app = builder.Build();
            app.UseSession();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.Use(async (context, next) =>
            {
                var token = context.Session.GetString("token");
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                }
                await next();
            });

            app.UseStatusCodePages(async contextAccessor =>
            {
                var response = contextAccessor.HttpContext.Response;
                if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    response.Redirect("/Authentication/Login");
                }
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        private static void SetUpAuthentication(WebApplicationBuilder builder, FirebaseCredentials firebaseCredentials)
        {
            var firebaseProjectName = firebaseCredentials.ProjectName;
            builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig
            {
                ApiKey = firebaseCredentials.ApiKey,
                AuthDomain = $"{firebaseProjectName}.firebaseapp.com",
                Providers = new FirebaseAuthProvider[] {
                    new EmailProvider(),
                    new GoogleProvider() }
            }));

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = $"https://securetoken.google.com/{firebaseProjectName}";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = $"https://securetoken.google.com/{firebaseProjectName}",
                        ValidateAudience = true,
                        ValidAudience = firebaseProjectName,
                        ValidateLifetime = true
                    };
                });

            builder.Services.AddSingleton<IFirebaseAuthService, FirebaseAuthService>();
        }

        private static void SetUpDatabase(WebApplicationBuilder builder)
        {
            CheckDbExists(new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("financeDb")));
            builder.Services.AddDbContext<FinanceDatasContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("financeDb")));
        }

        private static void CheckDbExists(SqlConnectionStringBuilder sqlConnection)
        {
            if (File.Exists(sqlConnection.DataSource)) return;

            Directory.CreateDirectory(path: Path.GetDirectoryName(sqlConnection.DataSource) ?? throw new Exception("Can't create directory for database!"));
            File.Create(sqlConnection.DataSource).Dispose();
        }
    }
}
