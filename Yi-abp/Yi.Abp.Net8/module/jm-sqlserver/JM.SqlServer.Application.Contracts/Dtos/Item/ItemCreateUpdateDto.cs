using System.ComponentModel.DataAnnotations;
using JM.SqlServer.Domain.Shared.Enums;

namespace JM.SqlServer.Application.Contracts.Dtos.Item
{

    public class ItemCreateUpdateDto
    {
            [Required]
            [StringLength(128)]
            public string Name { get; set; }

            [Required]
            public ItemTypeEnum Type { get; set; } = ItemTypeEnum.Undefined;

            [Required]
            [DataType(DataType.Date)]
            public DateTime PublishDate { get; set; } = DateTime.Now;

            [Required]
            public float Price { get; set; }
        }
  
}
