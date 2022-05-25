using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace ProductManagement.Students
{
    [RemoteService]
    [Area("productManagement")]
    [Route("api/productManagement/students")]
    public class StudentController : AbpController, IStudentAppService
    {
        private readonly IStudentAppService _studentAppService;

        public StudentController(IStudentAppService studentAppService)
        {
            _studentAppService = studentAppService;
        }


        [HttpPost]
        [Route("create")]
        public Task<StudentDto> CreateAsync(CreateUpdateSudentDto input)
        {
            return _studentAppService.CreateAsync(input);
        }

        [HttpDelete]
        public Task DeleteAsync(Guid id)
        {
            return _studentAppService.DeleteAsync(id);
        }


        [HttpGet]
        [Route("{id}")]
        public Task<StudentDto> GetAsync(Guid id)
        {
            return _studentAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("all")]
        public Task<ListResultDto<StudentDto>> GetListAsync()
        {
            return _studentAppService.GetListAsync();
        }

        [HttpGet]
        [Route("getlist")]
        public Task<PagedResultDto<StudentDto>> GetListPagedAsync(PagedAndSortedResultRequestDto input)
        {
            return _studentAppService.GetListPagedAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public Task<StudentDto> UpdateAsync(Guid id, CreateUpdateSudentDto input)
        {
            return _studentAppService.UpdateAsync(id, input);
        }
    }
}
