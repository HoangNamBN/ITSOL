using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace ProductManagement.Customers
{
    [RemoteService]
    [Area("productManagement")]
    [Route("api/productManagement/customers")]
    public class CustomerController : AbpController, ICustomerAppService
    {
        private readonly ICustomerAppService _customerAppService;

        public CustomerController(ICustomerAppService customerAppService)
        {
            _customerAppService = customerAppService;
        }

        [HttpGet]
        [Route("getlist")]
        public Task<PagedResultDto<CustomerDto>> GetListPagedAsync(PagedAndSortedResultRequestDto input)
        {
            return _customerAppService.GetListPagedAsync(input);
        }

        [HttpGet]
        [Route("all")]
        public Task<ListResultDto<CustomerDto>> GetListAsync()
        {
            return _customerAppService.GetListAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public Task<CustomerDto> GetAsync(Guid id)
        {
            return _customerAppService.GetAsync(id);
        }

        [HttpPost]
        [Route("create")]
        public Task<CustomerDto> CreateAsync(CreateUpdateCustomerDto input)
        {
            return _customerAppService.CreateAsync(input);
        }


        [HttpPut]
        [Route("{id}")]
        public Task<CustomerDto> UpdateAsync(Guid id, CreateUpdateCustomerDto input)
        {
            return _customerAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        public Task DeleteAsync(Guid id)
        {
            return _customerAppService.DeleteAsync(id);
        }
    }
}
