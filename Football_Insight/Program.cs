using Football_Insight.Core.Contracts;
using Football_Insight.Core.Services;
using Football_Insight.Extensions;
using Football_Insight.Infrastructure.Data;
using Football_Insight.Infrastructure.Data.Common;
using Football_Insight.Infrastructure.Data.Models;
using Football_Insight.Jobs;
using Microsoft.EntityFrameworkCore;
using Quartz;
var builder = WebApplication.CreateBuilder(args);

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
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
})
    .AddEntityFrameworkStores<FootballInsightDbContext>();

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
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
