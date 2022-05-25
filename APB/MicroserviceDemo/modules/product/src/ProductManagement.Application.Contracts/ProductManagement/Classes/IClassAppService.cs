using System;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ProductManagement.Classes
{
    public interface IClassAppService : IApplicationService
    {
        Task<PagedResultDto<ClassDto>> GetListPagedAsync(PagedAndSortedResultRequestDto input);
        Task<ListResultDto<ClassDto>> GetListAsync();
        Task<ClassDto> GetAsync(Guid id);
        Task<ClassDto> CreateAsync(CreateUpdateClassDto input);
        Task<ClassDto> UpdateAsync(Guid id, CreateUpdateClassDto input);
        Task DeleteAsync(Guid id);
    }
}
