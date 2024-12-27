using Volo.Abp.Domain;
using Yi.Framework.AuditLogging.Domain.Shared;
using Yi.Framework.Bbs.Domain.Shared;
using Yi.Framework.ChatHub.Domain.Shared;
using Yi.Framework.Rbac.Domain.Shared;

namespace JM.SqlServer.Domain.Shared
{
    [DependsOn(
        typeof(YiFrameworkRbacDomainSharedModule),
        typeof(YiFrameworkBbsDomainSharedModule),
          typeof(YiFrameworkChatHubDomainSharedModule),
        typeof(YiFrameworkAuditLoggingDomainSharedModule),

        typeof(AbpDddDomainSharedModule))]
    public class JMSqlServerDomainSharedModule : AbpModule
    {

    }
}