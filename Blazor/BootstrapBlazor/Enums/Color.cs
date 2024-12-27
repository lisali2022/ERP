// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// 颜色枚举类型
/// </summary>
public enum Color
{
    /// <summary>
    /// 无颜色
    /// </summary>
    [System.ComponentModel.Description("none")]
    None,

    /// <summary>
    /// active
    /// </summary>
    [System.ComponentModel.Description("active")]
    Active,

    /// <summary>
    /// primary
    /// </summary>
    [System.ComponentModel.Description("primary")]
    Primary,

    /// <summary>
    /// secondary
    /// </summary>
    [System.ComponentModel.Description("secondary")]
    Secondary,

    /// <summary>
    /// success
    /// </summary>
    [System.ComponentModel.Description("success")]
    Success,

    /// <summary>
    /// danger
    /// </summary>
    [System.ComponentModel.Description("danger")]
    Danger,

    /// <summary>
    /// warning
    /// </summary>
    [System.ComponentModel.Description("warning")]
    Warning,

    /// <summary>
    /// info
    /// </summary>
    [System.ComponentModel.Description("info")]
    Info,

    /// <summary>
    /// light
    /// </summary>
    [System.ComponentModel.Description("light")]
    Light,

    /// <summary>
    /// dark
    /// </summary>
    [System.ComponentModel.Description("dark")]
    Dark,

    /// <summary>
    /// link
    /// </summary>
    [System.ComponentModel.Description("link")]
    Link
}
