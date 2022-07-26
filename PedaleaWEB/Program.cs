using CanonicalModel.Model.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddOptions();
builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection($"JwtConfiguration"));


builder.Services.AddSession(options =>
{
    // Set a short timeout for easy testing.
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.Name = "InfiniteSystems.SessionCookie";
    // configurar las cookies de la aplicación solo a través de una conexión segura:
    // options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.HttpOnly = true;
    //options.Cookie.Expiration = TimeSpan.FromMinutes(2);
    // Hacer que la cookie de sesión sea esencial
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCookiePolicy();

app.UseRouting();

app.UseSession();

app.UseAuthentication();

app.UseRouting();

//app.MapDefaultControllerRoute();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();


