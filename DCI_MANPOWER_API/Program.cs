
using DCI_MANPOWER_API.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DBSCM>();
builder.Services.AddCors(options => options.AddPolicy("Cors", builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
}));
// Configure the HTTP request pipeline.
var app = builder.Build();
app.UseCors("Cors");
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.MapControllers();
app.Run();
