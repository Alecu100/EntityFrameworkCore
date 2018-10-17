// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Data;

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    ///     A generic database neutral parameter that gets directly translated into a corresponding DbParameter
    ///     based on the underlying database type. For example if SQL Server is used as a database, the it will get translated to
    ///     a SqlParameter.
    /// </summary>
    public class DbGenericParameter
    {
        public DbType DbType { get; set; }

        public bool? IsNullable { get; set; }

        public string ParameterName { get; set; }

        public object Value { get; set; }
    }
}
