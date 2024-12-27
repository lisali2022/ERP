using Volo.Abp.Application.Dtos;

namespace Yi.Framework.Bbs.Application.Contracts.Dtos.MyType
{
    public class MyTypeGetListInputVo : PagedAndSortedResultRequestDto
    {
        public string? Name { get; set; }
        public string? Color { get; set; }
        public string? BackgroundColor { get; set; }
        public Guid? UserId { get; set; }
    }
}
