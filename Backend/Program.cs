using Backend;
using Backend.Authentication;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://localhost:44368/login",
                "https://localhost:44368"
                );
        });
});

// Add services to the container.
builder.Services.AddControllers(options => options.UseCentralRoutePrefix(new RouteAttribute("api")))
    .AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = builder.Environment.IsDevelopment());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<SessionService>();
builder.Services.AddDbContext<AtriaContext>();
builder.Services.AddHostedService<SessionClearerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
