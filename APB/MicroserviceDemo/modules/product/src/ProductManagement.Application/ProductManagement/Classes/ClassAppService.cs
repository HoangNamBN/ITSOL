using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace ProductManagement.Classes
{
    // [Authorize(ProductManagementPermissions.Products.Default)]
    public class ClassAppService : ApplicationService, IClassAppService
    {
        private readonly IRepository<Class, Guid> _classRepository;

        private readonly ClassManager _classManager;

        public ClassAppService(IRepository<Class, Guid> classRepository, ClassManager classManager)
        {
            _classManager = classManager;
            _classRepository = classRepository;
        }

        public async Task<ClassDto> CreateAsync(CreateUpdateClassDto input)
        {
            var classes = await _classManager.CreateAsync(input.ClassName);

            return ObjectMapper.Map<Class, ClassDto>(classes);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _classRepository.DeleteAsync(id);
        }

        public async Task<ClassDto> GetAsync(Guid id)
        {
            var classes = await _classRepository.GetAsync(id);

            return ObjectMapper.Map<Class, ClassDto>(classes);
        }

        public async Task<ListResultDto<ClassDto>> GetListAsync()
        {
            var classes = await _classRepository.GetListAsync();

            var classesList = ObjectMapper.Map<List<Class>, List<ClassDto>>(classes);

            return new ListResultDto<ClassDto>(classesList);
        }

        private async Task NormalizeMaxResultCountAsync(PagedAndSortedResultRequestDto input)
        {
            var maxPageSize = (await SettingProvider.GetOrNullAsync(ProductManagementSettings.MaxPageSize))?.To<int>();
            if (maxPageSize.HasValue && input.MaxResultCount > maxPageSize.Value)
            {
                input.MaxResultCount = maxPageSize.Value;
            }
        }

        public async Task<PagedResultDto<ClassDto>> GetListPagedAsync(PagedAndSortedResultRequestDto input)
        {
            await NormalizeMaxResultCountAsync(input);

            var classes = await (await _classRepository.GetQueryableAsync())
                .OrderBy(input.Sorting ?? "Name")
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToListAsync();

            var totalCount = await _classRepository.GetCountAsync();

            var dtos = ObjectMapper.Map<List<Class>, List<ClassDto>>(classes);

            return new PagedResultDto<ClassDto>(totalCount, dtos);
        }

        public async Task<ClassDto> UpdateAsync(Guid id, CreateUpdateClassDto input)
        {
            var classes = await _classRepository.GetAsync(id);

            classes.ClassName = input.ClassName;

            return ObjectMapper.Map<Class, ClassDto>(classes);
        }
    }
}
