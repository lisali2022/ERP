using Volo.Abp.Application.Dtos;
using Yi.Framework.Rbac.Domain.Shared.Enums;

namespace Yi.Framework.Rbac.Application.Contracts.Dtos.Role
{
    public class RoleGetListOutputDto : EntityDto<Guid>
    {
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public Guid? CreatorId { get; set; }
        public string? RoleName { get; set; }
        public string? RoleCode { get; set; }
        public string? Remark { get; set; }
        public DataScopeEnum DataScope { get; set; } = DataScopeEnum.ALL;
        public bool State { get; set; }

        public long OrderNum { get; set; }
    }
}
