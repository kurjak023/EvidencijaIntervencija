using AplikacioniSloj;
using DomenskiSloj;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SlojPodataka.Interfejsi;
using SlojPodataka.Repozitorijumi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddMvc().AddSessionStateTempDataProvider();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IKorisnikRepo>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var stringKonekcije = configuration.GetConnectionString("MojKonekcioniString");
    return new clsKorisnikRepo(stringKonekcije);
});

builder.Services.AddScoped<IOglasRepo>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var stringKonekcije = configuration.GetConnectionString("MojKonekcioniString");
    return new clsOglasRepo(stringKonekcije);
});

builder.Services.AddScoped<IIntervencijaRepo>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var stringKonekcije = configuration.GetConnectionString("MojKonekcioniString");
    return new clsIntervencijaRepo(stringKonekcije);
});

builder.Services.AddScoped<clsKorisnikServis>();
builder.Services.AddScoped<clsOglasServis>();
builder.Services.AddScoped<clsIntervencijaServis>();
builder.Services.AddScoped<clsPoslovnaPravila>();


var app = builder.Build();

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSession();

app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Pocetna}/{id?}");

app.Run();
