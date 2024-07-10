
using ApiTMDT.Repositories;
using ApiTMDT.Service;
using Data;
using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design.Serialization;
using System.Text.Json.Serialization;
using ApiTMDT.Data;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cấu hình CORS
builder.Services.AddCors(options => options.AddDefaultPolicy
(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
//builder.Services.AddScoped<AccountRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PasswordHasher>(); 
builder.Services.AddScoped<SanPhamService>();
builder.Services.AddScoped<NhanVienSevice>();
builder.Services.AddScoped<PhongBanService>();
builder.Services.AddScoped<HopDongLaoDongService>();
builder.Services.AddScoped<HocVanService>();
builder.Services.AddScoped<NghiPhepService>();
builder.Services.AddScoped<KhachHangSevice>();
builder.Services.AddScoped<HoaDonService>();
builder.Services.AddScoped<ChiTietHoaDonService>();

builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

builder.Services.AddDbContext<ApiDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
        ));
// Configure DinkToPdf
var context = new CustomAssemblyLoadContext();
context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.dll"));
builder.Services.AddSingleton(typeof(DinkToPdf.Contracts.IConverter), new DinkToPdf.SynchronizedConverter(new DinkToPdf.PdfTools()));
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