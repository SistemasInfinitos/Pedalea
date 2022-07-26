using CanonicalModel.Model.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddOptions();
builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection($"JwtConfiguration"));

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // Esta lambda determina si se necesita el consentimiento del usuario para las cookies no esenciales
    // para una solicitud dada.
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Strict;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "InfiniteSystems.AppCookie";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    // configurar las cookies de la aplicaci�n solo a trav�s de una conexi�n segura:
    // opciones.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
    options.SlidingExpiration = true;
});

// Seg�n https://github.com/aspnet/AspNetCore/issues/5828
// la configuraci�n de la cookie se sobrescribir�a si se usa la interfaz de usuario predeterminada, por lo que
// necesitamos "post-configurar" la cookie de autenticaci�n
builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme, options =>
{
    options.AccessDeniedPath = "/access-denied";
    options.LoginPath = "/login";
    options.LogoutPath = "/logout";

    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
});
builder.Services.AddAntiforgery();

builder.Services.AddControllersWithViews(options =>
{
    // Slugify rutas para que podamos usar /employee/employee-details/1 en lugar de
    // el valor predeterminado /Employee/EmployeeDetails/1
    //
    // Usar un transformador de par�metros de salida es una mejor opci�n ya que tambi�n permite
    // la creaci�n de rutas correctas utilizando ayudantes de vista
    //options.Conventions.Add(new RouteTokenTransformerConvention( new SlugifyParameterTransformer()));

    // Habilitar la funci�n Antifalsificaci�n de forma predeterminada en todas las acciones del controlador
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});

//builder.Services.AddRazorPages(options =>
//{
//    options.Conventions.AddAreaPageRoute("Identity", "/Account/Register", "/register");
//    options.Conventions.AddAreaPageRoute("Identity", "/Account/Login", "/login");
//}).SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
});


builder.Services.AddSession(options =>
{
    // Set a short timeout for easy testing.
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.Name = "InfiniteSystems.SessionCookie";
    // configurar las cookies de la aplicaci�n solo a trav�s de una conexi�n segura:
    // options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.HttpOnly = true;
    //options.Cookie.Expiration = TimeSpan.FromMinutes(2);
    // Hacer que la cookie de sesi�n sea esencial
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

app.UseStatusCodePagesWithReExecute("/status-code", "?code={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCookiePolicy();

app.UseRouting();

app.UseSession();

app.UseAuthentication();


app.UseAuthorization();

app.MapControllerRoute( name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
