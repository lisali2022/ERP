// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;
/// <summary>
/// 浏览器event事件枚举
/// </summary>
[JsonConverter(typeof(DOMEventsConverter))]
public enum DOMEvents
{
    /// <summary>
    /// Click 事件枚举
    /// </summary>
    [System.ComponentModel.Description("click")]
    Click,
    /// <summary>
    /// Dblclick 事件枚举
    /// </summary>
    [System.ComponentModel.Description("dblclick")]
    Dblclick,
    /// <summary>
    /// Mouseup 事件枚举
    /// </summary>
    [System.ComponentModel.Description("mouseup")]
    Mouseup,
    /// <summary>
    /// Mousedown 事件枚举
    /// </summary>
    [System.ComponentModel.Description("mousedown")]
    Mousedown,
    /// <summary>
    /// Contextmenu 事件枚举
    /// </summary>
    [System.ComponentModel.Description("contextmenu")]
    Contextmenu,
    /// <summary>
    /// Mousewheel 事件枚举
    /// </summary>
    [System.ComponentModel.Description("mousewheel")]
    Mousewheel,
    /// <summary>
    /// DOMMouseScroll 事件枚举
    /// </summary>
    [System.ComponentModel.Description("dOMMouseScroll")]
    DOMMouseScroll,
    /// <summary>
    /// Mouseover 事件枚举
    /// </summary>
    [System.ComponentModel.Description("mouseover")]
    Mouseover,
    /// <summary>
    /// Mouseout 事件枚举
    /// </summary>
    [System.ComponentModel.Description("mouseout")]
    Mouseout,
    /// <summary>
    /// Mousemove 事件枚举
    /// </summary>
    [System.ComponentModel.Description("mousemove")]
    Mousemove,
    /// <summary>
    /// Selectstart 事件枚举
    /// </summary>
    [System.ComponentModel.Description("selectstart")]
    Selectstart,
    /// <summary>
    /// Selectend 事件枚举
    /// </summary>
    [System.ComponentModel.Description("selectend")]
    Selectend,
    /// <summary>
    /// Keydown 事件枚举
    /// </summary>
    [System.ComponentModel.Description("keydown")]
    Keydown,
    /// <summary>
    /// Keypress 事件枚举
    /// </summary>
    [System.ComponentModel.Description("keypress")]
    Keypress,
    /// <summary>
    /// Keyup 事件枚举
    /// </summary>
    [System.ComponentModel.Description("keyup")]
    Keyup,
    /// <summary>
    /// Orientationchange 事件枚举
    /// </summary>
    [System.ComponentModel.Description("orientationchange")]
    Orientationchange,
    /// <summary>
    /// Touchstart 事件枚举
    /// </summary>
    [System.ComponentModel.Description("touchstart")]
    Touchstart,
    /// <summary>
    /// Touchmove 事件枚举
    /// </summary>
    [System.ComponentModel.Description("touchmove")]
    Touchmove,
    /// <summary>
    /// Touchend 事件枚举
    /// </summary>
    [System.ComponentModel.Description("touchend")]
    Touchend,
    /// <summary>
    /// Touchcancel 事件枚举
    /// </summary>
    [System.ComponentModel.Description("touchcancel")]
    Touchcancel,
    /// <summary>
    /// Pointerdown 事件枚举
    /// </summary>
    [System.ComponentModel.Description("pointerdown")]
    Pointerdown,
    /// <summary>
    /// Pointermove 事件枚举
    /// </summary>
    [System.ComponentModel.Description("pointermove")]
    Pointermove,
    /// <summary>
    /// Pointerup 事件枚举
    /// </summary>
    [System.ComponentModel.Description("pointerup")]
    Pointerup,
    /// <summary>
    /// Pointerleave 事件枚举
    /// </summary>
    [System.ComponentModel.Description("pointerleave")]
    Pointerleave,
    /// <summary>
    /// Pointercancel 事件枚举
    /// </summary>
    [System.ComponentModel.Description("pointercancel")]
    Pointercancel,
    /// <summary>
    /// Gesturestart 事件枚举
    /// </summary>
    [System.ComponentModel.Description("gesturestart")]
    Gesturestart,
    /// <summary>
    /// Gesturechange 事件枚举
    /// </summary>
    [System.ComponentModel.Description("gesturechange")]
    Gesturechange,
    /// <summary>
    /// Gestureend 事件枚举
    /// </summary>
    [System.ComponentModel.Description("gestureend")]
    Gestureend,
    /// <summary>
    /// Focus 事件枚举
    /// </summary>
    [System.ComponentModel.Description("focus")]
    Focus,
    /// <summary>
    /// Blur 事件枚举
    /// </summary>
    [System.ComponentModel.Description("blur")]
    Blur,
    /// <summary>
    /// Change 事件枚举
    /// </summary>
    [System.ComponentModel.Description("change")]
    Change,
    /// <summary>
    /// Reset 事件枚举
    /// </summary>
    [System.ComponentModel.Description("reset")]
    Reset,
    /// <summary>
    /// Select 事件枚举
    /// </summary>
    [System.ComponentModel.Description("select")]
    Select,
    /// <summary>
    /// Submit 事件枚举
    /// </summary>
    [System.ComponentModel.Description("submit")]
    Submit,
    /// <summary>
    /// Focusin 事件枚举
    /// </summary>
    [System.ComponentModel.Description("focusin")]
    Focusin,
    /// <summary>
    /// Focusout 事件枚举
    /// </summary>
    [System.ComponentModel.Description("focusout")]
    Focusout,
    /// <summary>
    /// Load 事件枚举
    /// </summary>
    [System.ComponentModel.Description("load")]
    Load,
    /// <summary>
    /// Unload 事件枚举
    /// </summary>
    [System.ComponentModel.Description("unload")]
    Unload,
    /// <summary>
    /// Beforeunload 事件枚举
    /// </summary>
    [System.ComponentModel.Description("beforeunload")]
    Beforeunload,
    /// <summary>
    /// Resize 事件枚举
    /// </summary>
    [System.ComponentModel.Description("resize")]
    Resize,
    /// <summary>
    /// Move 事件枚举
    /// </summary>
    [System.ComponentModel.Description("move")]
    Move,
    /// <summary>
    /// DOMContentLoaded 事件枚举
    /// </summary>
    [System.ComponentModel.Description("dOMContentLoaded")]
    DOMContentLoaded,
    /// <summary>
    /// Readystatechange 事件枚举
    /// </summary>
    [System.ComponentModel.Description("readystatechange")]
    Readystatechange,
    /// <summary>
    /// Error 事件枚举
    /// </summary>
    [System.ComponentModel.Description("error")]
    Error,
    /// <summary>
    /// Abort 事件枚举
    /// </summary>
    [System.ComponentModel.Description("abort")]
    Abort,
    /// <summary>
    /// Scroll 事件枚举
    /// </summary>
    [System.ComponentModel.Description("scroll")]
    Scroll
}

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
class DOMEventsConverter : JsonConverter<DOMEvents>
{
    public override DOMEvents Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, DOMEvents value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToDescriptionString());
}
