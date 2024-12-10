var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// 启用路由
app.UseRouting();

// 创建端点
app.UseEndpoints(endpoints =>
{
    // 添加端点
});

app.Run();
