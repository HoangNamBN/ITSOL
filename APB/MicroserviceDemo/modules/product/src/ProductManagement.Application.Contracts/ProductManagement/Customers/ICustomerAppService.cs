using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ProductManagement.Customers
{
    public interface ICustomerAppService : IApplicationService
    {
        Task<PagedResultDto<CustomerDto>> GetListPagedAsync(PagedAndSortedResultRequestDto input);
        Task<ListResultDto<CustomerDto>> GetListAsync();
        Task<CustomerDto> GetAsync(Guid id);
        Task<CustomerDto> CreateAsync(CreateUpdateCustomerDto input);
        Task<CustomerDto> UpdateAsync(Guid id, CreateUpdateCustomerDto input);
        Task DeleteAsync(Guid id);
    }
}
