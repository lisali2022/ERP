﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Image 组件
/// </summary>
public partial class ImageViewer
{
    /// <summary>
    /// 获得 组件样式
    /// </summary>
    private string? ClassString => CssBuilder.Default("bb-img")
        .AddClass("is-preview", ShowPreviewList)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? ImageClassString => CssBuilder.Default()
        .AddClass($"obj-fit-{FitMode.ToDescriptionString()}")
        .AddClass("d-none", ShouldHandleError && !IsLoaded)
        .Build();

    /// <summary>
    /// 获得/设置 图片 Url 默认 null 必填
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
#if NET6_0_OR_GREATER
    [Microsoft.AspNetCore.Components.EditorRequired]
#endif
    public string? Url { get; set; }

    /// <summary>
    /// 获得/设置 图片是否异步加载
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool IsAsync { get; set; }

    /// <summary>
    /// 获得/设置 原生 alt 属性 默认 null 未设置
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? Alt { get; set; }

    /// <summary>
    /// 获得/设置 是否显示占位符 适用于大图片加载 默认 false
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool ShowPlaceHolder { get; set; }

    /// <summary>
    /// 获得/设置 加载失败时是否显示错误占位符 默认 false
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool HandleError { get; set; }

    /// <summary>
    /// 获得/设置 占位模板 未设置 <see cref="Url"/> 或者 正在加载时显示 默认 null 未设置
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
  public Microsoft.AspNetCore.Components.RenderFragment? PlaceHolderTemplate { get; set; }

    /// <summary>
    /// 获得/设置 错误模板 默认 null 未设置
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
  public Microsoft.AspNetCore.Components.RenderFragment? ErrorTemplate { get; set; }

    /// <summary>
    /// 获得/设置 原生 object-fit 属性 默认 fill 未设置
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public ObjectFitMode FitMode { get; set; }

    /// <summary>
    /// 获得/设置 原生 z-index 属性 默认 2050
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public int ZIndex { get; set; } = 2050;

    /// <summary>
    /// 获得/设置 预览大图链接集合 默认 null
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public List<string>? PreviewList { get; set; }
    
    /// <summary>
    /// 获得/设置 预览大图当前链接集合点开的索引 默认为 0
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public int PreviewIndex { get; set; } = 0;

    /// <summary>
    /// 获得/设置 图片加载失败时回调方法
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Func<string, Task>? OnErrorAsync { get; set; }

    /// <summary>
    /// 获得/设置 图片加载成功时回调方法
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Func<string, Task>? OnLoadAsync { get; set; }

    /// <summary>
    /// 获得/设置 图片文件图标
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? FileIcon { get; set; }

     [Microsoft.AspNetCore.Components.Inject]
    [System.Diagnostics.CodeAnalysis.NotNull]
    private IIconTheme? IconTheme { get; set; }

    private bool ShowImage => !string.IsNullOrEmpty(Url);

    private bool IsLoaded { get; set; }

    private bool IsError { get; set; }

    private string? IsAsyncString => IsAsync ? "true" : null;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        IsError = false;
        FileIcon ??= IconTheme.GetIconByKey(ComponentIcons.ImageViewerFileIcon);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
        {
            await InvokeVoidAsync("update", Id, PreviewList, PreviewIndex);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Url, PreviewList, PreviewIndex);

    private Microsoft.AspNetCore.Components.RenderFragment RenderChildContent() => builder =>
    {
        if (!IsError)
        {
            builder.OpenElement(0, "img");
            builder.AddAttribute(1, "class", ImageClassString);
            if (!IsAsync)
            {
                builder.AddAttribute(2, "src", Url);
            }
            if (!string.IsNullOrEmpty(Alt))
            {
                builder.AddAttribute(3, "alt", Alt);
            }
            if (ShowPlaceHolder || ShouldHandleError)
            {
                builder.AddAttribute(4, "onload", Microsoft.AspNetCore.Components.EventCallback.Factory.Create(this, async () =>
                {
                    IsLoaded = true;
                    if (OnLoadAsync != null)
                    {
                        await OnLoadAsync(Url);
                    }
                }));
            }
            if (ShouldHandleError)
            {
                builder.AddAttribute(5, "onerror", Microsoft.AspNetCore.Components.EventCallback.Factory.Create(this, async () =>
                {
                    IsError = true;
                    if (OnErrorAsync != null)
                    {
                        await OnErrorAsync(Url);
                    }
                }));
            }
            builder.CloseElement();

            if (ShouldRenderPlaceHolder)
            {
                builder.AddContent(6, PlaceHolderTemplate ?? RenderPlaceHolder());
            }
        }
        else
        {
            builder.AddContent(7, ErrorTemplate ?? RenderErrorTemplate());
        }
    };

    private bool ShouldRenderPlaceHolder => (ShowPlaceHolder || PlaceHolderTemplate != null) && !IsLoaded;

    private bool ShouldHandleError => HandleError || ErrorTemplate != null;

    private bool ShowPreviewList => PreviewList?.Any() ?? false;

    private string PreviewerId => $"prev_{Id}";
}
