using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddResponseCaching( x => x.MaximumBodySize = 2048);
// Configure response caching globally
builder.Services.AddControllers();
//builder.Services.AddMvcCore(options =>
//{
//    options.CacheProfiles.Add("GetListById", new CacheProfile()
//    {
//        Duration = 10,
//        Location = ResponseCacheLocation.Any
//    });
//});

builder.Services.AddMemoryCache();
builder.Services.AddLazyCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
//app.UseResponseCaching();
//app.Use(async (context, next) =>
//{
//    context.Response.GetTypedHeaders().CacheControl =
//    new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
//    {
//        Public = true,
//        MaxAge = TimeSpan.FromSeconds(20)
//    };

//    await next();
//});

app.Run();
