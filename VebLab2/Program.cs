using VebLab2;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog
builder.Host.UseSerilog((context, configuration) =>
{
    var logFilePath = context.Configuration["LogSettings:FilePath"];
    configuration.WriteTo.File(
        path: logFilePath,
        outputTemplate: "Час: {Timestamp:yyyy-MM-dd HH:mm:ss} | {Message}{NewLine}",
        rollingInterval: RollingInterval.Day    
    );
});

// Add services to the container.
builder.Services.AddControllersWithViews();
// Реєструємо наш сервіс для відправки пошти
builder.Services.AddTransient<VebLab2.Services.IEmailSender, VebLab2.Services.EmailSenderService>();

var app = builder.Build();

// Логер
app.UseMiddleware<RequestLoggingMiddleware>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
