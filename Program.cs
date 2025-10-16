using InsuranceWebApp.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationLocalization();
builder.Services.AddApplicationIdentity();
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();
app.ConfigureMiddlewarePipeline();

app.Run();
