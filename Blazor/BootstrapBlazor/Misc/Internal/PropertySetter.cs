﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#if NET6_0_OR_GREATER
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Microsoft.AspNetCore.Components.Reflection;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal sealed class PropertySetter
{
    private static readonly MethodInfo CallPropertySetterOpenGenericMethod =
        typeof(PropertySetter).GetMethod(nameof(CallPropertySetter), BindingFlags.NonPublic | BindingFlags.Static)!;

    private readonly Action<object, object> _setterDelegate;

    [UnconditionalSuppressMessage(
        "ReflectionAnalysis",
        "IL2060:MakeGenericMethod",
        Justification = "The referenced methods don't have any DynamicallyAccessedMembers annotations. See https://github.com/mono/linker/issues/1727")]
    public PropertySetter(Type targetType, PropertyInfo property)
    {
        if (property.SetMethod == null)
        {
            throw new InvalidOperationException("Cannot provide a value for property " +
                $"'{property.Name}' on type '{targetType.FullName}' because the property " +
                "has no setter.");
        }

        var setMethod = property.SetMethod;

        var propertySetterAsAction =
            setMethod.CreateDelegate(typeof(Action<,>).MakeGenericType(targetType, property.PropertyType));
        var callPropertySetterClosedGenericMethod =
            CallPropertySetterOpenGenericMethod.MakeGenericMethod(targetType, property.PropertyType);
        _setterDelegate = (Action<object, object>)
            callPropertySetterClosedGenericMethod.CreateDelegate(typeof(Action<object, object>), propertySetterAsAction);
    }

    public bool Cascading { get; init; }

    public void SetValue(object target, object value) => _setterDelegate(target, value);

    private static void CallPropertySetter<TTarget, TValue>(
        Action<TTarget, TValue> setter,
        object target,
        object value)
        where TTarget : notnull
    {
        if (value == null)
        {
            setter((TTarget)target, default!);
        }
        else
        {
            setter((TTarget)target, (TValue)value);
        }
    }
}
#endif
