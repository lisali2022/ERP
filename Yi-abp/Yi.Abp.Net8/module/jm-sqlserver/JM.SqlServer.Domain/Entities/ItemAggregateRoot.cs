using JM.SqlServer.Domain.Shared.Enums;
using SqlSugar;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;

namespace JM.SqlServer.Domain.Entities
{
    [SugarTable("JmItem")]
    public class ItemAggregateRoot : AuditedAggregateRoot<Guid>
    {
        [SugarColumn(IsPrimaryKey = true)]
        public override Guid Id { get; protected set; }
        public string Name { get; set; }

        public ItemTypeEnum Type { get; set; }

        public DateTime PublishDate { get; set; }

        public float Price { get; set; }

        public string? Description { get; set; }

        [SugarColumn(IsIgnore = true)]
        public override ExtraPropertyDictionary ExtraProperties { get; protected set; }
    }
}
