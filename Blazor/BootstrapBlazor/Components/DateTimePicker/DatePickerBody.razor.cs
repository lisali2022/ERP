﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 日期选择组件
/// </summary>
public partial class DatePickerBody
{
    /// <summary>
    /// 获得/设置 日历框开始时间
    /// </summary>
    private DateTime StartDate
    {
        get
        {
            var d = GetSafeDayDateTime(CurrentDate, 1 - CurrentDate.Day);
            d = GetSafeDayDateTime(d, 0 - (int)d.DayOfWeek);
            return d;
        }
    }

    /// <summary>
    /// 获得/设置 日历框结束时间
    /// </summary>
    private DateTime EndDate => GetSafeDayDateTime(StartDate, 42);

    /// <summary>
    /// 获得/设置 当前日历框月份
    /// </summary>
    private DateTime CurrentDate { get; set; }

    /// <summary>
    /// 获得/设置 当前日历框时刻值
    /// </summary>
    private TimeSpan CurrentTime { get; set; }

    /// <summary>
    /// 获得/设置 当前选中时间 未点击确认时 与 Value 可能不一致
    /// </summary>
    private DateTime SelectValue { get; set; }

    /// <summary>
    /// 获得/设置 是否显示时刻选择框
    /// </summary>
    private bool ShowTimePicker { get; set; }

    private string? ClassString => CssBuilder.Default("picker-panel")
        .AddClass("is-sidebar", ShowSidebar)
        .AddClass("is-lunar", ShowLunar)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 日期样式
    /// </summary>
    private string? GetDayClass(DateTime day, bool overflow) => CssBuilder.Default()
        .AddClass("prev-month", day.Month < CurrentDate.Month)
        .AddClass("next-month", day.Month > CurrentDate.Month)
        .AddClass("current", day.Date == SelectValue.Date && Ranger == null && day.Month == SelectValue.Month && !overflow)
        .AddClass("start", Ranger != null && day == Ranger.SelectedValue.Start.Date)
        .AddClass("end", Ranger != null && day == Ranger.SelectedValue.End.Date)
        .AddClass("range", Ranger != null && day >= Ranger.SelectedValue.Start.Date && day <= Ranger.SelectedValue.End.Date)
        .AddClass("today", day == DateTime.Today)
        .AddClass("disabled", IsDisabled(day) || overflow)
        .Build();

    private string? WrapperClassString => CssBuilder.Default("picker-pannel-body-main-wrapper")
        .AddClass("is-open", ShowTimePicker)
        .Build();

    private bool IsDisabled(DateTime day) => (MinValue.HasValue && day < MinValue.Value) || (MaxValue.HasValue && day > MaxValue.Value);

    /// <summary>
    /// 获得 上一月按钮样式
    /// </summary>
    private string? PrevMonthClassName => CssBuilder.Default("picker-panel-icon-btn pick-panel-arrow-left")
        .AddClass("d-none", CurrentViewMode == DatePickerViewMode.Year || CurrentViewMode == DatePickerViewMode.Month)
        .Build();

    /// <summary>
    /// 获得 下一月按钮样式
    /// </summary>
    private string? NextMonthClassName => CssBuilder.Default("picker-panel-icon-btn pick-panel-arrow-right")
        .AddClass("d-none", CurrentViewMode == DatePickerViewMode.Year || CurrentViewMode == DatePickerViewMode.Month)
        .Build();

    /// <summary>
    /// 获得 年月日显示表格样式
    /// </summary>
    private string? DateViewClassName => CssBuilder.Default("date-table")
        .AddClass("d-none", CurrentViewMode == DatePickerViewMode.Year || CurrentViewMode == DatePickerViewMode.Month)
        .Build();

    /// <summary>
    /// 获得 年月日显示表格样式
    /// </summary>
    private string? YearViewClassName => CssBuilder.Default("year-table")
        .AddClass("d-none", CurrentViewMode != DatePickerViewMode.Year)
        .Build();

    /// <summary>
    /// 获得 年月日显示表格样式
    /// </summary>
    private string? MonthViewClassName => CssBuilder.Default("month-table")
        .AddClass("d-none", CurrentViewMode != DatePickerViewMode.Month)
        .Build();

