// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public enum StackJustifyContent
{
    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("justify-content-start")]
    Start,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("justify-content-center")]
    Center,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("justify-content-end")]
    End,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("justify-content-between")]
    Between,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("justify-content-around")]
    Around,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("justify-content-evenly")]
    Evenly,
}
