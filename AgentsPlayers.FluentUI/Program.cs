using AgentsPlayers.FluentUI.Components;
using AgentsPlayers.Persistance;
using Microsoft.EntityFrameworkCore;
using AgentsPlayers.ServicesInterfaces;
using AgentsPlayers.Services;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<AgentsPlayersContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"), opt=>
opt.EnableRetryOnFailure(
    maxRetryCount:5,
    maxRetryDelay: TimeSpan.FromSeconds(30),
    errorNumbersToAdd: null)));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddFluentUIComponents();

builder.Services.AddScoped<IAgentService, AgentService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
