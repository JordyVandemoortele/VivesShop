using System.Collections.Generic;
using VivesShop.Core;
using VivesShop.Models;
using Microsoft.EntityFrameworkCore;
using VivesShop.Services;

namespace VivesShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<VivesShopDbContext>(options =>
            {
                options.UseInMemoryDatabase(nameof(VivesShopDbContext));
            });

            builder.Services.AddScoped<ProductCategoryService>();
            builder.Services.AddScoped<ProductService>();
            builder.Services.AddSingleton<User>();

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
                var scope = app.Services.CreateScope();
                var database = scope.ServiceProvider.GetRequiredService<VivesShopDbContext>();
                database.Seed();
                var CurrentUser = app.Services.GetRequiredService<User>();
                CurrentUser.FirstName = "Jordy";
                CurrentUser.LastName = "Vandemoortele";
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
    }
}