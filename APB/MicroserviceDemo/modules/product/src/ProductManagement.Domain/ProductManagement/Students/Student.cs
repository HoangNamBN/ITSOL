using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace ProductManagement.Students
{
    public class Student : AuditedAggregateRoot<Guid>
    {
        [NotNull]
        public string StudentName { get; set; }
        public int Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Address { get; set; }
        public Guid ClassId { get; set; }

        private Student() { }

        // public Class Class { get; set; }

        internal Student(Guid id,string studentName, int gender, DateTime dateOfBirth, string placeOfBirth, string address, Guid classId)
        {
            Id = id;
            SetStudentName(Check.NotNullOrWhiteSpace(studentName, nameof(studentName)));
            Gender = gender;
            DateOfBirth = dateOfBirth;
            PlaceOfBirth = placeOfBirth;
            Address = address;
            ClassId = classId;
        }

        //internal Student(Guid id, [NotNull] string studentName, int gender, DateTime dateOfBirth, string placeOfBirth, string address, Guid currentClassId,[NotNull] Class @class)
        //{
        //    Id = id;
        //    StudentName = studentName;
        //    Gender = gender;
        //    DateOfBirth = dateOfBirth;
        //    PlaceOfBirth = placeOfBirth;
        //    Address = address;
        //    CurrentClassId = currentClassId;
        //    Class = @class;
        //}
        public Student SetStudentName([NotNull] string studentName)
        {
            Check.NotNullOrWhiteSpace(studentName, nameof(studentName));
            if (studentName.Length > StudentConsts.MaxStudentName)
            {
                throw new ArgumentException($"studentName can not be longer than {StudentConsts.MaxStudentName}");
            }
            StudentName = studentName;
            return this;
        }
    }
}
