﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class FilterLogicItem
{
    private FilterLogic _value;
    private FilterLogic Value
    {
        get
        {
            _value = Logic;
            return _value;
        }
        set
        {
            _value = value;
            if (LogicChanged.HasDelegate) LogicChanged.InvokeAsync(value);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public FilterLogic Logic { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Microsoft.AspNetCore.Components.EventCallback<FilterLogic> LogicChanged { get; set; }

    private IEnumerable<SelectedItem>? Items { get; set; }

     [Microsoft.AspNetCore.Components.Inject]
    [System.Diagnostics.CodeAnalysis.NotNull]
    private IStringLocalizer<FilterLogicItem>? Localizer { get; set; }

    /// <summary>
    /// 
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items = new List<SelectedItem>()
        {
            new SelectedItem("And",Localizer["And"].Value),
            new SelectedItem("Or",Localizer["Or"].Value)
        };
    }
}
