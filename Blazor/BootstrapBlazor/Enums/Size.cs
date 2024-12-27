// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazor.Components;

/// <summary>
/// Size 枚举类型
/// </summary>
public enum Size
{
    /// <summary>
    /// 无设置
    /// </summary>
    None,

    /// <summary>
    /// xs 超小设置小于 576px
    /// </summary>
    [System.ComponentModel.Description("xs")]
    ExtraSmall,

    /// <summary>
    /// sm 小设置大于等于 576px
    /// </summary>
    [System.ComponentModel.Description("sm")]
    Small,

    /// <summary>
    /// md 中等设置大于等于 768px
    /// </summary>
    [System.ComponentModel.Description("md")]
    Medium,

    /// <summary>
    /// lg 大设置大于等于 992px
    /// </summary>
    [System.ComponentModel.Description("lg")]
    Large,

    /// <summary>
    /// xl 超大设置大于等于 1200px
    /// </summary>
    [System.ComponentModel.Description("xl")]
    ExtraLarge,

    /// <summary>
    /// xl 超大设置大于等于 1400px
    /// </summary>
    [System.ComponentModel.Description("xxl")]
    ExtraExtraLarge
}
