using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace ProductManagement.Classes
{
    public class ClassAlreadyExistsException : BusinessException
    {
        public ClassAlreadyExistsException(string className)
            : base("PM:000001", $"A className with code {className} has already exists!")
        {

        }
    }
}
