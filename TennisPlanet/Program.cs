using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TennisPlanet.Core.Contracts;
using TennisPlanet.Core.Services;
using TennisPlanet.Infrastructure.Data;
using TennisPlanet.Infrastructure.Data.Domain;
using TennisPlanet.Infrastructure.Data.Infrastructure;

namespace TennisPlanet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseLazyLoadingProxies()
                .UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 5;

            })
                .AddRoles<IdentityRole>()
              .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            builder.Services.AddTransient<ICategoryService, CategoryService>();
            builder.Services.AddTransient<IBrandService, BrandService>();
            builder.Services.AddTransient<IProductItemService, ProductItemService>();
            builder.Services.AddTransient<IOrderService, OrderService>();
            builder.Services.AddTransient<IDimensionService, DimensionService>();
            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddTransient<IStatisticsService, StatisticsService>();
            builder.Services.AddTransient<IContactService, ContactService>();

            var app = builder.Build();
            app.PrepareDatabase();  

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}