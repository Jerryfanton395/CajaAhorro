using CajaAhorro.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Inject dependencies
builder.InjectDependencies();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MigrateDatabase();
}

app.UseCors("AllowElectron");
app.UseAuthorization();
app.MapControllers();

app.Run();