using System;
using Volo.Abp.Application.Dtos;

namespace ProductManagement.Customers
{
    public class CustomerDto : AuditedEntityDto<Guid>
    {
        public string FristName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
