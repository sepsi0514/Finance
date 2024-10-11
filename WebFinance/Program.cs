using Authentication.Models;
using Firebase.Auth;
using Firebase.Auth.Providers;
using FirebaseAdmin;
using FireStoreDao;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces.Services;
using Services.Services;
using System.Net;
using WebFinance.Services;

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

            var fireDataContext = new FinanceDatasContext(firebaseCredentials);

            builder.Services.AddSingleton<IWalletService>(new WalletService(fireDataContext));
            builder.Services.AddSingleton<IUserService>(new UserService(fireDataContext));

            SetUpAuthentication(builder, firebaseCredentials);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

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
    }
}
