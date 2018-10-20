// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq.Expressions;

namespace Microsoft.EntityFrameworkCore.Metadata
{
    /// <summary>
    ///     <para>
    ///         Represents a parameterized query for which a parameter object can be provided to the query to filter the data
    ///         that is fetched dynamically
    ///     </para>
    ///     <para>
    ///         This interface is used during model creation and allows the metadata to be modified.
    ///         Once the model is built, <see cref="IForeignKey" /> represents a ready-only view of the same metadata.
    ///     </para>
    /// </summary>
    public interface IMutableParameterizedQuery : IParameterizedQuery
    {
        /// <summary>
        ///     Gets the query type that this belongs to.
        /// </summary>
        new IMutableEntityType DeclaringEntityType { get; }

        /// <summary>
        ///     Gets the type of the parameter used to provide dynamic parameters to the query.
        /// </summary>
        new Type ParameterType { get; set; }

        /// <summary>
        ///     Represents the actual query that will get executed to return data for the query type using the provided parameter
        /// </summary>
        new LambdaExpression Query { get; set; }
    }
}
