var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        p => p.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod());
});
builder.Services.AddControllers();
var app = builder.Build();
app.UseHttpsRedirection();
app.UseCors("AllowAngular");
app.MapControllers();

app.Run();
