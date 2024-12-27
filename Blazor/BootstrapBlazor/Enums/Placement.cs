// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public enum Placement
{
    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("auto")]
    Auto,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("top")]
    Top,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("top-start")]
    TopStart,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("top-center")]
    TopCenter,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("top-end")]
    TopEnd,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("middle")]
    Middle,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("middle-start")]
    MiddleStart,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("middle-center")]
    MiddleCenter,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("middle-end")]
    MiddleEnd,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("bottom")]
    Bottom,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("bottom-start")]
    BottomStart,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("bottom-center")]
    BottomCenter,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("bottom-end")]
    BottomEnd,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("left")]
    Left,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("left-start")]
    LeftStart,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("left-end")]
    LeftEnd,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("right")]
    Right,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("right-start")]
    RightStart,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("right-end")]
    RightEnd,
}
