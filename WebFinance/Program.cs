using DAO.DBModels;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Interfaces;
using System.Data.SqlClient;
using UnitOfWork;
using UnitOfWork.Interfaces;

namespace WebFinance
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            SetUpDatabase(builder);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            builder.Services.AddScoped<IWalletService<Wallet>, WalletService>();

            var app = builder.Build();


            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
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
