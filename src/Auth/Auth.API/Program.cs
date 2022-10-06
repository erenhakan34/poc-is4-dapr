

using Auth.API;
using Auth.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddCustomConfiguration();
//uilder.AddCustomSerilog();
builder.AddCustomSwagger();
builder.AddCustomAuthentication();
builder.AddCustomAuthorization(); 
builder.AddCustomHealthChecks();
builder.AddCustomApplicationServices();
builder.Services.AddHttpClient();
builder.Services.AddControllers().AddDapr();
builder.Services.AddDaprClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/*
var pathBase = builder.Configuration["PATH_BASE"];
if (!string.IsNullOrEmpty(pathBase))
{
    app.UsePathBase(pathBase);
}*/

app.UseRouting();
app.UseCloudEvents();

app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
  
    endpoints.MapControllers();
    endpoints.MapSubscribeHandler();
});

app.Run();