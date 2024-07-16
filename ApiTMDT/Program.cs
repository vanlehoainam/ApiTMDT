using ApiTMDT.Repositories;
using ApiTMDT.Service;
using Data;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cấu hình CORS
builder.Services.AddCors(options => options.AddDefaultPolicy(
    policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
));

// Add services to the DI container
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
builder.Services.AddScoped<GioHangService>();
builder.Services.AddScoped<BinhLuanService>();
builder.Services.AddScoped<KhuyenMaiService>();

builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
