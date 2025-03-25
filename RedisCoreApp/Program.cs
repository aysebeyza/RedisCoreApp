
using RedisCoreApp.Services;
using StackExchange.Redis;//redis k�t�phanesini ekledik

var builder = WebApplication.CreateBuilder(args);

// Redis ba�lant�s�n� olu�turuyoruz
builder.Services.AddSingleton<IConnectionMultiplexer>(sp=>
{
    return ConnectionMultiplexer.Connect("localhost:6379");//redis ba�lant� adresi
    //var configuration =builder.Configuration.GetValue<string>("Redis:ConnectionString");
    //return ConnectionMultiplexer.Connect("configuration");
});

builder.Services.AddSingleton<RedisCacheService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=RedisCache}/{action=Anasayfa}/{id?}");

app.Run();
