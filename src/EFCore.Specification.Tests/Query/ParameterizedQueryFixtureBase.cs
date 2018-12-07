// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.TestModels.RainbowsModel;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Query
{
    public abstract class ParameterizedQueryFixtureBase : SharedStoreFixtureBase<RainbowsContext>, IQueryFixtureBase
    {
        protected ParameterizedQueryFixtureBase()
        {
            var entitySorters = new Dictionary<Type, Func<dynamic, object>>
            {
                { typeof(Person), e => e?.FirstName + e?.LastName },
                { typeof(Location), e => e?.Location },
                { typeof(RainbowType), e => e?.Name },
                { typeof(RainbowSighting), e => e?.SeenTime }
            };

            var entityAsserters = new Dictionary<Type, Action<dynamic, dynamic>>
            {
                {
                    typeof(Person),
                    (e, a) =>
                    {
                        Assert.Equal(e == null, a == null);

                        if (a != null)
                        {
                            Assert.Equal(e.FirstName, a.FirstName);
                            Assert.Equal(e.LastName, a.LastName);
                        }
                    }
                },
                {
                    typeof(RainbowType),
                    (e, a) =>
                    {
                        Assert.Equal(e == null, a == null);

                        if (a != null)
                        {
                            Assert.Equal(e.Id, a.Id);
                            Assert.Equal(e.Name, a.Name);
                            Assert.Equal(e.NumberOfRainbows, a.NumberOfRainbows);
                        }
                    }
                },
                {
                    typeof(Location),
                    (e, a) =>
                    {
                        Assert.Equal(e == null, a == null);

                        if (a != null)
                        {
                            Assert.Equal(e.Id, a.Id);
                            Assert.Equal(e.Name, a.Name);
                            Assert.Equal(e.Longitude, a.Longitude);
                            Assert.Equal(e.Latitude, a.Latitude);
                        }
                    }
                },
                {
                    typeof(RainbowSighting),
                    (e, a) =>
                    {
                        Assert.Equal(e == null, a == null);

                        if (a != null)
                        {
                            Assert.Equal(e.Id, a.Id);
                            Assert.Equal(e.RainbowTypeId, a.RainbowTypeId);
                            Assert.Equal(e.PersonId, a.PersonId);
                            Assert.Equal(e.LocationId, a.LocationId);
                            Assert.Equal(e.SeenTime, a.SeenTime);
                            Assert.Equal(e.Duration, a.Duration);
                        }
                    }
                }
            };

            QueryAsserter = new QueryAsserter<RainbowsContext>(
                CreateContext,
                new RainbowsData(),
                entitySorters,
                entityAsserters);
        }

        protected override string StoreName { get; } = "ParameterizedQueryTest";

        public QueryAsserterBase QueryAsserter { get; set; }

        protected override void Seed(RainbowsContext context) => RainbowsContext.Seed(context);

        protected override void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
        {
            modelBuilder.Entity<Person>().HasKey(p => p.Id);
            modelBuilder.Entity<Location>().HasKey(l => l.Id);
            modelBuilder.Entity<RainbowType>().HasKey(rbt => rbt.Id);
            modelBuilder.Entity<RainbowSighting>().HasKey(rbs => rbs.Id);

            modelBuilder.Entity<Person>().Property(p => p.Id).ValueGeneratedNever();
            modelBuilder.Entity<Location>().Property(l => l.Id).ValueGeneratedNever();
            modelBuilder.Entity<RainbowType>().Property(rbt => rbt.Id).ValueGeneratedNever();
            modelBuilder.Entity<RainbowSighting>().Property(rbs => rbs.Id).ValueGeneratedNever();

            modelBuilder.Entity<RainbowSighting>().HasOne(rbs => rbs.RainbowType).WithMany(rbt => rbt.RainbowSightings).HasForeignKey(rbs => rbs.RainbowTypeId);
            modelBuilder.Entity<RainbowSighting>().HasOne(rbs => rbs.Person).WithMany(p => p.RainbowSightings).HasForeignKey(rbs => rbs.PersonId);
            modelBuilder.Entity<RainbowSighting>().HasOne(rbs => rbs.Location).WithMany(l => l.RainbowSightings).HasForeignKey(rbs => rbs.LocationId);
        }
    }
}
