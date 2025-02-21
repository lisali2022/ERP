﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Step 组件类
/// </summary>
public partial class Step
{
    /// <summary>
    /// 获得/设置 步骤集合
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public List<StepOption>? Items { get; set; }

    /// <summary>
    /// 获得/设置 是否垂直渲染 默认 false 水平渲染
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool IsVertical { get; set; }

    /// <summary>
    /// 获得/设置 当前步骤索引 默认 0
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public int StepIndex { get; set; }

    /// <summary>
    /// 获得/设置 组件内容实例
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
  public Microsoft.AspNetCore.Components.RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 步骤全部完成时模板 默认 null
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
  public Microsoft.AspNetCore.Components.RenderFragment? FinishedTemplate { get; set; }

    /// <summary>
    /// 获得/设置 步骤全部完成时回调方法
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Func<Task>? OnFinishedCallback { get; set; }

     [Microsoft.AspNetCore.Components.Inject]
    [System.Diagnostics.CodeAnalysis.NotNull]
    private IIconTheme? IconTheme { get; set; }

    private int _currentStepIndex;

    /// <summary>
    /// 获得 组件样式字符串
    /// </summary>
    private string? ClassString => CssBuilder.Default("step")
        .AddClass("step-vertical", IsVertical)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string GetStepString(StepOption option) => $"{Items.IndexOf(option) + 1}";

    private string? GetHeaderClassString(StepOption option)
    {
        var index = Items.IndexOf(option);
        return CssBuilder.Default("step-item")
            .AddClass("is-done", index < _currentStepIndex)
            .AddClass("active", index == _currentStepIndex)
            .Build();
    }

    private string? GetBodyClassString(StepOption option)
    {
        var index = Items.IndexOf(option);
        return CssBuilder.Default("step-body-item")
            .AddClass("active", index == _currentStepIndex)
            .Build();
    }

    private bool ShowFinishedIcon(StepOption option) => !string.IsNullOrEmpty(option.FinishedIcon) && Items.IndexOf(option) < _currentStepIndex;

    private static string? GetIconClassString(StepOption option) => CssBuilder.Default("step-icon")
        .AddClass(option.Icon)
        .Build();

    private static string? GetFinishedIconClassString(StepOption option) => CssBuilder.Default("step-icon")
        .AddClass(option.FinishedIcon)
        .Build();

    private string? _finishedIcon;

    private bool IsFinished => _currentStepIndex == Items.Count;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _currentStepIndex = StepIndex;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        _finishedIcon ??= IconTheme.GetIconByKey(ComponentIcons.StepIcon);
        Items ??= [];
    }

    /// <summary>
    /// 上一步
    /// </summary>
    public void Prev()
    {
        _currentStepIndex = Math.Max(0, _currentStepIndex - 1);
        StateHasChanged();
    }

    /// <summary>
    /// 下一步
    /// </summary>
    public async Task Next()
    {
        _currentStepIndex = Math.Min(Items.Count, _currentStepIndex + 1);
        if (IsFinished && OnFinishedCallback != null)
        {
            await OnFinishedCallback();
        }
        StateHasChanged();
    }

    /// <summary>
    /// 下一步
    /// </summary>
    public void Reset()
    {
        _currentStepIndex = 0;
        StateHasChanged();
    }

    /// <summary>
    /// 添加步骤到组件中
    /// </summary>
    /// <param name="option"></param>
    public void Add(StepOption option)
    {
        Items.Add(option);
    }

    /// <summary>
    /// 插入步骤到组件中
    /// </summary>
    /// <param name="index"></param>
    /// <param name="option"></param>
    public void Insert(int index, StepOption option)
    {
        Items.Insert(index, option);
    }

    /// <summary>
    /// 从组件中移除步骤
    /// </summary>
    /// <param name="option"></param>
    public void Remove(StepOption option)
    {
        Items.Remove(option);
    }
}
