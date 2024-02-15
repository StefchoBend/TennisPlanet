using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisPlanet.Infrastructure.Data.Domain;

namespace TennisPlanet.Infrastructure.Data.Infrastructure
{
    public static class ApplicationBuilderExtension
    {
        public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var services = serviceScope.ServiceProvider;

            await RoleSeeder(services);
            await SeedAdministrator(services);

            var dataCategory = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            SeedCategories(dataCategory);

            var dataBrand = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            SeedBrands(dataBrand);

            var dataDimension = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            SeedDimensions(dataDimension);

            return app;
        }
        private static void SeedCategories(ApplicationDbContext dataCategory)
        {
            if (dataCategory.Categories.Any())
            {
                return;
            }
            dataCategory.Categories.AddRange(new[]
            {
                new Category{CategoryName="Tennis racket"},
                new Category{CategoryName="T-shirt"},
                new Category{CategoryName="Pants"},
                new Category{CategoryName="Accessory"},
                new Category{CategoryName="Antivibrators"},
                new Category{CategoryName="Cordage"},
                new Category{CategoryName="Sack"},
                new Category{CategoryName="Shoe"}, //Pitai gospojata
            });
            dataCategory.SaveChanges();
        }
        private static void SeedBrands(ApplicationDbContext dataBrand)
        {
            if (dataBrand.Brands.Any())
            {
                return;
            }
            dataBrand.Brands.AddRange(new[]
            {
                new Brand{BrandName="Wilson"},
                new Brand{BrandName="Babolat"},
                new Brand{BrandName="Head"},
                new Brand{BrandName="Yonex"},
                new Brand{BrandName="Dunlop"},
                new Brand{BrandName="Tecnifibre"},
            });
            dataBrand.SaveChanges();
        }
        private static void SeedDimensions(ApplicationDbContext dataDimension)
        {
            if (dataDimension.Dimensions.Any())
            {
                return;
            }
            dataDimension.Dimensions.AddRange(new[]
            {
                new Dimension{Size="No size"},
                new Dimension{Size="S"},
                new Dimension{Size="M"},
                new Dimension{Size="L"},
                new Dimension{Size="XL"},
                new Dimension{Size="37"},
                new Dimension{Size="38"},
                new Dimension{Size="39"},
                new Dimension{Size="40"},
                new Dimension{Size="41"},
                new Dimension{Size="42"},
                new Dimension{Size="43"},
            });
            dataDimension.SaveChanges();
        }
        private static async Task RoleSeeder(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Administrator", "Client" };
            IdentityResult roleResult;
            foreach (var role in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
        private static async Task SeedAdministrator(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (await userManager.FindByNameAsync("admin") == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.FirstName = "admin";
                user.LastName = "admin";
                user.UserName = "admin";
                user.Email = "admin@admin.com";
                user.Address = "admin address";
                user.PhoneNumber = "0888888888";

                var result = await userManager.CreateAsync
                (user, "Admin123456");

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
        }
    }
}
