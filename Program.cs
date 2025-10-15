using BaiMoiiii.DAL;
using BaiMoiiii.BUS;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ==================== 🔹 ĐĂNG KÝ LỚP DAL + BUS ====================
builder.Services.AddSingleton(new BaoHanhDAL(
    builder.Configuration.GetConnectionString("DefaultConnection")!
));
builder.Services.AddScoped<BaoHanhBUS>();

// ================================================================

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
