using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Reflection;
using WebProje2023.Entity;
using WebProje2023.Services;

var builder = WebApplication.CreateBuilder(args);

#region
builder.Services.AddSingleton<LanguageService>();
builder.Services.AddLocalization(options =>options.ResourcesPath="Resources");
builder.Services.AddMvc().AddMvcLocalization().AddDataAnnotationsLocalization(options=>
	options.DataAnnotationLocalizerProvider = (type, factory) =>
	{
		var assemblyName=new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
		return factory.Create(nameof(SharedResource), assemblyName.Name);
	});
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
	var supportCulture = new List<CultureInfo>
	{
		new CultureInfo("en-US"),
		new CultureInfo("tr-TR"),
	};
	options.DefaultRequestCulture = new RequestCulture(culture: "tr-TR", uiCulture: "tr-TR");
	options.SupportedCultures = supportCulture;
	options.SupportedUICultures = supportCulture;
	options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
});

#endregion

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<DatabaseContext>(opts =>
{
	opts.UseNpgsql(builder.Configuration.GetConnectionString("DbDefault"));
});

builder.Services
	.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(opts =>
	{
		opts.Cookie.Name = ".FlightReservation.auth";
		opts.ExpireTimeSpan = TimeSpan.FromDays(7);    //tekrar bak
		opts.SlidingExpiration = false;
		opts.LoginPath = "/Account/Login";
		opts.LogoutPath = "/Account/Logout";
		opts.AccessDeniedPath = "/Home/AccesDenied";     //buraya tekrar bak
	});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
