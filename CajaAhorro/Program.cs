var builder = WebApplication.CreateBuilder(args);

// Habilita peticiones desde Electron (CORS)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowElectron",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
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

app.UseCors("AllowElectron");
app.UseAuthorization();
app.MapControllers();

app.Run();