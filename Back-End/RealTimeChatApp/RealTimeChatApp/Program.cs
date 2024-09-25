using RealTimeChatApp.DataServices;
using RealTimeChatApp.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();
builder.Services.AddSingleton<SharedDb>();

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
