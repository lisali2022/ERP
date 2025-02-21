using Microsoft.Extensions.Logging;
using SqlSugar;
using Volo.Abp.DependencyInjection;
using Yi.Framework.Rbac.SqlSugarCore;
using Yi.Framework.SqlSugarCore;

namespace JM.SqlServer.SqlSugarCore
{
    public class YiDbContext : YiRbacDbContext
    {
        public YiDbContext(IAbpLazyServiceProvider lazyServiceProvider) : base(lazyServiceProvider)
        {
        }
    }
}
