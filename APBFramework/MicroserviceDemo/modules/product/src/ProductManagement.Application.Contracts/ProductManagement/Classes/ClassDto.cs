using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace ProductManagement.Classes
{
    public class ClassDto : AuditedEntityDto<Guid>
    {
        public string ClassName { get; set; }
    }
}
