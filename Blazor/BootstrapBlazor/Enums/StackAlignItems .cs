// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public enum StackAlignItems
{
    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("align-items-stretch")]
    Stretch,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("align-items-start")]
    Start,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("align-items-center")]
    Center,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("align-items-end")]
    End,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("align-items-baseline")]
    Baseline,
}
