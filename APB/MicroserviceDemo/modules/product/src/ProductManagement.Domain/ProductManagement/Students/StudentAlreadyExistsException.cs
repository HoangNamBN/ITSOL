using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace ProductManagement.Students
{
    public class StudentAlreadyExistsException : BusinessException
    {
        public StudentAlreadyExistsException(string studentName)
            : base("PM:000001", $"A student with code {studentName} has already exists!")
        {

        }
    }
}
