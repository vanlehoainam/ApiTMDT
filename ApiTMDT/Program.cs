using ApiTMDT.Service;
using Data;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cấu hình CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy => policy.WithOrigins("http://localhost:5500", "http://127.0.0.1:5500") // Thay đổi URL này thành nguồn gốc của trang web của bạn
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

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

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");
app.UseStaticFiles(); // Phục vụ các tệp tĩnh từ wwwroot

// Thêm middleware để phục vụ các tệp từ thư mục data/images
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "data/images")),
    RequestPath = "/data/images"
});

app.UseAuthorization();
app.MapControllers();

app.Run();
