using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace ProductManagement.Customers
{
    public class CustomerManager : DomainService
    {
        public readonly IRepository<Customer, Guid> _customerRepository;

        public CustomerManager(IRepository<Customer, Guid> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> CreateAsync(
            [NotNull] string fristName,
            [NotNull] string lastName, [NotNull] string email)
        {
            return await _customerRepository.InsertAsync(new Customer(GuidGenerator.Create(), fristName, lastName, email));
        }
    }
}