    /// <summary>
    /// 获得 年月日显示表格样式
    /// </summary>
    private string? CurrentMonthViewClassName => CssBuilder.Default("picker-panel-header-label")
        .AddClass("d-none", CurrentViewMode == DatePickerViewMode.Year || CurrentViewMode == DatePickerViewMode.Month)
        .Build();

    private ClockPicker? TimePickerPanel { get; set; }

    [System.Diagnostics.CodeAnalysis.NotNull]
    private string? YearText { get; set; }

    /// <summary>
    /// 获得 年显示文字
    /// </summary>
    private string? YearString => CurrentViewMode switch
    {
        DatePickerViewMode.Year => GetYearPeriod(),
        _ => string.Format(YearText, CurrentDate.Year)
    };

    [System.Diagnostics.CodeAnalysis.NotNull]
    private string? MonthText { get; set; }

    private string MonthString => string.Format(MonthText, Months.ElementAtOrDefault(CurrentDate.Month - 1));

    [System.Diagnostics.CodeAnalysis.NotNull]
    private string? YearPeriodText { get; set; }

    /// <summary>
    /// 获得/设置 组件显示模式 默认为显示年月日模式
    /// </summary>
    private DatePickerViewMode CurrentViewMode { get; set; }

    /// <summary>
    /// 获得/设置 组件显示模式 默认为显示年月日模式
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public DatePickerViewMode ViewMode { get; set; } = DatePickerViewMode.Date;

    /// <summary>
    /// 获得/设置 日期时间格式字符串 默认为 null
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public string? DateTimeFormat { get; set; }

    /// <summary>
    /// 获得/设置 日期格式字符串 默认为 null
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public string? DateFormat { get; set; }

    /// <summary>
    /// 获得/设置 时间格式字符串 默认为 null
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public string? TimeFormat { get; set; }

    /// <summary>
    /// 获得/设置 是否显示快捷侧边栏 默认 false 不显示
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool ShowSidebar { get; set; }

    /// <summary>
    /// 获得/设置 侧边栏模板 默认 null
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
  public Microsoft.AspNetCore.Components.RenderFragment<Func<DateTime, Task>>? SidebarTemplate { get; set; }

