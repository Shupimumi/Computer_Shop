using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerShop.Domain.Migrations
{
    public class IdentityDataInitializer
    {
        public static void SeedData
(UserManager<IdentityUser> userManager,
RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        private static void SeedUsers
    (UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByNameAsync
("user1@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "user1@localhost";
                user.Email = "user1@localhost";

                IdentityResult result = userManager.CreateAsync
                (user, "Secret123!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                                        "NormalUser").Wait();
                }
            }


            if (userManager.FindByNameAsync
        ("user2@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "user2@localhost";
                user.Email = "user2@localhost";

                IdentityResult result = userManager.CreateAsync
                (user, "Secret123!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                                        "Administrator").Wait();
                }
            }
        }

        private static void SeedRoles
    (RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync
("NormalUser").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "NormalUser";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync
        ("Administrator").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Administrator";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
        }
    }
}
