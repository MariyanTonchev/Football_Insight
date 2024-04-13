using Football_Insight.Core.Contracts;
using Football_Insight.Core.Services;
using Football_Insight.Extensions;
using Football_Insight.Infrastructure.Data;
using Football_Insight.Infrastructure.Data.Common;
using Football_Insight.Infrastructure.Data.Models;
using Football_Insight.Jobs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/Foobtall-Insight-Logs.txt", rollingInterval: RollingInterval.Day));

builder.Services.AddQuartz(q =>
{
    // Configure a job and trigger with Quartz
    q.UseMicrosoftDependencyInjectionJobFactory();
    q.AddJobAndTrigger<MatchStartJob>();
});

builder.Services.AddMemoryCache();
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FootballInsightDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<FootballInsightDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/User/Account/AccessDenied";
    options.SlidingExpiration = true;
});

builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<ILeagueService, LeagueService>();
builder.Services.AddScoped<IMatchService, MatchService>();
builder.Services.AddScoped<IStadiumService, StadiumService>();
builder.Services.AddScoped<IMatchTimerService, MatchTimerService>();
builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddScoped<IMatchJobService, MatchJobService>();
builder.Services.AddScoped<IGoalService, GoalService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();

builder.Services.AddControllersWithViews()
    .AddMvcOptions(options =>
    {

    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error/InternalServerError");
    app.UseStatusCodePagesWithReExecute("/Error/HandleError", "?statusCode={0}");
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.UseEndpoints(endpoints =>
{
    // Map area route
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    // Map controller route
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{area=User}/{controller=Home}/{action=Index}/{id?}");

    // Map Razor Pages
    endpoints.MapRazorPages();
});


app.Run();
