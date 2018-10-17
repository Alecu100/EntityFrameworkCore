// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Concurrent;
using System.Reflection;
using JetBrains.Annotations;

namespace Microsoft.EntityFrameworkCore.Internal
{
    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class DbSetSource : IDbSetSource, IDbQuerySource
    {
        private static readonly MethodInfo _genericCreateSet
            = typeof(DbSetSource).GetTypeInfo().GetDeclaredMethod(nameof(CreateSetFactory));

        private static readonly MethodInfo _genericCreateQuery
            = typeof(DbSetSource).GetTypeInfo().GetDeclaredMethod(nameof(CreateQueryFactory));

        private static readonly MethodInfo _genericCreateParameterizedQuery
            = typeof(DbSetSource).GetTypeInfo().GetDeclaredMethod(nameof(CreateParameterizedQueryFactory));

        private readonly ConcurrentDictionary<Type, Func<DbContext, object>> _cache
            = new ConcurrentDictionary<Type, Func<DbContext, object>>();

        private readonly ConcurrentDictionary<Tuple<Type,Type>, Func<DbContext, object>> _cache2
            = new ConcurrentDictionary<Tuple<Type, Type>, Func<DbContext, object>>();

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public virtual object Create(DbContext context, Type type)
            => CreateCore(context, type, _genericCreateSet);

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public virtual object CreateQuery(DbContext context, Type type)
            => CreateCore(context, type, _genericCreateParameterizedQuery);

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public virtual object CreateParameterizedQuery(DbContext context, Type type, Type type2)
            => CreateCore(context, type, type2, _genericCreateQuery);

        private object CreateCore(DbContext context, Type type, MethodInfo createMethod)
            => _cache.GetOrAdd(
                type,
                t => (Func<DbContext, object>)createMethod
                    .MakeGenericMethod(t)
                    .Invoke(null, null))(context);


        private object CreateCore(DbContext context, Type type,Type type2, MethodInfo createMethod)
            => _cache2.GetOrAdd(
                 new Tuple<Type, Type>(type, type2),
                t => (Func<DbContext, object>)createMethod
                    .MakeGenericMethod(t.Item1, t.Item2)
                    .Invoke(null, null))(context);

        [UsedImplicitly]
        private static Func<DbContext, object> CreateSetFactory<TEntity>()
            where TEntity : class
            => c => new InternalDbSet<TEntity>(c);

        [UsedImplicitly]
        private static Func<DbContext, DbQuery<TQuery>> CreateQueryFactory<TQuery>()
            where TQuery : class
            => c => new InternalDbQuery<TQuery>(c);


        [UsedImplicitly]
        private static Func<DbContext, DbQuery<TQuery, TParam>> CreateParameterizedQueryFactory<TQuery, TParam>()
            where TQuery : class
            => c => new InternalDbQuery<TQuery, TParam>(c);
    }
}
