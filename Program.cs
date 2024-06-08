using health_consulting_website.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// builder.Services.AddControllers().AddJsonOptions(options =>
//     {
//         options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
//     });

builder.Services.AddSession(options =>
    {
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });

// Thiết lập thời gian timeout cho Cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    /*
     // Thiết lập cookie chỉ được truy cập thông qua HTTP (không thể truy cập bằng JavaScript)
    options.HttpOnly = HttpOnlyPolicy.Always;

    // Thiết lập cùng một site không được yêu cầu
    options.MinimumSameSitePolicy = SameSiteMode.None;

    // Thiết lập bảo mật của cookie (ví dụ: Secure = Always chỉ cho phép truy cập qua HTTPS)
    options.Secure = CookieSecurePolicy.None; // Có thể là Always nếu chỉ cho phép truy cập qua HTTPS
 
    */
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Thiết lập thời gian timeout là 30 phút
});

// Cấu hình và kích hoạt Session
builder.Services.AddDistributedSqlServerCache(options =>
{
    options.ConnectionString = "Data Source=.\\MSSQL_2012;Initial Catalog=heathconsult;TrustServerCertificate=True;User ID=;Password=";
    options.SchemaName = "dbo";
    options.TableName = "Session";
});

builder.Services.AddSingleton<ConsultContext>();

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

// app.UseAuthentication();

// app.UseAuthorization();
app.UseMiddleware<AuthenticationMiddleware>();
app.UseMiddleware<AuthorizationMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
