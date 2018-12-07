// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore.TestModels.RainbowsModel
{
    public class RainbowsContext : PoolableDbContext
    {
        public static readonly string StoreName = "GearsOfWar";

        public RainbowsContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<RainbowType> RainbowTypes { get; set; }
        public DbSet<RainbowSighting> RainbowSightings { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbQuery<RainbowSightingWithDetails, RainbowSightingWithDetailsParam> RainbowSightingWithDetails { get; set; }
        public DbQuery<LocationWithRainbowSightingDetails, LocationWithRainbowSightingDetailsParam> LocationWithRainbowSightingDetails { get; set; }

        public static void Seed(RainbowsContext context)
        {
            var persons = RainbowsData.CreatePersons();
            var locations = RainbowsData.CreateLocations();
            var rainbowTypes = RainbowsData.CreateRainbowTypes();
            var rainbowSightings = RainbowsData.CreateRainbowSightings();

            RainbowsData.WireUp(persons, rainbowTypes, locations, rainbowSightings);

            context.RainbowSightings.AddRange(rainbowSightings);
            context.Locations.AddRange(locations);
            context.Persons.AddRange(persons);
            context.RainbowTypes.AddRange(rainbowTypes);

            context.SaveChanges();
        }
    }
}
