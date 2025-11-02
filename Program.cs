using BaiMoiiii.BUS;
using BaiMoiiii.DAL;

var builder = WebApplication.CreateBuilder(args);

// ===== DỊCH VỤ CƠ BẢN =====
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ===== CẤU HÌNH CORS =====
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500", "http://localhost:5500")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// ===== KẾT NỐI CHUỖI CSDL =====
var connStr = builder.Configuration.GetConnectionString("DefaultConnection");

// =====================================================
// 🧩 ĐĂNG KÝ DAL & BUS (ADO.NET)
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

// ⚙️ Bạn có thể thêm module khác tương tự:
// builder.Services.AddSingleton(new NhanVienDAL(connStr));
// builder.Services.AddScoped<NhanVienBUS>(_ => new NhanVienBUS(connStr));

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
app.UseCors(MyAllowSpecificOrigins); // ⚠️ BẮT BUỘC để FE (HTML/JS) truy cập được
app.UseAuthorization();

app.MapControllers();
app.Run();
