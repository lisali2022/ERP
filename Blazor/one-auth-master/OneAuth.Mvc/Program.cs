var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddAuthentication()
    .AddCookie(options =>
    {
        options.LoginPath = "/";
    })
    ;


builder.Services.AddOneAuth();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseOneAuth();

app.MapRazorPages();

app.Run();
