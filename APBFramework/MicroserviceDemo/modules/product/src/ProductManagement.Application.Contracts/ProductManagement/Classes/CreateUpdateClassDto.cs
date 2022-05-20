using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductManagement.Classes
{
    public class CreateUpdateClassDto
    {
        [Required]
        [StringLength(StudentConsts.MaxClassName)]
        public string ClassName { get; set; }
    }
}
