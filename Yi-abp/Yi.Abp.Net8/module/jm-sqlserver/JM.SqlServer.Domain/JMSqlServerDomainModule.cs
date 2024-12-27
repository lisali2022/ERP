using Volo.Abp.Caching;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using JM.SqlServer.Domain.Shared;
using Yi.Framework.AuditLogging.Domain;
using Yi.Framework.Bbs.Domain;
using Yi.Framework.ChatHub.Domain;
using Yi.Framework.Mapster;
using Yi.Framework.Rbac.Domain;
using Yi.Framework.TenantManagement.Domain;

namespace JM.SqlServer.Domain
{
    [DependsOn(
        typeof(JMSqlServerDomainSharedModule),
       
        typeof(YiFrameworkTenantManagementDomainModule),
        typeof(YiFrameworkRbacDomainModule),
        typeof(YiFrameworkBbsDomainModule),
        typeof(YiFrameworkChatHubDomainModule),
        typeof(YiFrameworkAuditLoggingDomainModule),

        typeof(YiFrameworkMapsterModule),
        typeof(AbpDddDomainModule),
        typeof(AbpCachingModule)
        )]
    public class JMSqlServerDomainModule : AbpModule
    {

    }
}