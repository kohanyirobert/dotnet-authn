using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication10.Data;
using WebApplication10.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddAuthentication()
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);

builder.Services
    .AddIdentityCore<IdentityUser>(options =>
    {
        options.Password = new PasswordOptions
        {
            RequiredLength = 1,
            RequireDigit = false,
            RequireLowercase = false,
            RequireUppercase = false,
            RequireNonAlphanumeric = false,
            RequiredUniqueChars = 0
        };
    })
    .AddRoles<IdentityRole>()
    .AddSignInManager()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetService<ApplicationDbContext>();
    dbContext!.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
    var userRole = new IdentityRole {Name = "User"};
    var adminRole = new IdentityRole {Name = "Admin"};
    var userUser = new IdentityUser {Email = "user@user", UserName = "user"};
    var adminUser = new IdentityUser {Email = "admin@admin", UserName = "admin"};
    var roleManager = services.GetService<RoleManager<IdentityRole>>();
    await roleManager!.CreateAsync(userRole);
    await roleManager.CreateAsync(adminRole);
    var userManager = services.GetService<UserManager<IdentityUser>>();
    await userManager!.CreateAsync(userUser, userUser.UserName);
    await userManager!.CreateAsync(adminUser, adminUser.UserName);
    await userManager.AddToRoleAsync(userUser, userRole.Name);
    await userManager.AddToRoleAsync(adminUser, adminRole.Name);
}

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
