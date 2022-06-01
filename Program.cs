using P2Pspeedrun.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();

// Singleton because we need the data inside to be persistent between requests
builder.Services.AddSingleton<AppService>();

// Scoped, one instance for one request. --but it doesn't really matter in this case, since the implementation is ales -- but thats great anyway because the rest of the app can have final shape and the user stuff can be solved latter.
builder.Services.AddScoped<UserService>();

var app = builder.Build();

app.MapControllers();

app.UseRouting();

app.Run();

