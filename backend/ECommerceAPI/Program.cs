using ECommerceAPI.Data;
using ECommerceAPI.Services;
using ECommerceAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 添加 SQLite 数据库连接
builder.Services.AddDbContext<ECommerceDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// 添加服务依赖注入
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IReviewService, ReviewService>();

// 添加控制器支持
builder.Services.AddControllers();

// 添加 Swagger 文档生成
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 开启 Swagger UI（开发环境用）
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

