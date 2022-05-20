using JetBrains.Annotations;
using ProductManagement.Classes;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace ProductManagement
{
    public class ClassManager : DomainService
    {
        private readonly IRepository<Class, Guid> _classRepository;

        public ClassManager(IRepository<Class, Guid> classRepository)
        {
            _classRepository = classRepository;
        }

        public async Task<Class> CreateAsync([NotNull] string className)
        {
            Check.NotNullOrWhiteSpace(className, nameof(className));

            var existingProduct = await _classRepository.FirstOrDefaultAsync(p => p.ClassName == className);
            if (existingProduct != null)
            {
                throw new ClassAlreadyExistsException(className);
            }
            return await _classRepository.InsertAsync(new Class(GuidGenerator.Create(), className));
        }
    }
}
