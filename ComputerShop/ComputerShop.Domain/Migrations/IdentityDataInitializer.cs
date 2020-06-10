using ComputerShop.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.Domain.Migrations
{
    public class IdentityDataInitializer
    {
        public static void SeedData(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager, 
            ComputerShopContext computerShopContext)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager, computerShopContext);
            SeedCategories(computerShopContext);
            SeedKits(computerShopContext);
        }

        private static void SeedUsers(UserManager<IdentityUser> userManager, ComputerShopContext computerShopContext)
        {
            if (userManager.FindByNameAsync("user1@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "user1@localhost";
                user.Email = "user1@localhost";

                IdentityResult result = userManager.CreateAsync
                (user, "Secret123!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                                        "Customer").Wait();

                    var customer = new Customer()
                    {
                        Id = Guid.NewGuid(),
                        IdentityUser = user,
                        Account = new Account()
                        {
                            Id = Guid.NewGuid(),
                            Amount = 1000,
                            CreatedDate = DateTime.UtcNow,
                        }, 
                        CreatedDate = DateTime.UtcNow,
                        Email = user.Email, 
                        Name = user.Email
                    };

                    computerShopContext.Customers.Add(customer);
                    computerShopContext.SaveChanges();
                }
            }


            if (userManager.FindByNameAsync("user2@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "user2@localhost";
                user.Email = "user2@localhost";

                IdentityResult result = userManager.CreateAsync
                (user, "Secret123!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                                        "Admin").Wait();
                }
            }
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Customer").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Customer";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
        }

        private static void SeedCategories(ComputerShopContext computerShopContext)
        {
            if(!computerShopContext.Categories.Any(c => c.Name == "Memory"))
            {
                var category = new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Memory",
                    CreatedDate = DateTime.UtcNow
                };
                computerShopContext.Categories.Add(category);
            }
            if (!computerShopContext.Categories.Any(c => c.Name == "Motherboard"))
            {
                var category = new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Motherboard",
                    CreatedDate = DateTime.UtcNow
                };
                computerShopContext.Categories.Add(category);
            }
            if (!computerShopContext.Categories.Any(c => c.Name == "Monitor"))
            {
                var category = new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Monitor",
                    CreatedDate = DateTime.UtcNow
                };
                computerShopContext.Categories.Add(category);
            }
            if (!computerShopContext.Categories.Any(c => c.Name == "Videocard"))
            {
                var category = new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Videocard",
                    CreatedDate = DateTime.UtcNow
                };
                computerShopContext.Categories.Add(category);
            }
            if (!computerShopContext.Categories.Any(c => c.Name == "Soundcard"))
            {
                var category = new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Soundcard",
                    CreatedDate = DateTime.UtcNow
                };
                computerShopContext.Categories.Add(category);
            }
            if (!computerShopContext.Categories.Any(c => c.Name == "Storrage"))
            {
                var category = new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Storrage",
                    CreatedDate = DateTime.UtcNow
                };
                computerShopContext.Categories.Add(category);
            }
            computerShopContext.SaveChanges();
        }
        private static void SeedKits(ComputerShopContext computerShopContext)
        {
            var memoryCategory = computerShopContext.Categories.FirstOrDefault(c => c.Name == "Memory");
            if(!computerShopContext.Kits.Any(x => x.Name == "SSD Kingston 128 GB"))
            {
                var kit = new Kit()
                {
                    Id = Guid.NewGuid(),
                    Category = memoryCategory,
                    Name = "SSD Kingston 128 GB",
                    Price = 130,
                    CreatedDate = DateTime.UtcNow
                };
                computerShopContext.Kits.Add(kit);
            }

            if (!computerShopContext.Kits.Any(x => x.Name == "SSD Kingston 516 GB"))
            {
                var kit = new Kit()
                {
                    Id = Guid.NewGuid(),
                    Category = memoryCategory,
                    Name = "SSD Kingston 516 GB",
                    Price = 170,
                    CreatedDate = DateTime.UtcNow
                };
                computerShopContext.Kits.Add(kit);
            }
            computerShopContext.SaveChanges();
        }
    }
}
