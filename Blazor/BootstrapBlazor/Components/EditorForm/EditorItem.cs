﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;

namespace BootstrapBlazor.Components;

/// <summary>
/// EditorItem 组件
/// </summary>
/// <remarks>用于 EditorForm 的 FieldItems 模板内</remarks>
#if NET6_0_OR_GREATER
public class EditorItem<TModel, TValue> : Microsoft.AspNetCore.Components.ComponentBase, IEditorItem
#else
public class EditorItem<TValue> : Microsoft.AspNetCore.Components.ComponentBase, IEditorItem
#endif
{
    /// <summary>
    /// 获得/设置 绑定字段值
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public TValue? Field { get; set; }

    /// <summary>
    /// 获得/设置 绑定字段值变化回调委托
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Microsoft.AspNetCore.Components.EventCallback<TValue> FieldChanged { get; set; }

    /// <summary>
    /// 获得/设置 绑定列类型
    /// </summary>
    [System.Diagnostics.CodeAnalysis.NotNull]
    public Type? PropertyType { get; set; }

    /// <summary>
    /// 获得/设置 ValueExpression 表达式
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Expression<Func<TValue>>? FieldExpression { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [Obsolete("已弃用，是否显示使用 Visible 参数，新建时使用 IsVisibleWhenAdd 编辑时使用 IsVisibleWhenEdit 只读使用 Readonly 参数，新建时使用 IsReadonlyWhenAdd 编辑时使用 IsReadonlyWhenEdit 参数; Discarded, use Visible parameter. IsVisibleWhenAdd should be used when creating a new one, and IsVisibleWhenEdit should be used when editing")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public bool Editable { get; set; } = true;

    /// <summary>
    /// <inheritdoc/>>
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool Ignore { get; set; }

    /// <summary>
    /// <inheritdoc/>>
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool Readonly { get; set; }

    /// <summary>
    /// 获得/设置 是否不进行验证 默认为 false
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool SkipValidate { get; set; }

    /// <summary>
    /// 获得/设置 是否显示标签 Tooltip 多用于标签文字过长导致裁减时使用 默认 null
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool? ShowLabelTooltip { get; set; }

    /// <summary>
    /// 获得/设置 表头显示文字
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? Step { get; set; }

    /// <summary>
    /// 获得/设置 Textarea行数
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public int Rows { get; set; }

    /// <summary>
    /// 获得/设置 编辑模板
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
#if NET5_0
  public Microsoft.AspNetCore.Components.RenderFragment<object>? EditTemplate { get; set; }
#elif NET6_0_OR_GREATER
  public Microsoft.AspNetCore.Components.RenderFragment<TModel>? EditTemplate { get; set; }

    Microsoft.AspNetCore.Components.RenderFragment<object>? IEditorItem.EditTemplate
    {
        get
        {
            return EditTemplate == null ? null : new Microsoft.AspNetCore.Components.RenderFragment<object>(item => builder =>
            {
                builder.AddContent(0, EditTemplate((TModel)item));
            });
        }
        set
        {
        }
    }
#endif

    /// <summary>
    /// 获得/设置 组件类型 默认为 null
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Type? ComponentType { get; set; }

    /// <summary>
    /// 获得/设置 组件自定义类型参数集合 默认为 null
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public IEnumerable<KeyValuePair<string, object>>? ComponentParameters { get; set; }

    /// <summary>
    /// 获得/设置 placeholder 文本 默认为 null
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? PlaceHolder { get; set; }

    /// <summary>
    /// 获得/设置 显示顺序
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public int Order { get; set; }

    /// <summary>
    /// 获得/设置 额外数据源一般用于下拉框或者 CheckboxList 这种需要额外配置数据源组件使用
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 字典数据源 常用于外键自动转换为名称操作
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public IEnumerable<SelectedItem>? Lookup { get; set; }

    /// <summary>
    /// 获得/设置 字段数据源下拉框是否显示搜索栏 默认 false 不显示
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool ShowSearchWhenSelect { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool IsPopover { get; set; }

    /// <summary>
    /// 获得/设置 字典数据源字符串比较规则 默认 StringComparison.OrdinalIgnoreCase 大小写不敏感 
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public StringComparison LookupStringComparison { get; set; } = StringComparison.OrdinalIgnoreCase;

    /// <summary>
    /// <inheritdoc/>>
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? LookupServiceKey { get; set; }

    /// <summary>
    /// <inheritdoc/>>
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public object? LookupServiceData { get; set; }

    /// <summary>
    /// 获得/设置 自定义验证集合
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public List<IValidator>? ValidateRules { get; set; }

    /// <summary>
    /// 获得/设置 IEditorItem 集合实例
    /// </summary>
    /// <remarks>EditorForm 组件级联传参下来的值</remarks>
    [Microsoft.AspNetCore.Components.CascadingParameter]
    private List<IEditorItem>? EditorItems { get; set; }

    /// <summary>
    /// 获得/设置 当前属性分组
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? GroupName { get; set; }

    /// <summary>
    /// 获得/设置 当前属性分组排序 默认 0
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public int GroupOrder { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        EditorItems?.Add(this);
        if (FieldExpression != null)
        {
            _fieldIdentifier = FieldIdentifier.Create(FieldExpression);
        }

        // 获取模型属性定义类型
        PropertyType = typeof(TValue);
    }

    private FieldIdentifier? _fieldIdentifier;
    /// <summary>
    /// 获取绑定字段显示名称方法
    /// </summary>
    public virtual string GetDisplayName() => Text ?? _fieldIdentifier?.GetDisplayName() ?? string.Empty;

    /// <summary>
    /// 获取绑定字段信息方法
    /// </summary>
    public string GetFieldName() => _fieldIdentifier?.FieldName ?? string.Empty;
}
