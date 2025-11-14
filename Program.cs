using BaiMoiiii.BUS;
using BaiMoiiii.DAL;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// =====================================================
// 🧩 DỊCH VỤ CƠ BẢN
// =====================================================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// =====================================================
// 🌐 CẤU HÌNH CORS CHO FRONT-END (HTML, JS)
// =====================================================
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins(
            "http://127.0.0.1:5501", // Live Server
            "http://localhost:5501"  // hoặc localhost
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

// =====================================================
// 🔗 KẾT NỐI CHUỖI CSDL
// =====================================================
var connStr = builder.Configuration.GetConnectionString("DefaultConnection");

// =====================================================
// 🧱 ĐĂNG KÝ DAL & BUS (ADO.NET)
// =====================================================

// ♦ BẢO HÀNH
builder.Services.AddSingleton(new BaoHanhDAL(connStr));
builder.Services.AddScoped<BaoHanhBUS>(_ => new BaoHanhBUS(connStr));

// ♦ KHÁCH HÀNG
builder.Services.AddSingleton(new KhachHangDAL(connStr));
builder.Services.AddScoped<KhachHangBUS>(_ => new KhachHangBUS(connStr));

// ♦ TÀI SẢN
builder.Services.AddSingleton(new TaiSanDAL(connStr));
builder.Services.AddScoped<TaiSanBUS>(_ => new TaiSanBUS(connStr));

// ♦ NHÂN VIÊN
builder.Services.AddSingleton(new NhanVienDAL(connStr));
builder.Services.AddScoped<NhanVienBUS>(_ => new NhanVienBUS(connStr));

// ♦ PHIẾU SỰ CỐ
builder.Services.AddSingleton(new PhieuSuCoDAL(connStr));
builder.Services.AddScoped<PhieuSuCoBUS>(_ => new PhieuSuCoBUS(connStr));

// ♦ PHIẾU CÔNG VIỆC
builder.Services.AddSingleton(new PhieuCongViecDAL(connStr));
builder.Services.AddScoped<PhieuCongViecBUS>(_ => new PhieuCongViecBUS(connStr));

// ♦ PHIẾU KHO ✅ (bổ sung mới)
builder.Services.AddSingleton(new PhieuKhoDAL(connStr));
builder.Services.AddScoped<PhieuKhoBUS>(_ => new PhieuKhoBUS(connStr));

//// ♦ CHI TIẾT PHIẾU KHO ✅
builder.Services.AddSingleton<PhieuKho_ChiTietDAL>();
builder.Services.AddScoped<PhieuKho_ChiTietBUS>();


// ♦ LINH KIỆN ✅
builder.Services.AddSingleton<LinhKienDAL>();
builder.Services.AddScoped<LinhKienBUS>();

// Tài Khoản
builder.Services.AddSingleton<TaiKhoanBUS>();
builder.Services.AddSingleton<TaiKhoanDAL>();

//Lịch Bảo trì 
builder.Services.AddSingleton<LichBaoTriBUS>();
builder.Services.AddSingleton<LichBaoTriDAL>();


// =====================================================
// 🚀 BUILD APP
// =====================================================
var app = builder.Build();

// =====================================================
// 🧩 MIDDLEWARE PIPELINE
// =====================================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();
app.MapControllers();
app.Run();
