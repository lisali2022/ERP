using JM.SqlServer.Application.Contracts.Dtos.Item;
using JM.SqlServer.Application.Contracts.IServices;
using JM.SqlServer.Domain.Entities;
using SqlSugar;
using Volo.Abp.Application.Dtos;
using Yi.Framework.Ddd.Application;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace JM.SqlServer.Application.Services
{
    public class ItemAppService :
           YiCrudAppService<
               ItemAggregateRoot, //The Item entity
               ItemDto, //Used to show Items
               Guid, //Primary key of the Item entity
               PagedAndSortedResultRequestDto, //Used for paging/sorting
               ItemCreateUpdateDto>, //Used to create/update a Item
           IItemAppService //implement the IItemAppService
    {
        private ISqlSugarRepository<ItemAggregateRoot, Guid> _repository;
        public ItemAppService(ISqlSugarRepository<ItemAggregateRoot, Guid> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public override async Task<PagedResultDto<ItemDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            {
                RefAsync<int> total = 0;

                //由于直接查询接口基本上都是有包含查询条件的，默认内置的查询接口将无法满足业务的需求，所以基本上多查询都是有进行重写的
                var entities = await _repository._DbQueryable
                              //.WhereIF(!string.IsNullOrEmpty(input.ConfigKey), x => x.ConfigKey.Contains(input.ConfigKey!))
                              //          .WhereIF(!string.IsNullOrEmpty(input.ConfigName), x => x.ConfigName!.Contains(input.ConfigName!))
                              //          .WhereIF(input.StartTime is not null && input.EndTime is not null, x => x.CreationTime >= input.StartTime && x.CreationTime <= input.EndTime)
                              .ToPageListAsync(input.SkipCount, input.MaxResultCount, total);
                return new PagedResultDto<ItemDto>(total, await MapToGetListOutputDtosAsync(entities));
            }
        }
    }
}
