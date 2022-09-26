using Backend;
using Backend.AspPlugins;
using Backend.Authentication;
using Backend.ParameterHelpers;

using Backend.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
var config = builder.Configuration;
var services = builder.Services;

config.AddStandardSources(env.EnvironmentName);

var opt = config.CreateAtriaOptions<BackendSettings>();

services.AddControllers(options => {
        options.Filters.Add<DatabaseBinderNullFilter>();
        options.Filters.Add<AuthenticationBinderFilter>();
        options.UseCentralRoutePrefix(new RouteAttribute(opt.ApiPrefix));
        options.ModelMetadataDetailsProviders.Add(new IncludeAttributeProvider());
    })
    .AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = env.IsDevelopment());

services
    .AddSelectiveValidator()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddSingleton<SessionService>()
    .AddSingleton<FuzzingService>()
    .AddDbContext<AtriaContext>()
    .AddHostedService<ClearerService>()
    .AddHostedService<ApiCheckerService>()
    .AddSingleton(opt);

var app = builder.Build();
var options = app.Services.GetRequiredService<BackendSettings>();

if (options.ShouldUseSwagger) {
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseCors(policy =>
    policy.WithOrigins(opt.AllowedOrigin)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());

app.UseHttpsRedirection();

app.MapControllers();
app.Run();
