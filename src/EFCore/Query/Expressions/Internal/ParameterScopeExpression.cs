// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Microsoft.EntityFrameworkCore.Query.Expressions.Internal
{
    /// <summary>
    ///     Represents a scope used to contain parameters passed to a parameterized query
    ///     to distinquish between parameters of the same type or with the same names
    /// </summary>
    public class ParameterScopeExpression : Expression
    {
        /// <summary>
        /// Used to add at the end of an extracted parameter name to denote the scope to which this parameter belongs to.
        /// </summary>
        public const string ParameterScopeSuffix = "_par_sc";

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public ParameterScopeExpression([NotNull] Expression wrappedExpression)
        {
            WrappedExpression = wrappedExpression;
        }

        /// <summary>
        ///     Represents an expression that is contained within this parameter scope. It can reference parameters from the current
        ///     parameter scope.
        /// </summary>
        public Expression WrappedExpression { get; }

        /// <summary>
        ///     Reduces the node and then calls the <see cref="ExpressionVisitor.Visit(Expression)" /> method passing the
        ///     reduced expression.
        ///     Throws an exception if the node isn't reducible.
        /// </summary>
        /// <param name="visitor"> An instance of <see cref="ExpressionVisitor" />. </param>
        /// <returns> The expression being visited, or an expression which should replace it in the tree. </returns>
        /// <remarks>
        ///     Override this method to provide logic to walk the node's children.
        ///     A typical implementation will call visitor.Visit on each of its
        ///     children, and if any of them change, should return a new copy of
        ///     itself with the modified children.
        /// </remarks>
        protected override Expression VisitChildren(ExpressionVisitor visitor)
        {
            visitor.Visit(WrappedExpression);

            return this;
        }

        /// <summary>
        ///     Tests if this object is considered equal to another.
        /// </summary>
        /// <param name="obj"> The object to compare with the current object. </param>
        /// <returns>
        ///     true if the objects are considered equal, false if they are not.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            return ReferenceEquals(this, obj) ? true : obj.GetType() == GetType() && Equals((ParameterScopeExpression)obj);
        }

        /// <summary>
        ///     Returns a hash code for this object.
        /// </summary>
        /// <returns>
        ///     A hash code for this object.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 565;
                hashCode = (hashCode * 397) ^ (WrappedExpression.GetHashCode());

                return hashCode;
            }
        }

        private bool Equals(ParameterScopeExpression other)
            => other.WrappedExpression.Equals(WrappedExpression);
    }
}
