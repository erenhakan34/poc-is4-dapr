using Core.Configuration;
using Core.Data;
using Document.Application.Services;
using Document.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DocumentDataContext>(options =>
{
    options.UseInMemoryDatabase("DocumentDb");
});

builder.Services.AddScoped<DataContext>((serviceProvider) => serviceProvider.GetRequiredService<DocumentDataContext>());
builder.Services.AddCoreServices();
builder.Services.AddTransient(typeof(IDocumentRepository), typeof(DocumentRepository));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddDapr();
var app = builder.Build();
/*
using (IServiceScope scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
   scope.ServiceProvider.GetService<DocumentDataContext>()?.Database.Migrate();
}*/

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
