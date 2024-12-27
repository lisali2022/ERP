using JM.Demo.Application.Contracts;
using JM.Demo.Domain;
using Yi.Framework.Bbs.Application;
using Yi.Framework.ChatHub.Application;
using Yi.Framework.CodeGen.Application;
using Yi.Framework.Ddd.Application;
using Yi.Framework.Rbac.Application;
using Yi.Framework.TenantManagement.Application;

namespace JM.Demo.Application
{
    [DependsOn(
        typeof(JMDemoApplicationContractsModule),
        typeof(JMDemoDomainModule),


        typeof(YiFrameworkRbacApplicationModule),
         typeof(YiFrameworkBbsApplicationModule),
         typeof(YiFrameworkChatHubApplicationModule),
        typeof(YiFrameworkTenantManagementApplicationModule),
        typeof(YiFrameworkCodeGenApplicationModule),

        typeof(YiFrameworkDddApplicationModule)
        )]
    public class JMDemoApplicationModule : AbpModule
    {
    }
}
