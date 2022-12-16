using Microsoft.EntityFrameworkCore;
using API.Data.Seed;
using Microsoft.AspNetCore.Identity;
using API.Data.Entities;

namespace API.Data.Seed
{
    internal static class SeedData
    {
        internal async static Task Initialize(IServiceProvider serviceProvider, UserManager<AppUser> userManager)
        {
            using (var context = new GameStoreDb(serviceProvider
                .GetRequiredService<DbContextOptions<GameStoreDb>>()))
            {
                var resources = new Resources();

                context.Database.EnsureCreated();
                context.Database.OpenConnection();
                try
                {
                    if (!userManager.Users.Any())
                    {
                        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Publishers ON");
                        foreach (var user in resources.AppUsers)
                        {
                            await userManager.CreateAsync(user, "Pa$$W0rd");
                            await userManager.AddToRoleAsync(user, "Member");
                        }
                    }
                    if (!context.Publishers.Any())
                    {
                        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Publishers ON");
                        context.Publishers.AddRange(resources.PublisherList);
                        context.SaveChanges();
                        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Publishers OFF");
                    }

                    if (!context.Games.Any())
                    {
                        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Games ON");
                        context.Games.AddRange(resources.GameList);
                        context.SaveChanges();
                        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Games OFF");
                    }

                    if (!context.Categories.Any())
                    {
                        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Categories ON");
                        context.Categories.AddRange(resources.CategoryList);
                        context.SaveChanges();
                        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Categories OFF");
                    }

                    if (!context.GameCategories.Any())
                    {
                        context.GameCategories.AddRange(resources.GameCategoryList);
                        context.SaveChanges();
                    }
                }
                finally
                {
                    context.Database.CloseConnection();
                } 
            }
        }

        internal async static Task CreateRoles(RoleManager<AppRole> roleManager)
        {
            var appRoles = new List<AppRole>
                    {
                        new AppRole("MEMBER"),
                        new AppRole("ADMIN")
                    };
            foreach (var role in appRoles)
            {
                if (!await roleManager.RoleExistsAsync(role.Name))
                {
                    await roleManager.CreateAsync(role);
                }
            }
        }
    }
}