    /// <summary>
    /// 获得/设置 是否显示左侧控制按钮 默认显示
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool ShowLeftButtons { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示右侧控制按钮 默认显示
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool ShowRightButtons { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示 Footer 区域 默认为 false 不显示
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool ShowFooter { get; set; }

    /// <summary>
    /// 获得/设置 时间 PlaceHolder 字符串
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public string? TimePlaceHolder { get; set; }

    /// <summary>
    /// 获得/设置 日期 PlaceHolder 字符串
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public string? DatePlaceHolder { get; set; }

    /// <summary>
    /// 获得/设置 是否允许为空 默认 false 不允许为空
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [Obsolete("已过期，请使用 ShowClearButton 代替 Please use ShowClearButton")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public bool AllowNull
    {
        get => ShowClearButton;
        set => ShowClearButton = value;
    }

    /// <summary>
    /// 获得/设置 是否显示 Clear 按钮 默认 false 不显示
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool ShowClearButton { get; set; }

    /// <summary>
    /// 获得/设置 点击日期时是否自动关闭弹窗 默认 false
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool AutoClose { get; set; }

    /// <summary>
    /// 获得/设置 确认按钮回调委托
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Func<Task>? OnConfirm { get; set; }

    /// <summary>
    /// 获得/设置 清空按钮回调委托
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Func<Task>? OnClear { get; set; }

    /// <summary>
    /// 获得/设置 清空按钮文字
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public string? ClearButtonText { get; set; }

    /// <summary>
    /// 获得/设置 此刻按钮文字
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public string? NowButtonText { get; set; }

    /// <summary>
    /// 获得/设置 确定按钮文字
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public string? ConfirmButtonText { get; set; }

    /// <summary>
    /// 获得/设置 组件值
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public DateTime Value { get; set; }

    /// <summary>
    /// 获得/设置 组件值改变时回调委托供双向绑定使用
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Microsoft.AspNetCore.Components.EventCallback<DateTime> ValueChanged { get; set; }

    /// <summary>
    /// 获得/设置 当前日期最大值
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public DateTime? MaxValue { get; set; }

    /// <summary>
    /// 获得/设置 当前日期最小值
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public DateTime? MinValue { get; set; }

    /// <summary>
    /// 获得/设置 上一年图标
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? PreviousYearIcon { get; set; }

    /// <summary>
    /// 获得/设置 上一年图标
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? NextYearIcon { get; set; }

    /// <summary>
    /// 获得/设置 上一年图标
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? PreviousMonthIcon { get; set; }

    /// <summary>
    /// 获得/设置 上一年图标
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? NextMonthIcon { get; set; }

    /// <summary>
    /// 获得/设置 子组件模板
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
  public Microsoft.AspNetCore.Components.RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 年月改变时回调方法
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Func<DateTime, Task>? OnDateChanged { get; set; }

    /// <summary>
    /// 获得/设置 日单元格模板
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
  public Microsoft.AspNetCore.Components.RenderFragment<DateTime>? DayTemplate { get; set; }

    /// <summary>
    /// 获得/设置 禁用日单元格模板
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
  public Microsoft.AspNetCore.Components.RenderFragment<DateTime>? DayDisabledTemplate { get; set; }

    /// <summary>
    /// 获得/设置 是否显示中国阴历历法 默认 false
    /// </summary>
    /// <remarks>日期范围 1901 年 2 月 19 日 - 2101 年 1 月 28 日</remarks>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool ShowLunar { get; set; }

    /// <summary>
    /// 获得/设置 是否显示中国 24 节气 默认 false
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool ShowSolarTerm { get; set; }

    /// <summary>
    /// 获得/设置 是否显示节日 默认 false
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool ShowFestivals { get; set; }

    /// <summary>
    /// 获得/设置 是否显示休假日 默认 false
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool ShowHolidays { get; set; }

    /// <summary>
    /// 获得/设置 是否为 Range 内使用 默认为 false
    /// </summary>
    [Microsoft.AspNetCore.Components.CascadingParameter]
    private DateTimeRange? Ranger { get; set; }

     [Microsoft.AspNetCore.Components.Inject]
    [System.Diagnostics.CodeAnalysis.NotNull]
    private ICalendarFestivals? CalendarFestivals { get; set; }

     [Microsoft.AspNetCore.Components.Inject]
    [System.Diagnostics.CodeAnalysis.NotNull]
    private IStringLocalizer<DateTimePicker<DateTime>>? Localizer { get; set; }

     [Microsoft.AspNetCore.Components.Inject]
    [System.Diagnostics.CodeAnalysis.NotNull]
    private IIconTheme? IconTheme { get; set; }

    [System.Diagnostics.CodeAnalysis.NotNull]
    private string? AiraPrevYearLabel { get; set; }

    [System.Diagnostics.CodeAnalysis.NotNull]
    private string? AiraNextYearLabel { get; set; }

    [System.Diagnostics.CodeAnalysis.NotNull]
    private string? AiraPrevMonthLabel { get; set; }

    [System.Diagnostics.CodeAnalysis.NotNull]
    private string? AiraNextMonthLabel { get; set; }

    [System.Diagnostics.CodeAnalysis.NotNull]
    private List<string>? Months { get; set; }

    [System.Diagnostics.CodeAnalysis.NotNull]
    private List<string>? MonthLists { get; set; }

    [System.Diagnostics.CodeAnalysis.NotNull]
    private List<string>? WeekLists { get; set; }

    [System.Diagnostics.CodeAnalysis.NotNull]
    private string? Today { get; set; }

    [System.Diagnostics.CodeAnalysis.NotNull]
    private string? Yesterday { get; set; }

    [System.Diagnostics.CodeAnalysis.NotNull]
    private string? Week { get; set; }

    private TimePickerOption TimePickerOption { get; } = new();

    private Dictionary<DatePickerViewMode, List<DatePickerViewMode>> AllowSwitchModes { get; } = new Dictionary<DatePickerViewMode, List<DatePickerViewMode>>
    {
        [DatePickerViewMode.DateTime] =
        [
            DatePickerViewMode.DateTime,
            DatePickerViewMode.Month,
            DatePickerViewMode.Year
        ],
        [DatePickerViewMode.Date] =
        [
            DatePickerViewMode.Date,
            DatePickerViewMode.Month,
            DatePickerViewMode.Year
        ],
        [DatePickerViewMode.Month] =
        [
            DatePickerViewMode.Month,
            DatePickerViewMode.Year
        ],
        [DatePickerViewMode.Year] =
        [
            DatePickerViewMode.Year
        ]
    };

    private bool IsDateTimeMode => ViewMode == DatePickerViewMode.DateTime && CurrentViewMode == DatePickerViewMode.DateTime;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        CurrentViewMode = ViewMode;
        CurrentDate = Value.Date;
        CurrentTime = Value.TimeOfDay;

        SelectValue = Value;

        DatePlaceHolder ??= Localizer[nameof(DatePlaceHolder)];
        TimePlaceHolder ??= Localizer[nameof(TimePlaceHolder)];
        DateTimeFormat ??= Localizer[nameof(DateTimeFormat)];
        DateFormat ??= Localizer[nameof(DateFormat)];
        TimeFormat ??= Localizer[nameof(TimeFormat)];

        AiraPrevYearLabel ??= Localizer[nameof(AiraPrevYearLabel)];
        AiraNextYearLabel ??= Localizer[nameof(AiraNextYearLabel)];
        AiraPrevMonthLabel ??= Localizer[nameof(AiraPrevMonthLabel)];
        AiraNextMonthLabel ??= Localizer[nameof(AiraNextMonthLabel)];

        ClearButtonText ??= Localizer[nameof(ClearButtonText)];
        NowButtonText ??= Localizer[nameof(NowButtonText)];
        ConfirmButtonText ??= Localizer[nameof(ConfirmButtonText)];

        YearText ??= Localizer[nameof(YearText)];
        MonthText ??= Localizer[nameof(MonthText)];
        YearPeriodText ??= Localizer[nameof(YearPeriodText)];
        MonthLists = [.. Localizer[nameof(MonthLists)].Value.Split(',')];
        Months = [.. Localizer[nameof(Months)].Value.Split(',')];
        WeekLists = [.. Localizer[nameof(WeekLists)].Value.Split(',')];

        Today ??= Localizer[nameof(Today)];
        Yesterday ??= Localizer[nameof(Yesterday)];
        Week ??= Localizer[nameof(Week)];

        PreviousYearIcon ??= IconTheme.GetIconByKey(ComponentIcons.DatePickBodyPreviousYearIcon);
        PreviousMonthIcon ??= IconTheme.GetIconByKey(ComponentIcons.DatePickBodyPreviousMonthIcon);
        NextMonthIcon ??= IconTheme.GetIconByKey(ComponentIcons.DatePickBodyNextMonthIcon);
        NextYearIcon ??= IconTheme.GetIconByKey(ComponentIcons.DatePickBodyNextYearIcon);
    }

    private async Task OnValueChanged()
    {
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }
    }

    /// <summary>
    /// 点击上一年按钮时调用此方法
    /// </summary>
    private async Task OnClickPrevYear()
    {
        CurrentDate = CurrentViewMode == DatePickerViewMode.Year
            ? GetSafeYearDateTime(CurrentDate, -20)
            : GetSafeYearDateTime(CurrentDate, -1);

        if (OnDateChanged != null)
        {
            await OnDateChanged(CurrentDate);
        }
    }

    /// <summary>
    /// 点击上一月按钮时调用此方法
    /// </summary>
    private async Task OnClickPrevMonth()
    {
        CurrentDate = CurrentDate.GetSafeMonthDateTime(-1);

        if (OnDateChanged != null)
        {
            await OnDateChanged(CurrentDate);
        }
    }

    /// <summary>
    /// 点击下一年按钮时调用此方法
    /// </summary>
    private async Task OnClickNextYear()
    {
        CurrentDate = CurrentViewMode == DatePickerViewMode.Year
            ? GetSafeYearDateTime(CurrentDate, 20)
            : GetSafeYearDateTime(CurrentDate, 1);

        if (OnDateChanged != null)
        {
            await OnDateChanged(CurrentDate);
        }
    }

    /// <summary>
    /// 点击下一月按钮时调用此方法
    /// </summary>
    private async Task OnClickNextMonth()
    {
        CurrentDate = CurrentDate.GetSafeMonthDateTime(1);

        if (OnDateChanged != null)
        {
            await OnDateChanged(CurrentDate);
        }
    }

    /// <summary>
    /// Day 选择时触发此方法
    /// </summary>
    /// <param name="d"></param>
    private async Task OnClickDateTime(DateTime d)
    {
        CurrentDate = d;
        SelectValue = d;

        if (Ranger != null || ShouldConfirm)
        {
            await ClickConfirmButton();
        }
        else
        {
            StateHasChanged();
        }
    }

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    private async Task OnTimeChanged(TimeSpan time)
    {
        CurrentTime = time;
        //if(Ranger != null)
        //{

        //}
        if (ShouldConfirm)
        {
            await ClickConfirmButton();
        }
        else
        {
            StateHasChanged();
        }
    }

    private bool ShouldConfirm => !IsDateTimeMode && (AutoClose || ShowFooter == false);

    /// <summary>
    /// 设置组件显示视图方法
    /// </summary>
    /// <param name="view"></param>
    private async Task SwitchView(DatePickerViewMode view)
    {
        if (AllowSwitchModes[ViewMode].Contains(view))
        {
            CurrentViewMode = view;
            StateHasChanged();
        }
        else if (AutoClose)
        {
            await ClickConfirmButton();
        }
    }

    private void SwitchTimeView()
    {
        ShowTimePicker = true;
    }

    internal void SwitchDateView()
    {
        ShowTimePicker = false;
        StateHasChanged();
    }

    /// <summary>
    /// 设置组件显示视图方法
    /// </summary>
    /// <param name="view"></param>
    /// <param name="d"></param>
    private async Task SwitchView(DatePickerViewMode view, DateTime d)
    {
        CurrentDate = d;
        await SwitchView(view);
    }

    /// <summary>
    /// 通过当前时间计算年跨度区间
    /// </summary>
    /// <returns></returns>
    private string GetYearPeriod()
    {
        var start = GetSafeYearDateTime(CurrentDate, 0 - CurrentDate.Year % 20).Year;
        return string.Format(YearPeriodText, start, start + 19);
    }

    /// <summary>
    /// 获取 年视图下的年份
    /// </summary>
    /// <param name="year"></param>
    /// <returns></returns>
    private DateTime GetYear(int year) => GetSafeYearDateTime(CurrentDate, year - (CurrentDate.Year % 20));

    /// <summary>
    /// 获取 年视图下月份单元格显示文字
    /// </summary>
    /// <param name="year"></param>
    /// <returns></returns>
    private string GetYearText(int year) => GetYear(year).Year.ToString();

    /// <summary>
    /// 获取 年视图下的年份单元格样式
    /// </summary>
    /// <returns></returns>
    private string? GetYearClassName(int year, bool overflow) => CssBuilder.Default()
        .AddClass("current", GetSafeYearDateTime(SelectValue, year - (SelectValue.Year % 20)).Year == SelectValue.Year)
        .AddClass("today", GetSafeYearDateTime(Value, year - (Value.Year % 20)).Year == DateTime.Today.Year)
        .AddClass("disabled", overflow)
        .Build();

    /// <summary>
    /// 获取 年视图下的月份
    /// </summary>
    /// <param name="month"></param>
    /// <returns></returns>
    private DateTime GetMonth(int month) => CurrentDate.GetSafeMonthDateTime(month - CurrentDate.Month);

    /// <summary>
    /// 获取 月视图下的月份单元格样式
    /// </summary>
    /// <returns></returns>
    private string? GetMonthClassName(int month) => CssBuilder.Default()
        .AddClass("current", month == SelectValue.Month)
        .AddClass("today", Value.Year == DateTime.Today.Year && month == DateTime.Today.Month)
        .Build();

    /// <summary>
    /// 获取 日视图下日单元格显示文字
    /// </summary>
    /// <param name="day"></param>
    /// <returns></returns>
    private static string GetDayText(int day) => day.ToString();

    /// <summary>
    /// 获取 月视图下月份单元格显示文字
    /// </summary>
    /// <param name="month"></param>
    /// <returns></returns>
    private string GetMonthText(int month) => MonthLists[month - 1];

    /// <summary>
    /// 点击 此刻时调用此方法
    /// </summary>
    private async Task ClickNowButton()
    {
        var val = ViewMode switch
        {
            DatePickerViewMode.DateTime => DateTime.Now,
            _ => DateTime.Today
        };
        CurrentDate = val.Date;
        CurrentTime = val.TimeOfDay;

        await ClickConfirmButton();
    }

    private async Task ClickConfirmButton()
    {
        ResetTimePickerPanel();

        if (Validate())
        {
            Value = CurrentDate + CurrentTime;
            await OnValueChanged();
        }
        if (OnConfirm != null)
        {
            await OnConfirm();
        }
    }

    /// <summary>
    /// 点击 清除按钮调用此方法
    /// </summary>
    /// <returns></returns>
    private async Task ClickClearButton()
    {
        // 关闭 TimerPicker
        ShowTimePicker = false;

        CurrentDate = DateTime.MinValue;
        CurrentTime = TimeSpan.Zero;

        Value = CurrentDate + CurrentTime;
        await OnValueChanged();

        if (OnClear != null)
        {
            await OnClear();
        }
    }

    private async Task OnClickSidebarButton(DateTime d)
    {
        CurrentDate = d.Date;
        CurrentTime = d.TimeOfDay;

        await ClickConfirmButton();
    }

    private void ResetTimePickerPanel()
    {
        // 关闭 TimerPicker
        ShowTimePicker = false;

        TimePickerPanel?.Reset();
    }

    private bool Validate() => (!MinValue.HasValue || SelectValue >= MinValue.Value) && (!MaxValue.HasValue || SelectValue <= MaxValue.Value);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="year"></param>
    /// <returns></returns>
    protected static DateTime GetSafeYearDateTime(DateTime dt, int year)
    {
        var @base = dt;
        if (year < 0)
        {
            if (DateTime.MinValue.AddYears(0 - year) < dt)
            {
                @base = dt.AddYears(year);
            }
            else
            {
                @base = DateTime.MinValue.Date;
            }
        }
        else if (year > 0)
        {
            if (DateTime.MaxValue.AddYears(0 - year) > dt)
            {
                @base = dt.AddYears(year);
            }
            else
            {
                @base = DateTime.MaxValue.Date;
            }
        }
        return @base;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="day"></param>
    /// <returns></returns>
    protected static DateTime GetSafeDayDateTime(DateTime dt, int day)
    {
        var @base = dt;
        if (day < 0)
        {
            if (DateTime.MinValue.AddDays(0 - day) < dt)
            {
                @base = dt.AddDays(day);
            }
            else
            {
                @base = DateTime.MinValue;
            }
        }
        else if (day > 0)
        {
            if (DateTime.MaxValue.AddDays(0 - day) > dt)
            {
                @base = dt.AddDays(day);
            }
            else
            {
                @base = DateTime.MaxValue;
            }
        }
        return @base;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="day"></param>
    /// <returns></returns>
    protected static bool IsDayOverflow(DateTime dt, int day) => DateTime.MaxValue.AddDays(0 - day) < dt;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="year"></param>
    /// <returns></returns>
    protected static bool IsYearOverflow(DateTime dt, int year)
    {
        var ret = false;
        if (year < 0)
        {
            ret = DateTime.MinValue.AddYears(0 - year) > dt;
        }
        else if (year > 0)
        {
            ret = DateTime.MaxValue.AddYears(0 - year) < dt;
        }
        return ret;
    }

    private string GetLunarText(DateTime dateTime) => dateTime.ToLunarText(ShowSolarTerm, ShowFestivals ? CalendarFestivals : null);
}
