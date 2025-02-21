﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Routing;
using System.Collections.ObjectModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public class BootstrapBlazorAuthorizeView : Microsoft.AspNetCore.Components.ComponentBase
{
    /// <summary>
    /// 获得/设置 路由关联上下文
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public Type? Type { get; set; }

    /// <summary>
    /// 获得/设置 路由关联上下文
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public IReadOnlyDictionary<string, object>? Parameters { get; set; }

    /// <summary>
    /// 获得/设置 NotAuthorized 模板
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
  public Microsoft.AspNetCore.Components.RenderFragment? NotAuthorized { get; set; }

    /// <summary>
    /// The resource to which access is being controlled.
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public object? Resource { get; set; }

    [Microsoft.AspNetCore.Components.CascadingParameter]
    private Task<AuthenticationState>? AuthenticationState { get; set; }

     [Microsoft.AspNetCore.Components.Inject]
    private IAuthorizationPolicyProvider? AuthorizationPolicyProvider { get; set; }

     [Microsoft.AspNetCore.Components.Inject]
    private IAuthorizationService? AuthorizationService { get; set; }

#if NET6_0_OR_GREATER
     [Microsoft.AspNetCore.Components.Inject]
    [System.Diagnostics.CodeAnalysis.NotNull]
    private Microsoft.AspNetCore.Components.NavigationManager? NavigationManager { get; set; }
#endif

    private bool Authorized { get; set; }

    /// <summary>
    /// OnInitializedAsync 方法
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        Authorized = Type == null
            || await Type.IsAuthorizedAsync(AuthenticationState, AuthorizationPolicyProvider, AuthorizationService, Resource);
    }

    /// <summary>
    /// BuildRenderTree 方法
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        // 判断是否开启权限
        if (Authorized && Type != null)
        {
            var index = 0;
            builder.OpenComponent(index++, Type);
            foreach (var kv in (Parameters ?? new ReadOnlyDictionary<string, object>(new Dictionary<string, object>())))
            {
                builder.AddAttribute(index++, kv.Key, kv.Value);
            }
#if NET6_0_OR_GREATER
            BuildQueryParameters();
#endif
            builder.CloseComponent();
        }
        else
        {
            builder.AddContent(0, NotAuthorized);
        }

#if NET6_0_OR_GREATER
        void BuildQueryParameters()
        {
            var queryParameterSupplier = QueryParameterValueSupplier.ForType(Type);
            if (queryParameterSupplier is not null)
            {
                // Since this component does accept some parameters from query, we must supply values for all of them,
                // even if the querystring in the URI is empty. So don't skip the following logic.
                var url = NavigationManager.Uri;
                var queryStartPos = url.IndexOf('?');
                var query = queryStartPos < 0 ? default : url.AsMemory(queryStartPos);
                queryParameterSupplier.RenderParametersFromQueryString(builder, query);
            }
        }
#endif
    }
}
