using Microsoft.AspNetCore.Identity;
using OneTimePassword.Api.Configurations;
using OneTimePassword.DataAccess;
using OneTimePassword.DataAccess.Seed;
using OneTimePassword.Model.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<DBContext>();

builder.Services.AddServices();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(jwtSettings);


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "DefaultPolicy",
                      policy =>
                      {
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                          policy.WithOrigins("https://localhost:7070");
                      });
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("DefaultPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    await Seed.EnsureUsers(userManager);
}

app.Run();