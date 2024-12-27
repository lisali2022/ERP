// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazor;

/// <summary>
/// TransitionType 枚举类型
/// </summary>
public enum TransitionType
{
    /// <summary>
    /// 淡入效果
    /// </summary>
    [System.ComponentModel.Description("animate__fadeIn")]
    FadeIn,

    /// <summary>
    /// 淡出效果
    /// </summary>
    [System.ComponentModel.Description("animate__fadeOut")]
    FadeOut,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__bounce")]
    Bounce,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__flash")]
    Flash,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__pulse")]
    Pulse,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__rubberBand")]
    RubberBand,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__shake")]
    Shake,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__shakeX")]
    ShakeX,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__shakeY")]
    ShakeY,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__headShake")]
    HeadShake,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__swing")]
    Swing,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__tada")]
    Tada,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__wobble")]
    Wobble,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__jello")]
    Jello,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__bounceIn")]
    BounceIn,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__bounceInDown")]
    BounceInDown,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__bounceInLeft")]
    BounceInLeft,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__bounceInRight")]
    BounceInRight,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__bounceInUp")]
    BounceInUp,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__bounceOut")]
    BounceOut,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__bounceOutDown")]
    BounceOutDown,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__bounceOutLeft")]
    BounceOutLeft,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__bounceOutRight")]
    BounceOutRight,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__bounceOutUp")]
    BounceOutUp,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__fadeInDown")]
    FadeInDown,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__fadeInDownBig")]
    FadeInDownBig,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__fadeInLeft")]
    FadeInLeft,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__fadeInLeftBig")]
    FadeInLeftBig,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__fadeInRight")]
    FadeInRight,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__fadeInRightBig")]
    FadeInRightBig,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__fadeInUp")]
    FadeInUp,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__fadeInUpBig")]
    FadeInUpBig,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__fadeOutDown")]
    FadeOutDown,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__fadeOutDownBig")]
    FadeOutDownBig,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__fadeOutLeft")]
    FadeOutLeft,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__fadeOutLeftBig")]
    FadeOutLeftBig,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__fadeOutRight")]
    FadeOutRight,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__fadeOutRightBig")]
    FadeOutRightBig,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__fadeOutUp")]
    FadeOutUp,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__fadeOutUpBig")]
    FadeOutUpBig,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__flipInX")]
    FlipInX,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__flipInY")]
    FlipInY,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__flipOutX")]
    FlipOutX,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__flipOutY")]
    FlipOutY,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__lightSpeedIn")]
    LightSpeedIn,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__lightSpeedOut")]
    LightSpeedOut,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__lightSpeedInRight")]
    LightSpeedInRight,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__lightSpeedInLeft")]
    LightSpeedInLeft,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__lightSpeedOutRight")]
    LightSpeedOutRight,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__lightSpeedOutLeft")]
    LightSpeedOutLeft,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__rotateIn")]
    RotateIn,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__rotateInDownLeft")]
    RotateInDownLeft,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__rotateInDownRight")]
    RotateInDownRight,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__rotateInUpLeft")]
    RotateInUpLeft,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__rotateInUpRight")]
    RotateInUpRight,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__rotateOut")]
    RotateOut,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__rotateOutDownLeft")]
    RotateOutDownLeft,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__rotateOutDownRight")]
    RotateOutDownRight,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__rotateOutUpLeft")]
    RotateOutUpLeft,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__rotateOutUpRight")]
    RotateOutUpRight,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__hinge")]
    Hinge,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__jackInTheBox")]
    JackInTheBox,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__rollIn")]
    RollIn,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__rollOut")]
    RollOut,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__zoomIn")]
    ZoomIn,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__zoomInDown")]
    ZoomInDown,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__zoomInLeft")]
    ZoomInLeft,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__zoomInRight")]
    ZoomInRight,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__zoomInUp")]
    ZoomInUp,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__zoomOut")]
    ZoomOut,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__zoomOutDown")]
    ZoomOutDown,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__zoomOutLeft")]
    ZoomOutLeft,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__zoomOutRight")]
    ZoomOutRight,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__zoomOutUp")]
    ZoomOutUp,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__slideInDown")]
    SlideInDown,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__slideInLeft")]
    SlideInLeft,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__slideInRight")]
    SlideInRight,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__slideInUp")]
    SlideInUp,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__slideOutDown")]
    SlideOutDown,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__slideOutLeft")]
    SlideOutLeft,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__slideOutRight")]
    SlideOutRight,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__slideOutUp")]
    SlideOutUp,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__heartBeat")]
    HeartBeat,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__fadeInTopLeft")]
    FadeInTopLeft,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__fadeInTopRight")]
    FadeInTopRight,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__fadeInBottomLeft")]
    FadeInBottomLeft,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__fadeInBottomRight")]
    FadeInBottomRight,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__fadeOutTopLeft")]
    FadeOutTopLeft,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__fadeOutTopRight")]
    FadeOutTopRight,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__fadeOutBottomRight")]
    FadeOutBottomRight,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__fadeOutBottomLeft")]
    FadeOutBottomLeft,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__backOutDown")]
    BackOutDown,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__backOutLeft")]
    BackOutLeft,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__backOutRight")]
    BackOutRight,

    /// <summary>
    /// 
    /// </summary>
    [System.ComponentModel.Description("animate__backOutU")]
    BackOutUp
}
