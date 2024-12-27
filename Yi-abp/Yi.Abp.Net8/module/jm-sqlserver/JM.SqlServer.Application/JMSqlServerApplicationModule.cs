using JM.SqlServer.Application.Contracts;
using JM.SqlServer.Domain;
using Yi.Framework.Bbs.Application;
using Yi.Framework.ChatHub.Application;
using Yi.Framework.CodeGen.Application;
using Yi.Framework.Ddd.Application;
using Yi.Framework.Rbac.Application;
using Yi.Framework.TenantManagement.Application;

namespace JM.SqlServer.Application
{
    [DependsOn(
        typeof(JMSqlServerApplicationContractsModule),
        typeof(JMSqlServerDomainModule),


        typeof(YiFrameworkRbacApplicationModule),
         typeof(YiFrameworkBbsApplicationModule),
         typeof(YiFrameworkChatHubApplicationModule),
        typeof(YiFrameworkTenantManagementApplicationModule),
        typeof(YiFrameworkCodeGenApplicationModule),

        typeof(YiFrameworkDddApplicationModule)
        )]
    public class JMSqlServerApplicationModule : AbpModule
    {
    }
}
