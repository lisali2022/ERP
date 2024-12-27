using FreeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Quartz;
using Volo.Abp.BackgroundWorkers.Quartz;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.EventBus.Local;
using Yi.Framework.Bbs.Domain.Entities;
using Yi.Framework.Bbs.Domain.Shared.Caches;
using Yi.Framework.Bbs.Domain.Shared.Enums;
using Yi.Framework.Bbs.Domain.Shared.Etos;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Yi.Framework.Bbs.Application.Jobs;

public class AccessLogCacheJob : QuartzBackgroundWorkerBase
{
    private readonly ILocalEventBus _localEventBus;

    public AccessLogCacheJob(ILocalEventBus localEventBus)
    {
        _localEventBus = localEventBus;
        JobDetail = JobBuilder.Create<AccessLogCacheJob>().WithIdentity(nameof(AccessLogCacheJob))
            .Build();

        //每10秒执行一次，将本地缓存转入redis，防止丢数据
        Trigger = TriggerBuilder.Create().WithIdentity(nameof(AccessLogCacheJob))
            .WithSimpleSchedule((schedule) => { schedule.WithInterval(TimeSpan.FromSeconds(10)).RepeatForever();; })
            .Build();
    }

    public override async Task Execute(IJobExecutionContext context)
    {
        await _localEventBus.PublishAsync(new AccessLogResetArgs());
    }
}