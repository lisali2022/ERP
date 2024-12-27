// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazor.Components;

/// <summary>
/// 步骤状态枚举
/// </summary>
public enum StepStatus
{
    /// <summary>
    /// 未开始
    /// </summary>
    [System.ComponentModel.Description("wait")]
    Wait,

    /// <summary>
    /// 进行中
    /// </summary>
    [System.ComponentModel.Description("process")]
    Process,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("finish")]
    Finish,

    /// <summary>
    /// 已完成
    /// </summary>
    [System.ComponentModel.Description("success")]
    Success,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("error")]
    Error,
}
