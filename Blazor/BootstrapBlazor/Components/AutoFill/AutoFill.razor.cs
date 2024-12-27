// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// AutoFill 组件
/// </summary>
public partial class AutoFill<TValue>
{
    private bool _isLoading;
    private bool _isShown;

    /// <summary>
    /// 获得 组件样式
    /// </summary>
    protected virtual string? ClassString => CssBuilder.Default("auto-complete auto-fill")
        .AddClass("is-loading", _isLoading)
        .AddClass("show", _isShown && !IsPopover)
        .Build();

    /// <summary>
    /// 获得 最终候选数据源
    /// </summary>
    private List<TValue> _filterItems = [];

    /// <summary>
    /// 获得/设置 组件数据集合
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public IEnumerable<TValue>? Items { get; set; }

    /// <summary>
    /// 获得/设置 匹配数据时显示的数量 默认 null 未设置
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public int? DisplayCount { get; set; }

    /// <summary>
    /// 获得/设置 是否开启模糊查询，默认为 false
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool IsLikeMatch { get; set; }

    /// <summary>
    /// 获得/设置 匹配时是否忽略大小写，默认为 true
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool IgnoreCase { get; set; } = true;

    /// <summary>
    /// 获得/设置 自定义集合过滤规则
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Func<string, Task<IEnumerable<TValue>>>? OnCustomFilter { get; set; }

    /// <summary>
    /// 获得/设置 候选项模板
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
  public Microsoft.AspNetCore.Components.RenderFragment<TValue>? Template { get; set; }

    /// <summary>
    /// 获得/设置 通过模型获得显示文本方法 默认使用 ToString 重载方法
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public Func<TValue, string>? OnGetDisplayText { get; set; }

    /// <summary>
    /// 获得/设置 是否跳过 Enter 按键处理 默认 false
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool SkipEnter { get; set; }

    /// <summary>
    /// 获得/设置 是否跳过 Esc 按键处理 默认 false
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool SkipEsc { get; set; }

    /// <summary>
    /// 获得/设置 选项改变回调方法 默认 null
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Func<TValue, Task>? OnSelectedItemChanged { get; set; }

    /// <summary>
    /// 图标
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 加载图标
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? LoadingIcon { get; set; }

     [Microsoft.AspNetCore.Components.Inject]
    [System.Diagnostics.CodeAnalysis.NotNull]
    private IStringLocalizer<AutoComplete>? Localizer { get; set; }

    private string _inputString = "";

    private TValue? ActiveSelectedItem { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        NoDataTip ??= Localizer[nameof(NoDataTip)];
        PlaceHolder ??= Localizer[nameof(PlaceHolder)];
        Items ??= Enumerable.Empty<TValue>();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Icon ??= IconTheme.GetIconByKey(ComponentIcons.AutoFillIcon);
        LoadingIcon ??= IconTheme.GetIconByKey(ComponentIcons.LoadingIcon);

        OnGetDisplayText ??= v => v?.ToString() ?? "";
        _inputString = Value == null ? string.Empty : OnGetDisplayText(Value);
    }

    /// <summary>
    /// OnBlur 方法
    /// </summary>
    protected async Task OnBlur()
    {
        _isShown = false;
        if (OnSelectedItemChanged != null && ActiveSelectedItem != null)
        {
            await OnSelectedItemChanged(ActiveSelectedItem);
            ActiveSelectedItem = default;
        }
    }

    /// <summary>
    /// 鼠标点击候选项时回调此方法
    /// </summary>
    protected virtual async Task OnClickItem(TValue val)
    {
        _isShown = false;
        CurrentValue = val;
        _inputString = OnGetDisplayText(val);
        ActiveSelectedItem = default;
        if (OnSelectedItemChanged != null)
        {
            await OnSelectedItemChanged(val);
        }
    }

    /// <summary>
    /// OnFocus 方法
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    private async Task OnFocus(FocusEventArgs args)
    {
        if (ShowDropdownListOnFocus)
        {
            await OnKeyUp("");
        }
    }

    /// <summary>
    /// OnKeyUp 方法
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    [Microsoft.JSInterop.JSInvokable]
    public virtual async Task OnKeyUp(string key)
    {
        if (!_isLoading)
        {
            _isLoading = true;
            if (OnCustomFilter != null)
            {
                var items = await OnCustomFilter(_inputString);
                _filterItems = items.ToList();
            }
            else
            {
                var items = FindItem();
                _filterItems = DisplayCount == null ? items.ToList() : items.Take(DisplayCount.Value).ToList();
            }
            _isLoading = false;
        }

        if (_filterItems.Any())
        {
            _isShown = true;
            // 键盘向上选择
            if (key == "ArrowUp")
            {
                var index = 0;
                if (ActiveSelectedItem != null)
                {
                    index = _filterItems.IndexOf(ActiveSelectedItem) - 1;
                    if (index < 0)
                    {
                        index = _filterItems.Count - 1;
                    }
                }
                ActiveSelectedItem = _filterItems[index];
                CurrentItemIndex = index;
            }
            else if (key == "ArrowDown")
            {
                var index = 0;
                if (ActiveSelectedItem != null)
                {
                    index = _filterItems.IndexOf(ActiveSelectedItem) + 1;
                    if (index > _filterItems.Count - 1)
                    {
                        index = 0;
                    }
                }
                ActiveSelectedItem = _filterItems[index];
                CurrentItemIndex = index;
            }
            else if (key == "Escape")
            {
                await OnBlur();
                if (!SkipEsc && OnEscAsync != null)
                {
                    await OnEscAsync(Value);
                }
            }
            else if (key == "Enter")
            {
                ActiveSelectedItem ??= FindItem().FirstOrDefault();
                if (ActiveSelectedItem != null)
                {
                    _inputString = OnGetDisplayText(ActiveSelectedItem);
                }
                await OnBlur();
                if (!SkipEnter && OnEnterAsync != null)
                {
                    await OnEnterAsync(Value);
                }
            }
        }
        StateHasChanged();

        IEnumerable<TValue> FindItem()
        {
            var comparison = IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            return IsLikeMatch ?
                Items.Where(s => OnGetDisplayText(s).Contains(_inputString, comparison)) :
                Items.Where(s => OnGetDisplayText(s).StartsWith(_inputString, comparison));
        }
    }

    /// <summary>
    /// TriggerOnChange 方法
    /// </summary>
    /// <param name="val"></param>
    [Microsoft.JSInterop.JSInvokable]
    public void TriggerOnChange(string val)
    {
        _inputString = val;
    }
}
