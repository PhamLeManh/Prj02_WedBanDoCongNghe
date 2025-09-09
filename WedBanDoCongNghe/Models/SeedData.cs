using Microsoft.AspNetCore.Identity;
using WedBanDoCongNghe.Models;

public static class SeedData
{
    public static async Task SeedRolesAndAdmin(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
    {
        string[] roles = new[] { "Admin", "Customer" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        // Tạo Admin mặc định
        string adminEmail = "admin@tech.com";
        string adminUser = "admin";
        string adminPass = "Admin123!";

        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var admin = new AppUser { UserName = adminUser, Email = adminEmail, HoTen = "Administrator" };
            var result = await userManager.CreateAsync(admin, adminPass);
            if (result.Succeeded)
                await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}
