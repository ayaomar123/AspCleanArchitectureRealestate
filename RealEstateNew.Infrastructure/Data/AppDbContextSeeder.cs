using RealEstateNew.Domain.Entities;

namespace RealEstateNew.Infrastructure.Data
{
    public static class AppDbContextSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { Name = "Commercial", Status = true },
                    new Category { Name = "Residential", Status = true },
                    new Category { Name = "Block", Status = false }
                );
            }


            if (!context.PropertyTypes.Any())
            {
                context.PropertyTypes.AddRange(
                    new PropertyType { Name = "Sale", Status = true },
                    new PropertyType { Name = "Rent", Status = true }
                );
            }

            if (!context.Cities.Any())
            {
                context.Cities.AddRange(
                    new City { Name = "Gaza", Status = true },
                    new City { Name = "Ramallah", Status = true }
                );
            }

            if (!context.Districts.Any())
            {
                context.Districts.AddRange(
                    new District { Name = "Al-shate", Status = true,CityId=1 },
                    new District { Name = "Sengel", Status = true ,CityId=2}
                );
            }


            if (!context.Items.Any())
            {
                var category = context.Categories.FirstOrDefault();
                var city = context.Cities.FirstOrDefault();
                var district = context.Districts.FirstOrDefault();
                var propertyType = context.PropertyTypes.FirstOrDefault();

                context.Items.AddRange(
                    new Item
                    {
                        Name = "Luxury Villa in Beverly Hills",
                        Image = "villa.jpg",
                        BackgroundImage = "villa-bg.jpg",
                        Status = true,
                        AdvertiseNo = 1001,
                        CategoryId = category.Id,
                        CityId = city.Id,
                        DistrictId = district.Id,
                        PropertyTypeId = propertyType.Id,
                        Latitude = 34.0900,
                        Longitude = -118.4068,
                        Soum = 500000,
                        Limit = 700000,
                        StreetWidth = 20,
                        Space = 1500,
                        PricePerMeter = 300,
                        Description = "A luxurious villa with stunning views and modern amenities.",
                        HashedPassword = "hashed_password_sample",
                        CreatedAt = DateTime.UtcNow
                    },
            new Item
            {
                Name = "Commercial Land in Downtown",
                Image = "land.jpg",
                BackgroundImage = "land-bg.jpg",
                Status = true,
                AdvertiseNo = 1002,
                CategoryId = category.Id,
                CityId = city.Id,
                DistrictId = district.Id,
                PropertyTypeId = propertyType.Id,
                Latitude = 34.0522,
                Longitude = -118.2437,
                Soum = 300000,
                Limit = 450000,
                StreetWidth = 25,
                Space = 2000,
                PricePerMeter = 220,
                Description = "Prime location commercial land suitable for investment.",
                HashedPassword = "hashed_password_sample",
                CreatedAt = DateTime.UtcNow
            }

                );
            }

            context.SaveChanges();
        }
    }
}
