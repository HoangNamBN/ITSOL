using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace ProductManagement.Classes
{
    [RemoteService]
    [Area("productManagement")]
    [Route("api/productManagement/Classes")]
    public class ClassController : AbpController, IClassAppService
    {
        private readonly IClassAppService _classAppService;

        public ClassController(IClassAppService classAppService)
        {
            _classAppService = classAppService;
        }

        [HttpPost]
        [Route("create")]
        public Task<ClassDto> CreateAsync(CreateUpdateClassDto input)
        {
            return _classAppService.CreateAsync(input);
        }

        [HttpDelete]
        public Task DeleteAsync(Guid id)
        {
            return _classAppService.DeleteAsync(id);
        }


        [HttpGet]
        [Route("{id}")]
        public Task<ClassDto> GetAsync(Guid id)
        {
            return _classAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("all")]
        public Task<ListResultDto<ClassDto>> GetListAsync()
        {
            return _classAppService.GetListAsync();
        }

        [HttpGet]
        [Route("getlist")]
        public Task<PagedResultDto<ClassDto>> GetListPagedAsync(PagedAndSortedResultRequestDto input)
        {
            return _classAppService.GetListPagedAsync(input);
        }


        [HttpPut]
        [Route("{id}")]
        public Task<ClassDto> UpdateAsync(Guid id, CreateUpdateClassDto input)
        {
            return _classAppService.UpdateAsync(id, input);
        }
    }
}
