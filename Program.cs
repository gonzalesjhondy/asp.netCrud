using BloggWeb.Data;
using BloggWeb.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages().AddRazorRuntimeCompilation(); //dire ko na error dapita almost 3hrs

// Add services to the container.
builder.Services.AddControllersWithViews();
//inject DbContext
builder.Services.AddDbContext<BloggieWebDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BloggieWebConnectionString")));

//adding an injection inside TagRepository 
builder.Services.AddScoped<ITagRepository,TagRepository>();
//addning an injection inside the BlogPoseRepository
builder.Services.AddScoped<IBlogPostRepository,BlogPostRepository>();

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
