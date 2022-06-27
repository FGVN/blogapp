using blogapp.Controllers;
using blogapp.Services;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//builder.Services.AddControllers();


//configuring services
builder.Services.AddTransient<LoginController>();
builder.Services.AddTransient<JsonLogedService>();

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


app.MapRazorPages();

//JsonLogedService loged = new JsonLogedService(this.IWebHostEnvironment);
//LoginController cont = new LoginController(loged);

//app.MapGet("/GetLoged", () => );

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllers();
    //endpoints.MapGet("/products", (context) =>
    //{
    //    var products = app.ApplicationServices.GetService<JsonFileProductService>().GetProducts();
    //    var json = JsonSerializer.Serialize<IEnumerable<Product>>(products);
    //    return context.Response.WriteAsync(json);
    //});
});

app.Run();
