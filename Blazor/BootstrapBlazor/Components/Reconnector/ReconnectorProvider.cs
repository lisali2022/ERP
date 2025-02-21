﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

class ReconnectorProvider : IReconnectorProvider
{
    private Action< Microsoft.AspNetCore.Components.RenderFragment?, Microsoft.AspNetCore.Components.RenderFragment?, Microsoft.AspNetCore.Components.RenderFragment?>? _action;

    public void NotifyContentChanged(IReconnector reconnector)
    {
        _action?.Invoke(reconnector.ReconnectingTemplate, reconnector.ReconnectFailedTemplate, reconnector.ReconnectRejectedTemplate);
    }

    public void Register(Action< Microsoft.AspNetCore.Components.RenderFragment?, Microsoft.AspNetCore.Components.RenderFragment?, Microsoft.AspNetCore.Components.RenderFragment?> action) => _action = action;
}
