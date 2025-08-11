using Microsoft.AspNetCore.Mvc;
using N_LayerBestPratice.Repository.Extensions;
using N_LayerBestPratice.Services.Extensions;
using N_LayerBestPratice.Services.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<FluentValidationFilters>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<ApiBehaviorOptions>(opt =>
{
    // Disable automatic model state validation
    opt.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddRepositories(builder.Configuration);

builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c => { c.SerializeAsV2 = true; });
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();