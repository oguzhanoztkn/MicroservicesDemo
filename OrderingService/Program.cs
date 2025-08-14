using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OrderContext>(opts =>
    opts.UseSqlite(builder.Configuration.GetConnectionString("OrdersDb") ?? "Data Source=orders.db"));

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        var host = builder.Configuration["EventBus:Host"] ?? "localhost";
        var user = builder.Configuration["EventBus:User"] ?? "guest";
        var pass = builder.Configuration["EventBus:Pass"] ?? "guest";

        cfg.Host(host, "/", h =>
        {
            h.Username(user);
            h.Password(pass);
        });
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();
app.MapControllers();
app.Run();
