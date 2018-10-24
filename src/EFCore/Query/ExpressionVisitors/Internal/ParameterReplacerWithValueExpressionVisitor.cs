// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal
{
    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class ParameterReplacerWithValueExpressionVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression _parameterToReplace;

        private readonly object _valueToReplaceParameter;

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public ParameterReplacerWithValueExpressionVisitor(
            [NotNull] ParameterExpression parameterToReplace,
            [NotNull] object valueToReplaceParameter)
        {
            _parameterToReplace = parameterToReplace;
            _valueToReplaceParameter = valueToReplaceParameter;
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (node.Name.Equals(_parameterToReplace.Name))
            {
                return Expression.Constant(_valueToReplaceParameter);
            }

            return base.VisitParameter(node);
        }
    }
}
