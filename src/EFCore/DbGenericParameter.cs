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
        /// <summary>
        /// The DbType that will be set on the resulting DbParameter
        /// </summary>
        public DbType DbType { get; set; }

        /// <summary>
        /// Set if parameter can be null
        /// </summary>
        public bool? IsNullable { get; set; }

        /// <summary>
        /// The name that will be set on the resulting DbParameter
        /// </summary>
        public string ParameterName { get; set; }

        /// <summary>
        /// The value that will be set on the resulting DbParameter
        /// </summary>
        public object Value { get; set; }
    }
}
