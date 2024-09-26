using RealTimeChatApp.DataServices;
using RealTimeChatApp.Hubs;
using RealTimeChatApp.Models;
using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();
builder.Services.AddSingleton<ConcurrentDictionary<string, UserConnection>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(opt =>
{
    opt.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod();


});
app.MapControllers();
app.MapHub<ChatHub>("/chat");


app.Run();
