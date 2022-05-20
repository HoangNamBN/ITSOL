using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductManagement.Students
{
    public class CreateUpdateSudentDto
    {
        [Required]
        [StringLength(StudentConsts.MaxStudentName)]
        public string StudentName { get; set; }

        [Required]
        public int Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; } = DateTime.Now;

        [Required]
        public string PlaceOfBirth { get; set; }

        [Required]
        public string Address { get; set; }

        public Guid ClassId { get; set; }
    }
}
