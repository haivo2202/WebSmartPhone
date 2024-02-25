using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project_PhoneStore.Models;
using Project_PhoneStore.Repository;
using System;

var builder = WebApplication.CreateBuilder(args);
//ConnectionDb
builder.Services.AddDbContext<DataContext>(option =>

	option.UseSqlServer(builder.Configuration["ConnectionStrings:ConnectedDb"])
);

builder.Services.AddDefaultIdentity<Account>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
   .AddEntityFrameworkStores<DataContext>();


// Add services to the container.


builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(15);
    options.Cookie.IsEssential = true;
});




//builder.Services.AddIdentity<AppUserModel,IdentityRole>()
//.AddEntityFrameworkStores<DbContext>().AddDefaultTokenProviders();


//builder.Services.Configure<IdentityOptions>(options =>
//{
// Password settings.
//options.Password.RequireDigit = true;
//options.Password.RequireLowercase = true;
//options.Password.RequireNonAlphanumeric = false;
//options.Password.RequireUppercase = false;
//options.Password.RequiredLength = 4;
//options.User.RequireUniqueEmail = true;
//});


var app = builder.Build();





app.UseStatusCodePagesWithRedirects("/Home/Error?statuscode={0}");


    app.UseSession();
    app.UseStaticFiles();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
    }

    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();

    app.UseAuthorization();



// Configure the HTTP request pipeline.



//backend

app.MapControllerRoute(
         name: "admin",
            pattern: "{area:exists}/{controller=Report}/{action=Index}/{id?}");

 app.MapControllerRoute(
          name: "admin.category",
            pattern: "{area:exists}/{controller=Category}/{action=Index}/{id?}");


app.MapControllerRoute(
          name: "admin.product",
            pattern: "{area:exists}/{controller=Product}/{action=Index}/{id?}");

app.MapControllerRoute(
          name: "admin.brand",
            pattern: "{area:exists}/{controller=Brand}/{action=Index}/{id?}");

app.MapControllerRoute(
          name: "admin.product",
            pattern: "{area:exists}/{controller=Product}/{action=Index}/{id?}");

app.MapControllerRoute(
          name: "admin.model",
            pattern: "{area:exists}/{controller=Model}/{action=Index}/{id?}");

app.MapControllerRoute(
          name: "admin.order",
            pattern: "{area:exists}/{controller=Order}/{action=Index}/{id?}");

app.MapControllerRoute(
          name: "admin.profile",
            pattern: "{area:exists}/{controller=Profile}/{action=Index}/{id?}");

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");


//luu du lieu ao
var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
    SeedData.SeedingData(context);

    

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roleNames = new[] { "Admin", "User", "Staff" }; // Add any additional roles
    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
            await roleManager.CreateAsync(new IdentityRole(roleName));

    }

}
app.Run();

