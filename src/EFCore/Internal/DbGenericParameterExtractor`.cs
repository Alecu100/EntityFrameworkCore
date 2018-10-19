// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Microsoft.EntityFrameworkCore.Internal
{
    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class DbGenericParameterExtractor<TParam> : DbGenericParameterExtractor
        where TParam : class
    {
        private readonly List<Func<TParam, DbGenericParameter>> _parameterExtractorsForMembers;

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public DbGenericParameterExtractor()
        {
            _parameterExtractorsForMembers = new List<Func<TParam, DbGenericParameter>>();

            InitializeParameterExtractorsForMembers();
        }

        private void InitializeParameterExtractorsForMembers()
        {
            var paramType = typeof(TParam);

            var dbGenericParamType = typeof(DbGenericParameter);

            var dbGenericParamConstructorInfo = dbGenericParamType.GetConstructors().First();

            var dbGenericParamNamePropInfo = (MemberInfo)dbGenericParamType.GetProperty(nameof(DbGenericParameter.ParameterName));

            var dbGenericParamValuePropInfo = (MemberInfo)dbGenericParamType.GetProperty(nameof(DbGenericParameter.Value));

            var targetFields = paramType.GetFields(BindingFlags.Instance | BindingFlags.Public);

            foreach (var targetField in targetFields)
            {
                CreateFuncExtractorForParamMember(paramType, targetField, dbGenericParamConstructorInfo, dbGenericParamNamePropInfo, dbGenericParamValuePropInfo);
            }

            var targetProperties = paramType.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (var targetProperty in targetProperties)
            {
                if (targetProperty.GetMethod != null)
                {
                    CreateFuncExtractorForParamMember(paramType, targetProperty, dbGenericParamConstructorInfo, dbGenericParamNamePropInfo, dbGenericParamValuePropInfo);
                }
            }
        }

        private void CreateFuncExtractorForParamMember(Type paramType, MemberInfo targetField, ConstructorInfo dbGenericParamConstructorInfo, MemberInfo dbGenericParamNamePropInfo, MemberInfo dbGenericParamValuePropInfo)
        {
            var paramExpression = Expression.Parameter(paramType, "param");

            var paramCurrentMemberAccessExpression = (Expression)Expression.MakeMemberAccess(paramExpression, targetField);

            var paramDbParameterNameExpression = (Expression)Expression.Constant(targetField.Name);

            var initializeDbParameterExpression = Expression.New(dbGenericParamConstructorInfo, new[] { paramDbParameterNameExpression, paramCurrentMemberAccessExpression }, dbGenericParamNamePropInfo, dbGenericParamValuePropInfo);

            var dbParamExtractorExpression = Expression.Lambda<Func<TParam, DbGenericParameter>>(initializeDbParameterExpression, paramExpression);

            var compiledDbExtratorFunc = dbParamExtractorExpression.Compile();

            _parameterExtractorsForMembers.Add(compiledDbExtratorFunc);
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public override IReadOnlyList<DbGenericParameter> ExtractParameters(object paramSource)
        {
            var param = (TParam)paramSource;

            return _parameterExtractorsForMembers.Select(paramExtractor => paramExtractor(param)).ToList();
        }
    }
}
