
using ApiTMDT.Repositories;
using ApiTMDT.Service;
using Data;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
//builder.Services.AddScoped<AccountRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PasswordHasher>(); // hoặc services.AddScoped<Helperss>();

builder.Services.AddDbContext<UserContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
        ));
//services cors
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("corsapp");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
