using BuchDatenbank;
using BuecherMVC.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IKonfigurationsLeser, KonfigurationsLeser>();

builder.Services.AddSingleton<KonfigurationsLeser>();
builder.Services.AddScoped<KonfigurationsLeser>();
builder.Services.AddTransient<KonfigurationsLeser>();


// Dependency Injection
builder.Services.AddScoped(
    sp => new DatenbankKontext(
        sp.GetRequiredService<KonfigurationsLeser>()
        .LiesDatenbankVerbindungZurMariaDB()));
builder.Services.AddScoped<IBuchRepository, BuchOrmRepository>();


/*builder.Services.AddScoped<IBuchRepository>(
    sp => new BuchRepository(
        sp.GetRequiredService<IKonfigurationsLeser>()
        .LiesDatenbankVerbindungZurMariaDB()));
*/

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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
