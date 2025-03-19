using StudyDS_web.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddSessionStateTempDataProvider();

// Configure HTTP client for StudyApi, used for interacting with the backend API
builder.Services.AddHttpClient("StudyApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7185/api/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

// Configure HTTP client for AuthorityApi
builder.Services.AddHttpClient("AuthorityApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7185/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

// Configure session management
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.IdleTimeout = TimeSpan.FromHours(5);
    options.Cookie.IsEssential = true;
});

// Add HttpContextAccessor to allow access to the HTTP context
builder.Services.AddHttpContextAccessor();

// Register custom WebApiExecuter for making API calls
builder.Services.AddTransient<IWebApiExecuter, WebApiExecuter>();

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

app.UseSession();

// Custom middleware for authentication logic
app.UseMiddleware<AuthMiddleware>();
app.UseAuthorization();

// Configure the default route for the application
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Runs the application
app.Run();
