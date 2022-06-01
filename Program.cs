using P2Pspeedrun.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();

// Singleton because we need the data inside to be persistent between requests
builder.Services.AddSingleton<AppService>();

// Scoped, but it doesn't really matter in this case, since the implementation is „static“
builder.Services.AddScoped<UserService>();

var app = builder.Build();

app.MapControllers();

app.UseRouting();

app.Run();

