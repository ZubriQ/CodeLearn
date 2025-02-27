using CodeLearn.Api;
using CodeLearn.Application;
using CodeLearn.Infrastructure;
using CodeLearn.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddWebServices();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    await app.InitialiseDatabaseAsync();
    app.UseCors(options =>
    {
        options
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}