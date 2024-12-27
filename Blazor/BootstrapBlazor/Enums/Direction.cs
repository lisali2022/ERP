// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazor.Components;

/// <summary>
/// 下拉框枚举类
/// </summary>
public enum Direction
{
    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("dropdown")]
    Dropdown,

    /// <summary>
    /// Dropup
    /// </summary>
    [System.ComponentModel.Description("dropup")]
    Dropup,

    /// <summary>
    /// Dropleft
    /// </summary>
    [System.ComponentModel.Description("dropstart")]
    Dropleft,

    /// <summary>
    /// Dropright
    /// </summary>
    [System.ComponentModel.Description("dropend")]
    Dropright
}
