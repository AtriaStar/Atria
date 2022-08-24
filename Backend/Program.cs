using Backend;
using Backend.Authentication;

using Backend.ParameterHelpers;

using Backend.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => {
        options.Filters.Add<DatabaseBinderNullFilter>();
        options.UseCentralRoutePrefix(new RouteAttribute("api"));
        options.ModelMetadataDetailsProviders.Add(new IncludeAttributeProvider());
    })
    // Pretty printed JSON in debug env.
    .AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = builder.Environment.IsDevelopment());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddSingleton<SessionService>()
    .AddDbContext<AtriaContext>()
    .AddHostedService<SessionClearerService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseCors(policy => 
    policy.WithOrigins("https://localhost:7206")
        .AllowAnyHeader()
        .AllowCredentials());

app.UseHttpsRedirection();

app.MapControllers();
app.Run();
