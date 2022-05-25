using System;
using Volo.Abp.Application.Dtos;

namespace ProductManagement.Students
{
    public class StudentDto : AuditedEntityDto<Guid>
    {
        public string StudentName { get; set; }
        public string ClassName { get; set; }

        public int Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Address { get; set; }

        public Guid ClassId { get; set; }
    }
}
