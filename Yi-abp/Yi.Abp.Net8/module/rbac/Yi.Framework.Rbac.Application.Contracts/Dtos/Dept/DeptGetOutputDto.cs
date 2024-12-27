using Volo.Abp.Application.Dtos;

namespace Yi.Framework.Rbac.Application.Contracts.Dtos.Dept
{
    public class DeptGetOutputDto : EntityDto<Guid>
    {
        public bool State { get; set; }
        public string DeptName { get; set; } = string.Empty;
        public string DeptCode { get; set; } = string.Empty;
        public string? Leader { get; set; }
        public string? Remark { get; set; }

        public Guid? deptId { get; set; }

        public long OrderNum { get; set; }

        public Guid ParentId { get; set; }
    }
}
