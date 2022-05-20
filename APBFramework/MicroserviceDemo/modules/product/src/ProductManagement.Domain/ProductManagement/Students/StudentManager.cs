using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace ProductManagement.Students
{
    public class StudentManager : DomainService
    {
        private readonly IRepository<Student, Guid> _studentRepository;

        public StudentManager(IRepository<Student, Guid> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Student> CreateAsync(
            [NotNull] string studentName,
            int gender, DateTime dateOfBirth,
            string placeOfBirth,
            string address,
            Guid currentClassId)
        {
            Check.NotNullOrWhiteSpace(studentName, nameof(studentName));

            var existingProduct = await _studentRepository.FirstOrDefaultAsync(p => p.StudentName == studentName);
            if (existingProduct != null)
            {
                throw new StudentAlreadyExistsException(studentName);
            }

            return await _studentRepository.InsertAsync(
                new Student(GuidGenerator.Create(), studentName, gender, dateOfBirth,
                placeOfBirth, address, currentClassId));
        }

    }
}
