using blogapp.Controllers;
using blogapp.Services;
using blogapp.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();


//builder.Services.AddControllers();


string address, database, user, password;

Console.WriteLine("Enter servers address: ");
address = Console.ReadLine();
Console.WriteLine("Enter database name: ");
database = Console.ReadLine();
Console.WriteLine("Enter username: ");
user = Console.ReadLine();
Console.WriteLine("Enter password: ");
password = Console.ReadLine();

string connectionString = "server=" + address +
    ";database=" + database +
    ";user=" + user + 
";password=" + password + ";";

//configuring services

AppDbContext context = new AppDbContext(connectionString);

builder.Services.AddTransient<SQLArticleService>();
builder.Services.AddTransient<SQLCommentService>();
builder.Services.AddTransient<SQLLogedService>();
builder.Services.AddTransient<SQLReactionService>();



builder.Services.AddDistributedMemoryCache();

// To ue cookies data in layout.cshtml
builder.Services.AddHttpContextAccessor();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.UseSession();

app.MapRazorPages();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllers();
});

app.UseStaticFiles();

app.Run();


public class MyOptions
{
    public string Test { get; set; }
}