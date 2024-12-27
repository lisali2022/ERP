using JM.SqlServer.Domain.Shared;
using Yi.Framework.Bbs.Application.Contracts;
using Yi.Framework.ChatHub.Application.Contracts;
using Yi.Framework.Ddd.Application.Contracts;
using Yi.Framework.Rbac.Application.Contracts;
using Yi.Framework.TenantManagement.Application.Contracts;

namespace JM.SqlServer.Application.Contracts
{
    [DependsOn(
        typeof(JMSqlServerDomainSharedModule),

        typeof(YiFrameworkRbacApplicationContractsModule),
        typeof(YiFrameworkBbsApplicationContractsModule),
        typeof(YiFrameworkChatHubApplicationContractsModule),

        typeof(YiFrameworkTenantManagementApplicationContractsModule),
        typeof(YiFrameworkDddApplicationContractsModule))]
    public class JMSqlServerApplicationContractsModule:AbpModule
    {

    }
}