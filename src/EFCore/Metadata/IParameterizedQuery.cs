// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq.Expressions;

namespace Microsoft.EntityFrameworkCore.Metadata
{
    /// <summary>
    ///     Represents a parameterized query used in a query type to generate a query in an <see cref="IModel" />.
    /// </summary>
    public interface IParameterizedQuery
    {
        /// <summary>
        ///     Gets the query type that this belongs to.
        /// </summary>
        IEntityType DeclaringEntityType { get; }

        /// <summary>
        ///     Gets the type of the parameter used to provide dynamic parameters to the query.
        /// </summary>
        Type ParameterType { get; }

        /// <summary>
        ///     Represents the actual query that will get executed to return data for the query type using the provided parameter.
        /// </summary>
        LambdaExpression Query { get; }
    }
}
