using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace ProductManagement.Customers
{
    //[Authorize(ProductManagementPermissions.Products.Default)] 
    public class CustomerAppService : ApplicationService, ICustomerAppService
    {
        private readonly CustomerManager _customerManager;
        private readonly IRepository<Customer, Guid> _customerRepository;

        public CustomerAppService(CustomerManager customerManager, IRepository<Customer, Guid> customerRepository)
        {
            _customerManager = customerManager;
            _customerRepository = customerRepository;
        }

        public async Task<PagedResultDto<CustomerDto>> GetListPagedAsync(PagedAndSortedResultRequestDto input)
        {
            await NormalizeMaxResultCountAsync(input);

            var customers = await (await _customerRepository.GetQueryableAsync())
                .OrderBy(input.Sorting ?? "Name")
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToListAsync();

            var totalCount = await _customerRepository.GetCountAsync();

            var dtos = ObjectMapper.Map<List<Customer>, List<CustomerDto>>(customers);

            return new PagedResultDto<CustomerDto>(totalCount, dtos);
        }

        public async Task<ListResultDto<CustomerDto>> GetListAsync() //TODO: Why there are two GetList. GetListPagedAsync would be enough (rename it to GetList)!
        {
            var customers = await _customerRepository.GetListAsync();

            var customerList = ObjectMapper.Map<List<Customer>, List<CustomerDto>>(customers);

            return new ListResultDto<CustomerDto>(customerList);
        }

        public async Task<CustomerDto> GetAsync(Guid id)
        {
            var customer = await _customerRepository.GetAsync(id);

            return ObjectMapper.Map<Customer, CustomerDto>(customer);
        }

        // [Authorize(ProductManagementPermissions.Products.Create)]
        public async Task<CustomerDto> CreateAsync(CreateUpdateCustomerDto input)
        {
            var customer = await _customerManager.CreateAsync(
                input.FristName,
                input.LastName,
                input.Email
            );
            return ObjectMapper.Map<Customer, CustomerDto>(customer);
        }

        // [Authorize(ProductManagementPermissions.Products.Update)]
        public async Task<CustomerDto> UpdateAsync(Guid id, CreateUpdateCustomerDto input)
        {
            var customer = await _customerRepository.GetAsync(id);

            customer.SetFristName(input.FristName);
            customer.SetLastName(input.LastName);
            customer.SetEmail(input.Email);

            return ObjectMapper.Map<Customer, CustomerDto>(customer);
        }

        // [Authorize(ProductManagementPermissions.Products.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _customerRepository.DeleteAsync(id);
        }

        private async Task NormalizeMaxResultCountAsync(PagedAndSortedResultRequestDto input)
        {
            var maxPageSize = (await SettingProvider.GetOrNullAsync(ProductManagementSettings.MaxPageSize))?.To<int>();
            if (maxPageSize.HasValue && input.MaxResultCount > maxPageSize.Value)
            {
                input.MaxResultCount = maxPageSize.Value;
            }
        }
    }
}
