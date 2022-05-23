using Microsoft.EntityFrameworkCore;
using ProductManagement.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace ProductManagement.Classes
{
    // [Authorize(ProductManagementPermissions.Products.Default)]
    public class StudentAppService : ApplicationService, IStudentAppService
    {
        private readonly IRepository<Student, Guid> _studentRepository;

        private readonly StudentManager _studentManager;

        private readonly IRepository<Class, Guid> _classRepository;


        public StudentAppService(IRepository<Student, Guid> studentRepository, StudentManager studentManager, IRepository<Class, Guid> classRepository)
        {
            _studentManager = studentManager;
            _studentRepository = studentRepository;
            _classRepository = classRepository;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _studentRepository.DeleteAsync(id);
        }

        private async Task NormalizeMaxResultCountAsync(PagedAndSortedResultRequestDto input)
        {
            var maxPageSize = (await SettingProvider.GetOrNullAsync(ProductManagementSettings.MaxPageSize))?.To<int>();
            if (maxPageSize.HasValue && input.MaxResultCount > maxPageSize.Value)
            {
                input.MaxResultCount = maxPageSize.Value;
            }
        }

        /* public async Task<PagedResultDto<StudentDto>> GetListPagedAsync(PagedAndSortedResultRequestDto input)
        {
            await NormalizeMaxResultCountAsync(input);

            var students = await (await _studentRepository.GetQueryableAsync())
                .OrderBy(input.Sorting ?? "Name")
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToListAsync();

            var totalCount = await _studentRepository.GetCountAsync();

            var dtos = ObjectMapper.Map<List<Student>, List<StudentDto>>(students);

            return new PagedResultDto<StudentDto>(totalCount, dtos);
        } */

        public async Task<ListResultDto<StudentDto>> GetListAsync()
        {
            var students = await _studentRepository.GetListAsync();

            var studentsList = ObjectMapper.Map<List<Student>, List<StudentDto>>(students);

            return new ListResultDto<StudentDto>(studentsList);
        }

        /* public async Task<StudentDto> GetAsync(Guid id)
        {
            var classes = await _studentRepository.GetAsync(id);

            return ObjectMapper.Map<Student, StudentDto>(classes);
        }*/

        public async Task<StudentDto> CreateAsync(CreateUpdateSudentDto input)
        {
            var students = await _studentManager.CreateAsync(input.StudentName, input.Gender, input.DateOfBirth, input.PlaceOfBirth, input.Address, input.ClassId);

            return ObjectMapper.Map<Student, StudentDto>(students);
        }

        public async Task<StudentDto> UpdateAsync(Guid id, CreateUpdateSudentDto input)
        {
            var students = await _studentRepository.GetAsync(id);

            students.StudentName = input.StudentName;
            students.Gender = input.Gender;
            students.Address = input.Address;
            students.DateOfBirth = input.DateOfBirth;
            students.PlaceOfBirth = input.PlaceOfBirth;
            students.ClassId = students.ClassId;
            return ObjectMapper.Map<Student, StudentDto>(students);
        }

        // using IQueryable
        public async Task<StudentDto> GetAsync(Guid id)
        {
            var queryable = await _studentRepository.GetQueryableAsync();

            var query = from PmStudents in queryable
                        join PmClasses in await _classRepository.GetQueryableAsync() on PmStudents.ClassId equals PmClasses.Id
                        where PmStudents.Id == id
                        select new { PmStudents, PmClasses };
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Student), id);
            }

            var studentDto = ObjectMapper.Map<Student, StudentDto>(queryResult.PmStudents);
            studentDto.ClassName = queryResult.PmClasses.ClassName;
            return studentDto;
        }


        public async Task<PagedResultDto<StudentDto>> GetListPagedAsync(PagedAndSortedResultRequestDto input)
        {
            var queryable = await _studentRepository.GetQueryableAsync();

            var query = from PmStudents in queryable
                        join PmClasses in await _classRepository.GetQueryableAsync() on PmStudents.ClassId equals PmClasses.Id
                        select new { PmStudents, PmClasses };

            query = query.OrderBy(input.Sorting ?? "Name")
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            var queryResult = await AsyncExecuter.ToListAsync(query);

            var studentDtos = queryResult.Select(x =>
            {
                var studentDto = ObjectMapper.Map<Student, StudentDto>(x.PmStudents);
                studentDto.ClassName = x.PmClasses.ClassName;
                return studentDto;
            }).ToList();

            var totalCount = await _studentRepository.GetCountAsync();
            return new PagedResultDto<StudentDto>(totalCount, studentDtos);
        }
    }
}

