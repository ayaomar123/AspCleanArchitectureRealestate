using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RealEstateNew.Application.Interfaces.Category;
using RealEstateNew.Application.Interfaces.City;
using RealEstateNew.Application.Interfaces.District;
using RealEstateNew.Application.Interfaces.Item;
using RealEstateNew.Application.Interfaces.Item.Validation;
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


//Category
builder.Services.AddScoped<ICategoryRepository>(provider =>
{
    var context = provider.GetRequiredService<AppDbContext>();
    var mapper = provider.GetRequiredService<IMapper>();
    var env = provider.GetRequiredService<IWebHostEnvironment>();
    return new CategoryRepository(context, mapper, env.WebRootPath);
});
builder.Services.AddScoped<ICategoryService, CategoryService>();

//City
builder.Services.AddScoped<ICityRepository>(provider =>
{
    var context = provider.GetRequiredService<AppDbContext>();
    var mapper = provider.GetRequiredService<IMapper>();
    var env = provider.GetRequiredService<IWebHostEnvironment>();
    return new CityRepository(context, mapper, env.WebRootPath);
});
builder.Services.AddScoped<ICityService, CityService>();


//District
builder.Services.AddScoped<IDistrictRepository>(provider =>
{
    var context = provider.GetRequiredService<AppDbContext>();
    var mapper = provider.GetRequiredService<IMapper>();
    var env = provider.GetRequiredService<IWebHostEnvironment>();
    return new DistrictRepository(context, mapper, env.WebRootPath);
});
builder.Services.AddScoped<IDistrictService, DistrictService>();

//Item
builder.Services.AddScoped<IItemRepository>(provider =>
{
    var context = provider.GetRequiredService<AppDbContext>();
    var mapper = provider.GetRequiredService<IMapper>();
    var env = provider.GetRequiredService<IWebHostEnvironment>();
    return new ItemRepository(context, mapper, env.WebRootPath);
});
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IItemValidationService, ItemValidationService>();



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
