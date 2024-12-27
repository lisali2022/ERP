using JM.Demo.Domain.Shared;
using Yi.Framework.Bbs.Application.Contracts;
using Yi.Framework.ChatHub.Application.Contracts;
using Yi.Framework.Ddd.Application.Contracts;
using Yi.Framework.Rbac.Application.Contracts;
using Yi.Framework.TenantManagement.Application.Contracts;

namespace JM.Demo.Application.Contracts
{
    [DependsOn(
        typeof(JMDemoDomainSharedModule),

        typeof(YiFrameworkRbacApplicationContractsModule),
        typeof(YiFrameworkBbsApplicationContractsModule),
        typeof(YiFrameworkChatHubApplicationContractsModule),

        typeof(YiFrameworkTenantManagementApplicationContractsModule),
        typeof(YiFrameworkDddApplicationContractsModule))]
    public class JMDemoApplicationContractsModule:AbpModule
    {

    }
}