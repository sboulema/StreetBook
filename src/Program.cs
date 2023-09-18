using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StreetBook.Policies;
using StreetBook.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddOutputCache(options => options.AddPolicy("AuthenticatedOutputCache", new AuthenticatedOutputCachePolicy()));

builder.Services.AddControllersWithViews();

builder.Services
    .AddAuthentication()
    .AddCookie(options => options.LoginPath = "/account/login");

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IStreetBookService, StreetBookService>();
builder.Services.AddScoped<IPictureService, PictureService>();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseOutputCache();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();