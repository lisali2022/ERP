﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Diagnostics.CodeAnalysis;

namespace Microsoft.AspNetCore.Components.Routing;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class RouteContext
{
    private static readonly char[] Separator = new[] { '/' };

    public RouteContext(string path)
    {
        // This is a simplification. We are assuming there are no paths like /a//b/. A proper routing
        // implementation would be more sophisticated.
        Segments = path.Trim('/').Split(Separator, StringSplitOptions.RemoveEmptyEntries);
        // Individual segments are URL-decoded in order to support arbitrary characters, assuming UTF-8 encoding.
        for (int i = 0; i < Segments.Length; i++)
        {
            Segments[i] = Uri.UnescapeDataString(Segments[i]);
        }
    }

    public string[] Segments { get; }

#if NET6_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
#endif
    public Type? Handler { get; set; }

    public IReadOnlyDictionary<string, object>? Parameters { get; set; }
}
