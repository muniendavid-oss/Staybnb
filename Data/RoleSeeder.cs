using Microsoft.AspNetCore.Identity;
using Staybnb.Models;
namespace Staybnb.Data;

public static class RoleSeeder {
 public static async Task SeedAsync(IServiceProvider services) {
  var roles = services.GetRequiredService<RoleManager<IdentityRole>>(); var users = services.GetRequiredService<UserManager<ApplicationUser>>();
  foreach (var role in new[]{"Guest","Host","Admin","SuperAdmin"}) if(!await roles.RoleExistsAsync(role)) await roles.CreateAsync(new IdentityRole(role));
  
  // Configure these environment variables before first deployment. A development-only fallback is provided for local setup.
  var email = Environment.GetEnvironmentVariable("STAYBNB_ADMIN_EMAIL") ?? "superadmin@staybnb.local";
  var password = Environment.GetEnvironmentVariable("STAYBNB_ADMIN_PASSWORD");
  if (string.IsNullOrWhiteSpace(password))
{
    throw new InvalidOperationException(
        "Set the STAYBNB_ADMIN_PASSWORD environment variable before starting the application.");
}
  var user = await users.FindByEmailAsync(email);
  if(user == null) { user = new ApplicationUser { UserName=email, Email=email, EmailConfirmed=true, DisplayName="Staybnb SuperAdmin" }; var result=await users.CreateAsync(user,password); if(!result.Succeeded) throw new InvalidOperationException("Could not seed SuperAdmin: "+string.Join("; ",result.Errors.Select(e=>e.Description))); }
  if(!await users.IsInRoleAsync(user,"SuperAdmin")) await users.AddToRoleAsync(user,"SuperAdmin");
 }
}