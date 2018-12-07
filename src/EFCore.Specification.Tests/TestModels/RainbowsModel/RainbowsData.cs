// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.TestModels.GearsOfWarModel;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore.TestModels.RainbowsModel
{
    public class RainbowsData : IExpectedData
    {
        public IReadOnlyList<Person> Persons { get; }
        public IReadOnlyList<RainbowType> RainbowTypes { get; }
        public IReadOnlyList<Location> Locations { get; }
        public IReadOnlyList<RainbowSighting> RainbowSightings { get; }

        public RainbowsData()
        {
            Persons = CreatePersons();
            RainbowTypes = CreateRainbowTypes();
            Locations = CreateLocations();
            RainbowSightings = CreateRainbowSightings();

            WireUp(Persons, RainbowTypes, Locations, RainbowSightings);
        }

        public static void WireUp(IReadOnlyList<Person> persons, IReadOnlyList<RainbowType> rainbowTypes, IReadOnlyList<Location> locations, IReadOnlyList<RainbowSighting> rainbowSightings)
        {
            rainbowSightings[0].Person = persons[3];
            rainbowSightings[0].Location = locations[1];
            rainbowSightings[0].RainbowType = rainbowTypes[0];

            rainbowSightings[1].Person = persons[2];
            rainbowSightings[1].Location = locations[0];
            rainbowSightings[1].RainbowType = rainbowTypes[2];

            rainbowSightings[2].Person = persons[0];
            rainbowSightings[2].Location = locations[2];
            rainbowSightings[2].RainbowType = rainbowTypes[3];

            rainbowSightings[3].Person = persons[1];
            rainbowSightings[3].Location = locations[2];
            rainbowSightings[3].RainbowType = rainbowTypes[3];

            rainbowSightings[4].Person = persons[0];
            rainbowSightings[4].Location = locations[1];
            rainbowSightings[4].RainbowType = rainbowTypes[2];

            rainbowSightings[5].Person = persons[2];
            rainbowSightings[5].Location = locations[1];
            rainbowSightings[5].RainbowType = rainbowTypes[0];

            rainbowSightings[6].Person = persons[0];
            rainbowSightings[6].Location = locations[2];
            rainbowSightings[6].RainbowType = rainbowTypes[1];

            rainbowSightings[6].Person = persons[2];
            rainbowSightings[6].Location = locations[1];
            rainbowSightings[6].RainbowType = rainbowTypes[1];
        }

        public IQueryable<TEntity> Set<TEntity>()
            where TEntity : class
        {
            if (typeof(TEntity) == typeof(Location))
            {
                return (IQueryable<TEntity>)Locations.AsQueryable();
            }

            if (typeof(TEntity) == typeof(Person))
            {
                return (IQueryable<TEntity>)Persons.AsQueryable();
            }

            if (typeof(TEntity) == typeof(RainbowType))
            {
                return (IQueryable<TEntity>)RainbowTypes.AsQueryable();
            }

            if (typeof(TEntity) == typeof(RainbowSighting))
            {
                return (IQueryable<TEntity>)RainbowSightings.AsQueryable();
            }
            
            throw new InvalidOperationException("Invalid entity type: " + typeof(TEntity));
        }

        public static IReadOnlyList<Person> CreatePersons()
            => new List<Person>
            {
                new Person
                {
                    Id = 1,
                    FirstName = "Jack",
                    LastName = "Smith"
                },
                new Person
                {
                    Id = 2,
                    FirstName = "Grizzly",
                    LastName = "Bear"
                },
                new Person
                {
                    Id = 3,
                    FirstName = "Panda",
                    LastName = "Bear"
                },
                new Person
                {
                    Id = 4,
                    FirstName = "Agent",
                    LastName = "007"
                }
            };

        public static IReadOnlyList<RainbowType> CreateRainbowTypes()
            => new List<RainbowType>
            {
                new RainbowType
                {
                    Id = 1,
                    Name = "Single Rainbow",
                    NumberOfRainbows = 1
                },
                new RainbowType
                {
                    Id = 2,
                    Name = "Double Rainbow",
                    NumberOfRainbows = 2
                },
                new RainbowType
                {
                    Id = 3,
                    Name = "Triple Rainbow",
                    NumberOfRainbows = 3
                },
                new RainbowType
                {
                    Id = 4,
                    Name = "Deca Rainbow",
                    NumberOfRainbows = 10
                }
            };

        public static IReadOnlyList<Location> CreateLocations() =>
            new List<Location>
            {
                new Location
                {
                    Id = 1,
                    Latitude = 55.255,
                    Longitude = 45.332,
                    Name = "Narnia"
                },
                new Location
                {
                    Id = 2,
                    Latitude = 53.511,
                    Longitude = 30.444,
                    Name = "Azeroth"
                },
                new Location
                {
                    Id = 3,
                    Latitude = 65.255,
                    Longitude = 70.113,
                    Name = "Tyria"
                }
            };

        public static IReadOnlyList<RainbowSighting> CreateRainbowSightings() =>
            new List<RainbowSighting>
            {
                new RainbowSighting
                {
                    Id = 1,
                    Duration = new TimeSpan(4, 2, 22),
                    SeenTime = new DateTime(2050, 11, 22)
                },
                new RainbowSighting
                {
                    Id = 2,
                    Duration = new TimeSpan(1, 3, 11),
                    SeenTime = new DateTime(2050, 11, 20)
                },
                new RainbowSighting
                {
                    Id = 3,
                    Duration = new TimeSpan(5, 11, 22),
                    SeenTime = new DateTime(2050, 9, 12)
                },
                new RainbowSighting
                {
                    Id = 4,
                    Duration = new TimeSpan(2, 23, 45),
                    SeenTime = new DateTime(2051, 3, 5)
                },
                new RainbowSighting
                {
                    Id = 5,
                    Duration = new TimeSpan(3, 44, 11),
                    SeenTime = new DateTime(2050, 6, 24)
                },
                new RainbowSighting
                {
                    Id = 6,
                    Duration = new TimeSpan(5, 11, 22),
                    SeenTime = new DateTime(2050, 9, 12)
                },
                new RainbowSighting
                {
                    Id = 7,
                    Duration = new TimeSpan(8, 32, 56),
                    SeenTime = new DateTime(2050, 3, 5)
                },
                new RainbowSighting
                {
                    Id = 8,
                    Duration = new TimeSpan(5, 22, 44),
                    SeenTime = new DateTime(2050, 2, 8)
                }
            };
    }
}
