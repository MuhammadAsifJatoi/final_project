using AsifBlog.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using AsifBlog.Repository.Implementaion;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AsifBlogDbContext>(Options =>
{
    Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
},ServiceLifetime.Transient);
builder.Services.AddTransient<AsifBlog.Repository.IUserAccount,AccountRepository>(p => new AccountRepository(builder.Services.BuildServiceProvider().GetService<AsifBlogDbContext>()));
builder.Services.AddTransient<AsifBlog.Repository.IUser,UserRepository>(p => new UserRepository(builder.Services.BuildServiceProvider().GetService<AsifBlogDbContext>()));
builder.Services.AddTransient<AsifBlog.Repository.IPost, PostRepository>(p => new PostRepository(builder.Services.BuildServiceProvider().GetService<AsifBlogDbContext>()));
builder.Services.AddControllersWithViews();
//builder.Services.AddTransient<AsifBlog.Repository.IUser, UserRepository>(p => new UserRepository(builder.Services.BuildServiceProvider().GetService<AsifBlogDbContext>()));
//builder.Services.AddControllersWithViews();
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
