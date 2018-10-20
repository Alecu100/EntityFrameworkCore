// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.EntityFrameworkCore.Internal
{
    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class InternalDbQuery<TQuery, TParam> : DbQuery<TQuery, TParam>, IInfrastructure<IServiceProvider>
        where TQuery : class
    {
        private readonly DbContext _context;
        private IEntityType _entityType;
        private Type _entityClrType;
        private Type _paramClrType;

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public InternalDbQuery([NotNull] DbContext context)
        {
            Check.NotNull(context, nameof(context));

            // Just storing context/service locator here so that the context will be initialized by the time the
            // set is used and services will be obtained from the correctly scoped container when this happens.
            _context = context;
        }

        private IEntityType EntityType
        {
            get
            {
                if (_entityType != null)
                {
                    return _entityType;
                }

                _entityType = _context.Model.FindEntityType(typeof(TQuery));

                if (_entityType == null)
                {
                    if (_context.Model.HasEntityTypeWithDefiningNavigation(typeof(TQuery)))
                    {
                        throw new InvalidOperationException(CoreStrings.InvalidSetTypeWeak(typeof(TQuery).ShortDisplayName()));
                    }

                    throw new InvalidOperationException(CoreStrings.InvalidSetType(typeof(TQuery).ShortDisplayName()));
                }

                if (!_entityType.IsQueryType)
                {
                    _entityType = null;

                    throw new InvalidOperationException(CoreStrings.InvalidSetTypeEntity(typeof(TQuery).ShortDisplayName()));
                }

                return _entityType;
            }
        }

        private Type EntityClrType
        {
            get
            {
                if (_entityClrType != null)
                {
                    return _entityClrType;
                }

                _entityClrType = typeof(TQuery);

                return _entityClrType;
            }
        }

        private Type ParamClrType
        {
            get
            {
                if (_paramClrType != null)
                {
                    return _paramClrType;
                }

                _paramClrType = typeof(TParam);

                return _paramClrType;
            }
        }

        private void CheckState()
        {
            // ReSharper disable once AssignmentIsFullyDiscarded
            _ = EntityType;

            _ = EntityClrType;

            _ = ParamClrType;
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public override IQueryable<TQuery> GetQuery(TParam parameters)
        {
            CheckState();

            return new EntityQueryable<TQuery>(_context.GetDependencies().QueryProvider);
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public override Type ElementType
        {
            get
            {
                CheckState();

                return EntityClrType;
            }
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public override Type ParameterType
        {
            get
            {
                CheckState();

                return ParamClrType;
            }
        }

        IServiceProvider IInfrastructure<IServiceProvider>.Instance
            => _context.GetInfrastructure();
    }
}
