// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;

namespace BootstrapBlazor.Components;

/// <summary>
/// 显示组件基类
/// </summary>
public abstract class DisplayBase<TValue> : BootstrapModuleComponentBase
{
    /// <summary>
    /// 是否显示 标签
    /// </summary>
    protected bool IsShowLabel { get; set; }

    /// <summary>
    /// Gets the <see cref="FieldIdentifier"/> for the bound value.
    /// </summary>
    protected FieldIdentifier? FieldIdentifier { get; set; }

    /// <summary>
    /// 获得/设置 泛型参数 TValue 可为空类型 Type 实例，为空时表示类型不可为空
    /// </summary>
    protected Type? NullableUnderlyingType { get; set; }

    /// <summary>
    /// 获得/设置 泛型参数 TValue 可为空类型 Type 实例
    /// </summary>
    [System.Diagnostics.CodeAnalysis.NotNull]
    protected Type? ValueType { get; set; }

    /// <summary>
    /// Gets or sets the value of the input. This should be used with two-way binding.
    /// </summary>
    /// <example>
    /// @bind-Value="model.PropertyName"
    /// </example>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public TValue? Value { get; set; }

    /// <summary>
    /// Gets or sets a callback that updates the bound value.
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Microsoft.AspNetCore.Components.EventCallback<TValue> ValueChanged { get; set; }

    /// <summary>
    /// Gets or sets an expression that identifies the bound value.
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Expression<Func<TValue>>? ValueExpression { get; set; }

    /// <summary>
    /// 获得/设置 是否显示前置标签 默认值为 null 为空时默认不显示标签
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool? ShowLabel { get; set; }

    /// <summary>
    /// 获得/设置 是否显示 Tooltip 多用于文字过长导致裁减时使用 默认 null
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool? ShowLabelTooltip { get; set; }

    /// <summary>
    /// 获得/设置 显示名称
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? DisplayText { get; set; }

    /// <summary>
    /// 获得 ValidateForm 实例
    /// </summary>
    [Microsoft.AspNetCore.Components.CascadingParameter]
    protected ValidateForm? ValidateForm { get; set; }

    /// <summary>
    /// 获得 IShowLabel 实例
    /// </summary>
    [Microsoft.AspNetCore.Components.CascadingParameter(Name = "EditorForm")]
    protected IShowLabel? EditorForm { get; set; }

    /// <summary>
    /// 获得 InputGroup 实例
    /// </summary>
    [Microsoft.AspNetCore.Components.CascadingParameter]
    protected BootstrapInputGroup? InputGroup { get; set; }

    /// <summary>
    /// 获得 IFilter 实例
    /// </summary>
    [Microsoft.AspNetCore.Components.CascadingParameter]
    protected IFilter? Filter { get; set; }

    /// <summary>
    /// SetParametersAsync 方法
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public override Task SetParametersAsync(Microsoft.AspNetCore.Components.ParameterView  parameters)
    {
        parameters.SetParameterProperties(this);

        NullableUnderlyingType = Nullable.GetUnderlyingType(typeof(TValue));
        ValueType ??= NullableUnderlyingType ?? typeof(TValue);

        if (ValueExpression != null)
        {
            FieldIdentifier = Microsoft.AspNetCore.Components.Forms.FieldIdentifier.Create(ValueExpression);
        }

        // For derived components, retain the usual lifecycle with OnInit/OnParametersSet/etc.
        return base.SetParametersAsync(Microsoft.AspNetCore.Components.ParameterView .Empty);
    }

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        // 显式设置显示标签时一定显示
        var showLabel = ShowLabel;

        if (Filter != null)
        {
            IsShowLabel = false;
        }
        else if (InputGroup == null)
        {
            // 如果被 InputGroup 包裹不显示 Label
            // 组件自身未设置 ShowLabel 取 EditorForm/VaidateForm 级联值
            if (ShowLabel == null && (EditorForm != null || ValidateForm != null))
            {
                showLabel = EditorForm?.ShowLabel ?? ValidateForm?.ShowLabel ?? true;
            }

            IsShowLabel = showLabel ?? false;

            // 设置显示标签时未提供 DisplayText 通过双向绑定获取 DisplayName
            if (IsShowLabel && DisplayText == null && FieldIdentifier.HasValue)
            {
                DisplayText = FieldIdentifier.Value.GetDisplayName();
            }
        }
        else
        {
            IsShowLabel = false;

            if (DisplayText == null && FieldIdentifier.HasValue)
            {
                DisplayText = FieldIdentifier.Value.GetDisplayName();
            }
        }

        if (ShowLabelTooltip == null && EditorForm != null)
        {
            ShowLabelTooltip = EditorForm.ShowLabelTooltip;
        }

        if (ShowLabelTooltip == null && ValidateForm != null)
        {
            ShowLabelTooltip = ValidateForm.ShowLabelTooltip;
        }
    }

    /// <summary>
    /// 将 Value 格式化为 String 方法
    /// </summary>
    /// <param name="value">The value to format.</param>
    /// <returns>A string representation of the value.</returns>
    protected virtual string? FormatValueAsString(TValue value)
    {
        string? ret;
        if (value is SelectedItem item)
        {
            ret = item.Value;
        }
        else
        {
            ret = value?.ToString();
        }
        return ret;
    }
}
