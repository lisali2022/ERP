using JM.SqlServer.Domain.Shared.Enums;
using Volo.Abp.Application.Dtos;

namespace JM.SqlServer.Application.Contracts.Dtos.Item
{
    public class ItemDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public ItemTypeEnum Type { get; set; }

        public DateTime PublishDate { get; set; }

        public float Price { get; set; }
    }
}
