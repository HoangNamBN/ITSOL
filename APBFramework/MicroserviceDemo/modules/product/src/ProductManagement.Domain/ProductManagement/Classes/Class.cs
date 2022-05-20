using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace ProductManagement.Classes
{
    public class Class : AuditedAggregateRoot<Guid>
    {
        [NotNull]
        public string ClassName { get; set; }

        //public ICollection<Student> Students { get; set; }

        private Class() { }

        //internal Class(Guid id, string className, ICollection<Student> students)
        //{
        //    Id = id;
        //    ClassName = className;
        //    Students = students;
        //}

        internal Class(Guid id, string className)
        {
            Id = id;
            SetClassName(className);
        }

        public Class SetClassName([NotNull] string className)
        {
            Check.NotNullOrWhiteSpace(className, nameof(className));
            if (className.Length > StudentConsts.MaxClassName)
            {
                throw new ArgumentException($"className can not be longer than {StudentConsts.MaxClassName}");
            }
            ClassName = className;
            return this;
        }
    }
}