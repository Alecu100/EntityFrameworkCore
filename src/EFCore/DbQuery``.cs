﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    ///     <para>
    ///         A <see cref="DbQuery{TQuery, TParam}" /> can be used to query instances of <typeparamref name="TQuery" />.
    ///         LINQ queries against a <see cref="DbQuery{TQuery, TParam}" /> will be translated into queries against the database.
    ///     </para>
    ///     <para>
    ///         The results of a LINQ query against a <see cref="DbQuery{TQuery, TParam}" /> will contain the results
    ///         returned from the database which can be filtered based on the parameters passed through an instance of
    ///         <typeparamref name="TParam" />.
    ///         It may not reflect changes made in the context that have not
    ///         been persisted to the database. For example, the results will not contain newly added views
    ///         and may still contain views that are marked for deletion.
    ///     </para>
    ///     <para>
    ///         Depending on the database being used, some parts of a LINQ query against a <see cref="DbQuery{TQuery, TParam}" />
    ///         may be evaluated in memory rather than being translated into a database query.
    ///     </para>
    ///     <para>
    ///         <see cref="DbQuery{TQuery, TParam}" /> objects are usually obtained from a <see cref="DbQuery{TQuery, TParam}" />
    ///         property on a derived <see cref="DbContext" /> or from the <see cref="DbQuery{TQuery, TParam}" />
    ///         method.
    ///     </para>
    /// </summary>
    /// <typeparam name="TQuery"> The type of view being operated on by this view. </typeparam>
    /// <typeparam name="TParam">
    ///     The type which contains the filter parameters which will be included in the query to the database. Each public field or property
    ///     in an instance of this type will get a corresponding parameter in the resulting query. Using this instance you filter the results after
    ///     you have instantiated the DbContext
    /// </typeparam>
    public abstract class DbQuery<TQuery, TParam> : IInfrastructure<IServiceProvider>
        where TQuery : class
    {
        /// <summary>
        ///     Creates a query that will fetch the queriable types from the database filtering them using the specified parameter. This parameter can
        ///     contain
        ///     multiple fields and properties, each of the used to filter the results dynamically after the context was initialized.
        /// </summary>
        /// <param name="parameter">
        ///     A parameter passed to the query when fetching it that contains additional information for filtering the data provided by the query.
        /// </param>
        /// <returns>The resulting query.</returns>
        public virtual IQueryable<TQuery> GetQuery(TParam parameter) => throw new NotImplementedException();

        /// <summary>
        ///     Gets the type of the queryable entity associated with the parameterized query
        /// </summary>
        public virtual Type ElementType => throw new NotImplementedException();

        /// <summary>
        ///     Gets the type of the parameter used to apply dynamic filters when fetching the queryable type
        /// </summary>
        public virtual Type ParameterType => throw new NotImplementedException();

        /// <summary>
        ///     Gets the provider of the parameterized queries
        /// </summary>
        public virtual IQueryProvider Provider => throw new NotImplementedException();

        /// <summary>
        ///     <para>
        ///         Gets the scoped <see cref="IServiceProvider" /> being used to resolve services.
        ///     </para>
        ///     <para>
        ///         This property is intended for use by extension methods that need to make use of services
        ///         not directly exposed in the public API surface.
        ///     </para>
        /// </summary>
        IServiceProvider IInfrastructure<IServiceProvider>.Instance => throw new NotImplementedException();
    }
}
