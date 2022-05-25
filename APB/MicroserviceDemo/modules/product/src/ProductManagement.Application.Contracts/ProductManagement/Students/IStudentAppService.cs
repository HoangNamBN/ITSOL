using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ProductManagement.Students
{
    public interface IStudentAppService : IApplicationService
    {
        Task<PagedResultDto<StudentDto>> GetListPagedAsync(PagedAndSortedResultRequestDto input);
        Task<ListResultDto<StudentDto>> GetListAsync();
        Task<StudentDto> GetAsync(Guid id);
        Task<StudentDto> CreateAsync(CreateUpdateSudentDto input);
        Task<StudentDto> UpdateAsync(Guid id, CreateUpdateSudentDto input);
        Task DeleteAsync(Guid id);
    }
}
