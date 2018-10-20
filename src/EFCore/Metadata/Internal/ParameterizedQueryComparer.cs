// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Microsoft.EntityFrameworkCore.Metadata.Internal
{
    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class ParameterizedQueryComparer : IEqualityComparer<IParameterizedQuery>, IComparer<IParameterizedQuery>
    {
        private ParameterizedQueryComparer()
        {
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static readonly ParameterizedQueryComparer Instance = new ParameterizedQueryComparer();

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public virtual bool Equals(IParameterizedQuery x, IParameterizedQuery y)
            => Compare(x, y) == 0;

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public virtual int GetHashCode(IParameterizedQuery obj) =>
            unchecked(
                ((obj.ParameterType.GetHashCode()) * 397)
                ^ EntityTypePathComparer.Instance.GetHashCode(obj.DeclaringEntityType));

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public virtual int Compare(IParameterizedQuery x, IParameterizedQuery y)
        {
            var result = EntityTypePathComparer.Instance.Compare(x.DeclaringEntityType, y.DeclaringEntityType);

            if (result != 0)
            {
                return result;
            }

            return string.CompareOrdinal(x.ParameterType.AssemblyQualifiedName, y.ParameterType.AssemblyQualifiedName);
        }
    }
}
