using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RealEstateNew.Application.Interfaces;
using RealEstateNew.Application.Services;
using RealEstateNew.Infrastructure.Data;
using RealEstateNew.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("RealEstateNew.Infrastructure")
    ));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ICategoryRepository>(provider =>
{
    var context = provider.GetRequiredService<AppDbContext>();
    var mapper = provider.GetRequiredService<IMapper>();
    var env = provider.GetRequiredService<IWebHostEnvironment>();
    return new CategoryRepository(context, mapper, env.WebRootPath);
});

builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddControllers();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed Database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<AppDbContext>();
    AppDbContextSeeder.Seed(dbContext);
}

// Middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();
app.Run();
