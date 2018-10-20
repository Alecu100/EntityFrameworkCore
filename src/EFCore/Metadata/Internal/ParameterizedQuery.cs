// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.EntityFrameworkCore.Metadata.Internal
{
    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class ParameterizedQuery : IMutableParameterizedQuery
    {
        private ConfigurationSource _configurationSource;

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public ParameterizedQuery(
            [NotNull] EntityType declaringEntityType,
            [NotNull] Type parameterType,
            [NotNull] LambdaExpression query,
            ConfigurationSource configurationSource)
        {
            Check.NotNull(declaringEntityType, nameof(declaringEntityType));
            Check.NotNull(parameterType, nameof(parameterType));
            Check.NotNull(query, nameof(query));

            DeclaringEntityType = declaringEntityType;
            ParameterType = parameterType;
            Query = query;

            _configurationSource = configurationSource;
        }

        IEntityType IParameterizedQuery.DeclaringEntityType => DeclaringEntityType;

        /// <summary>
        ///     Gets the query type that this belongs to.
        /// </summary>
        public IMutableEntityType DeclaringEntityType { get; }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public Type ParameterType { get; set; }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public LambdaExpression Query { get; set; }
    }
}
