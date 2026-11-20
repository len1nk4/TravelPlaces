using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using TravelPLaces11.Data;
using TravelPlaces11.Services;
using TravelPlaces11.Services.Contracts;

namespace TravelPlaces11.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<TravelPlaces11DbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IPlaceService, PlaceService>();
            builder.Services.AddScoped<ILookupService, LookupService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            else
            {
                // Добавяме демо данни само в режим Development
                using var scope = app.Services.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<TravelPlaces11DbContext>();

                DemoData.Seed(db);
            }

            // Десетичните числа (входна такса) винаги се въвеждат с точка, напр. 10.50
            var culture = CultureInfo.InvariantCulture;
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture),
                SupportedCultures = new List<CultureInfo> { culture },
                SupportedUICultures = new List<CultureInfo> { culture }
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Place}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
