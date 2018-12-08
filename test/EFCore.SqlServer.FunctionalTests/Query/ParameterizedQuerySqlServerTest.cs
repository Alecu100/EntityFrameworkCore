// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore.TestUtilities.Xunit;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class ParameterizedQuerySqlServerTest : ParameterizedQueryTestBase<ParameterizedQuerySqlServerFixture>
    {
        public ParameterizedQuerySqlServerTest(ParameterizedQuerySqlServerFixture fixture)
            : base(fixture)
        {
        }

        [ConditionalFact]
        public override void Query_should_fetch_queryables_by_parameter()
        {
            base.Query_should_fetch_queryables_by_parameter();
        }
    }
}
