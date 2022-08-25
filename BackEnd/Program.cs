using BackEnd.Data;
using BackEnd.Endpoints;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllersWithViews();
//builder.Services.AddCors();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(o =>
{
    o.Cookie.Name = "__Host-spa";
    o.Cookie.SameSite = SameSiteMode.Strict;
    o.Events.OnRedirectToLogin = (context) =>
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };
});

builder.Services.AddAuthorization(o =>
    o.AddPolicy("admin", p => p.RequireClaim("role", "Admin"))
);

builder.Services.AddDbContext<HouseDbContext>(o =>
  o.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
builder.Services.AddScoped<IHouseRepository, HouseRepository>();
builder.Services.AddScoped<IBidRepository, BidRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseStaticFiles();
app.UseAuthentication();

//app.UseCors(p => p.WithOrigins("http://localhost:3000")
//    .AllowAnyHeader().AllowAnyMethod());

app.MapHouseEndpoints();
app.MapBidEndpoints();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(e => e.MapDefaultControllerRoute());
app.MapFallbackToFile("index.html");

app.Run();