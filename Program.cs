using BaiMoiiii.BUS;
using BaiMoiiii.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ========== CORS CHO FRONT-END ==========
// Cho phép gọi từ 127.0.0.1:5500 (Live Server của VSCode)
builder.Services.AddCors(options =>
{
    options.AddPolicy("_myCors", policy =>
    {
        policy.WithOrigins(
            "http://127.0.0.1:5500",
            "http://localhost:5500"
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});


// ========= Lấy connection string =========
var connStr = builder.Configuration.GetConnectionString("DefaultConnection");

// ========== ĐĂNG KÝ DAL ==========
builder.Services.AddTransient(_ => new BaoHanhDAL(connStr));
builder.Services.AddTransient(_ => new KhachHangDAL(connStr));
builder.Services.AddTransient(_ => new TaiSanDAL(connStr));
builder.Services.AddTransient(_ => new NhanVienDAL(connStr));
builder.Services.AddTransient(_ => new PhieuSuCoDAL(connStr));
builder.Services.AddTransient(_ => new PhieuCongViecDAL(connStr));
builder.Services.AddTransient(_ => new PhieuKhoDAL(connStr));
builder.Services.AddTransient(_ => new LichBaoTriDAL(connStr));
builder.Services.AddTransient(_ => new TaiKhoanDAL(connStr));
builder.Services.AddTransient<PhieuKho_ChiTietDAL>();

builder.Services.AddSingleton(new NhatKyHeThongDAL(connStr));
builder.Services.AddSingleton(new LinhKienDAL(connStr));
builder.Services.AddSingleton(new PCV_ChecklistDAL(connStr));

builder.Services.AddSingleton<LogHelper>();

// ========== ĐĂNG KÝ BUS ==========
builder.Services.AddScoped<BaoHanhBUS>();
builder.Services.AddScoped<KhachHangBUS>();
builder.Services.AddScoped<TaiSanBUS>();
builder.Services.AddScoped<NhanVienBUS>();
builder.Services.AddScoped<PhieuSuCoBUS>();
builder.Services.AddScoped<PhieuCongViecBUS>();
builder.Services.AddScoped<PhieuKhoBUS>();
builder.Services.AddScoped<LichBaoTriBUS>();
builder.Services.AddScoped<TaiKhoanBUS>();
builder.Services.AddScoped<PhieuKho_ChiTietBUS>();
builder.Services.AddScoped<NhatKyHeThongBUS>();
builder.Services.AddScoped<LinhKienBUS>();
builder.Services.AddScoped<PCV_ChecklistBUS>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("_myCors");
app.UseAuthorization();
app.MapControllers();

app.Run();
