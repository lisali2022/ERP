using JM.SqlServer.Application.Contracts.Dtos.Item;
using Volo.Abp.Application.Dtos;
using Yi.Framework.Ddd.Application.Contracts;

namespace JM.SqlServer.Application.Contracts.IServices
{
    public interface IItemAppService :
      IYiCrudAppService< //Defines CRUD methods
          ItemDto, //Used to show books
          Guid, //Primary key of the book entity
          PagedAndSortedResultRequestDto, //Used for paging/sorting
          ItemCreateUpdateDto> //Used to create/update a book
    {

    }
}
