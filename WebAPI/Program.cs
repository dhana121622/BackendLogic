using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllWithCredentials", builder =>
    {
        builder.SetIsOriginAllowed(origin => true)  // Allows all origins
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});


builder.Services.AddDbContext<UserLoginDbContext>(options =>
   options.UseNpgsql(builder.Configuration.GetConnectionString("APIConnection"))
);

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAllWithCredentials");

app.UseAuthorization();

app.MapControllers();

app.MapHub<Chat>("/hubs/Chat");

app.Run();
