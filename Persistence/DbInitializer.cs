using System;
using Domain;

namespace Persistence;

public class DbInitializer
{
    public static async Task SeedData(AppDbContext context)
    {
        if (context.Activities.Any()) return;

        var activities = new List<Activity>
        {
            new Activity
            {
                Title = "Past Activity 1",
                Date = DateTime.Now.AddMonths(-2),
                Description = "Activity 2 months ago",
                Category = "drinks",
                City = "London",
                Venue = "Pub",
                Latitude = 51.509865,
                Longitude = -0.118092
            },
            new Activity
            {
                Title = "Past Activity 2",
                Date = DateTime.Now.AddMonths(-1),
                Description = "Activity 1 month ago",
                Category = "culture",
                City = "Paris",
                Venue = "Louvre",
                Latitude = 48.860611,
                Longitude = 2.337644
            },
            new Activity
            {
                Title = "Future Activity 1",
                Date = DateTime.Now.AddMonths(1),
                Description = "Activity 1 month in future",
                Category = "culture",
                City = "London",
                Venue = "Natural History Museum",
                Latitude = 51.496715,
                Longitude = -0.176367
            },
            new Activity
            {
                Title = "Future Activity 2",
                Date = DateTime.Now.AddMonths(2),
                Description = "Activity 2 months in future",
                Category = "music",
                City = "London",
                Venue = "O2 Arena",
                Latitude = 51.503038,
                Longitude = 0.003154
            },
            new Activity
            {
                Title = "Future Activity 3",
                Date = DateTime.Now.AddMonths(3),
                Description = "Activity 3 months in future",
                Category = "drinks",
                City = "London",
                Venue = "Another pub",
                Latitude = 51.509865,
                Longitude = -0.118092
            },
            new Activity
            {
                Title = "Future Activity 4",
                Date = DateTime.Now.AddMonths(4),
                Description = "Activity 4 months in future",
                Category = "drinks",
                City = "London",
                Venue = "Yet another pub",
                Latitude = 51.509865,
                Longitude = -0.118092
            },
            new Activity
            {
                Title = "Future Activity 5",
                Date = DateTime.Now.AddMonths(5),
                Description = "Activity 5 months in future",
                Category = "drinks",
                City = "London",
                Venue = "Just another pub",
                Latitude = 51.509865,
                Longitude = -0.118092
            },
            new Activity
            {
                Title = "Future Activity 6",
                Date = DateTime.Now.AddMonths(6),
                Description = "Activity 6 months in future",
                Category = "music",
                City = "London",
                Venue = "Roundhouse Camden",
                Latitude = 51.541290,
                Longitude = -0.143189
            },
            new Activity
            {
                Title = "Future Activity 7",
                Date = DateTime.Now.AddMonths(7),
                Description = "Activity 7 months in future",
                Category = "travel",
                City = "London",
                Venue = "Heathrow Airport",
                Latitude = 51.470020,
                Longitude = -0.454295
            },
            new Activity
            {
                Title = "Future Activity 8",
                Date = DateTime.Now.AddMonths(8),
                Description = "Activity 8 months in future",
                Category = "film",
                City = "London",
                Venue = "Cinema",
                Latitude = 51.509865,
                Longitude = -0.118092
            }
        };

        await context.Activities.AddRangeAsync(activities);
        await context.SaveChangesAsync();
    }
}
