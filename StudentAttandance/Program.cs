using StudentAttandanceLibrary.Repositories.Implements;
using StudentAttandanceLibrary.Repositories.IRepositories;
using StudentAttandanceLibrary.Services;
using StudentAttandanceLibrary.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AddPageRoute("/Public/Login", "/");
});

builder.Services.AddTransient<IExcelService, ExcelService>();
builder.Services.AddTransient<ILogRepository, LogRepository>();
builder.Services.AddTransient<IAdminRepository, AdminRepository>();
builder.Services.AddTransient<IAttandanceRepository, AttandanceRepository>();
builder.Services.AddTransient<ICourseRepository, CourseRepository>();
builder.Services.AddTransient<IMailRepository, MailRepository>();
builder.Services.AddTransient<IStudentRepository, StudentRepository>();
builder.Services.AddTransient<ITeacherRepository, TeacherRepository>();
builder.Services.AddTransient<ITermRepository, TermRepository>();

builder.Services.AddSession();
//builder.Services.AddAuthentication(options =>
//    {
//        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    })
//    .AddCookie(options =>
//    {
//        options.LoginPath = "/Login";
//    });
//builder.Services.AddAuthorization(options =>
// {
//     options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("1"));
// });


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
app.UseSession();
app.UseAuthorization();
app.UseAuthentication();

app.MapRazorPages();

app.Run();
