// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore.TestModels.RainbowsModel;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class ParameterizedQuerySqlServerFixture : ParameterizedQueryFixtureBase
    {
        protected override ITestStoreFactory TestStoreFactory => SqlServerTestStoreFactory.Instance;

        protected override void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
        {
            base.OnModelCreating(modelBuilder, context);

            modelBuilder.Query<RainbowSightingWithDetails>().ToQuery<RainbowSightingWithDetailsParam>(param => context.Query<RainbowSightingWithDetails>().FromSql("SELECT * FROM RainbowSightings"));
            modelBuilder.Query<LocationWithRainbowSightingDetails>().ToQuery<LocationWithRainbowSightingDetailsParam>(param => context.Query<LocationWithRainbowSightingDetails>().FromSql("SELECT * FROM RainbowSightings"));
        }
    }
}
