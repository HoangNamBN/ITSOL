using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductManagement.Customers
{
    public class CreateUpdateCustomerDto
    {
        [Required]
        [StringLength(CustomerConsts.MaxFristName)]
        public string FristName { get; set; }

        [Required]
        [StringLength(CustomerConsts.MaxLastName)]
        public string LastName { get; set; }

        [Required]
        [StringLength(CustomerConsts.MaxEmail)]
        public string Email { get; set; }
    }
}
