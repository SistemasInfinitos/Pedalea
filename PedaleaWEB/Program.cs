using CanonicalModel.Model.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddOptions();
builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection($"JwtConfiguration"));

//builder.Services.Configure<CookiePolicyOptions>(options =>
//{
//    // Esta lambda determina si se necesita el consentimiento del usuario para las cookies no esenciales
//    // para una solicitud dada.
//    options.CheckConsentNeeded = context => true;
//    options.MinimumSameSitePolicy = SameSiteMode.Strict;
//});

//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.Cookie.Name = "InfiniteSystems.AppCookie";
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//    // configurar las cookies de la aplicación solo a través de una conexión segura:
//    // opciones.Cookie.SecurePolicy = CookieSecurePolicy.Always;
//    options.Cookie.SameSite = SameSiteMode.Strict;
//    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
//    options.SlidingExpiration = true;
//});

//builder.Services.AddControllers()
//    .ConfigureApiBehaviorOptions(options =>
//    {
//        options.SuppressConsumesConstraintForFormFileParameters = true;
//        options.SuppressInferBindingSourcesForParameters = true;
//        options.SuppressModelStateInvalidFilter = true;
//        options.SuppressMapClientErrors = true;
//        options.ClientErrorMapping[StatusCodes.Status404NotFound].Link =
//            "https://httpstatuses.com/404";
//    });

// Según https://github.com/aspnet/AspNetCore/issues/5828
// la configuración de la cookie se sobrescribiría si se usa la interfaz de usuario predeterminada, por lo que
// necesitamos "post-configurar" la cookie de autenticación
//builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme, options =>
//{
//    options.AccessDeniedPath = "/access-denied";
//    options.LoginPath = "/login";
//    options.LogoutPath = "/logout";

//    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
//});


//builder.Services.AddAuthorization(options =>
//{
//    options.DefaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
//});


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
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseStatusCodePagesWithReExecute("/status-code", "?code={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCookiePolicy();

app.UseRouting();

app.UseSession();

app.UseAuthentication();


//app.UseAuthorization();

app.UseRouting();
//app.MapDefaultControllerRoute();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    //endpoints.MapRazorPages();
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
//app.MapControllerRoute( name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
//app.MapControllers();
app.Run();


