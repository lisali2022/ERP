using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using JM.Demo.Domain;
using JM.Demo.SqlSugarCore;
using Yi.Framework.AuditLogging.SqlSugarCore;
using Yi.Framework.Bbs.SqlSugarCore;
using Yi.Framework.ChatHub.SqlSugarCore;
using Yi.Framework.CodeGen.SqlSugarCore;
using Yi.Framework.Mapster;
using Yi.Framework.Rbac.SqlSugarCore;
using Yi.Framework.SqlSugarCore;
using Yi.Framework.SqlSugarCore.Abstractions;
using Yi.Framework.TenantManagement.SqlSugarCore;

namespace JM.Demo.SqlsugarCore
{
    [DependsOn(
        typeof(JMDemoDomainModule),

        typeof(YiFrameworkRbacSqlSugarCoreModule),
        typeof(YiFrameworkBbsSqlSugarCoreModule),
        typeof(YiFrameworkCodeGenSqlSugarCoreModule),
         typeof(YiFrameworkChatHubSqlSugarCoreModule),

        typeof(YiFrameworkAuditLoggingSqlSugarCoreModule),
        typeof(YiFrameworkTenantManagementSqlSugarCoreModule),
        typeof(YiFrameworkMapsterModule),
        typeof(YiFrameworkSqlSugarCoreModule)
        )]
    public class JMDemoSqlSugarCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddYiDbContext<YiDbContext>();
            //默认不开放，可根据项目需要是否Db直接对外开放
            //context.Services.AddTransient(x => x.GetRequiredService<ISqlSugarDbContext>().SqlSugarClient);
        }
    }
}