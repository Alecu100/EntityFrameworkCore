// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.EntityFrameworkCore.TestModels.Northwind;
using Microsoft.EntityFrameworkCore.TestModels.RainbowsModel;
using Microsoft.EntityFrameworkCore.TestUtilities.Xunit;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Query
{
    public abstract class ParameterizedQueryTestBase<TFixture> : QueryTestBase<TFixture>
        where TFixture : ParameterizedQueryFixtureBase, new()
    {
        protected ParameterizedQueryTestBase(TFixture fixture)
            : base(fixture)
        {
        }

        protected RainbowsContext CreateContext()
        {
            return Fixture.CreateContext();
        }

        [ConditionalFact]
        public virtual void Query_should_fetch_queryables_by_parameter()
        {
            using (var context = CreateContext())
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
        }
    }
}
