using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;

namespace ProductManagement
{
    //[Authorize(ProductManagementPermissions.Products.Default)]
    public class ProductAppService : ApplicationService, IProductAppService, ITransientDependency
    {
        private readonly ProductManager _productManager;
        private readonly IRepository<Product, Guid> _productRepository;
        private readonly IDistributedEventBus _distributedEventBus;

        public ProductAppService(ProductManager productManager, IRepository<Product, Guid> productRepository, IDistributedEventBus distributedEventBus)
        {
            _productManager = productManager;
            _productRepository = productRepository;
            _distributedEventBus = distributedEventBus;
        }

        public async Task<PagedResultDto<ProductDto>> GetListPagedAsync(PagedAndSortedResultRequestDto input)
        {
            await NormalizeMaxResultCountAsync(input);

            var products = await (await _productRepository.GetQueryableAsync())
                .OrderBy(input.Sorting ?? "Name")
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToListAsync();

            var totalCount = await _productRepository.GetCountAsync();

            var dtos = ObjectMapper.Map<List<Product>, List<ProductDto>>(products);

            return new PagedResultDto<ProductDto>(totalCount, dtos);
        }

        public async Task<ListResultDto<ProductDto>> GetListAsync() //TODO: Why there are two GetList. GetListPagedAsync would be enough (rename it to GetList)!
        {
            var products = await _productRepository.GetListAsync();

            var productList =  ObjectMapper.Map<List<Product>, List<ProductDto>>(products);

            return new ListResultDto<ProductDto>(productList);
        }

        public async Task<ProductDto> GetAsync(Guid id)
        {
            var product = await _productRepository.GetAsync(id);

            return ObjectMapper.Map<Product, ProductDto>(product);
        }

        // [Authorize(ProductManagementPermissions.Products.Create)]
        public async Task<ProductDto> CreateAsync(CreateProductDto input)
        {
            var product = await _productManager.CreateAsync(
                input.Code,
                input.Name,
                input.Price,
                input.StockCount,
                input.ImageName
            );

            return ObjectMapper.Map<Product, ProductDto>(product);
        }

        // [Authorize(ProductManagementPermissions.Products.Update)]
        public async Task<ProductDto> UpdateAsync(Guid id, UpdateProductDto input)
        {
            var product = await _productRepository.GetAsync(id);

            product.SetName(input.Name);
            product.SetPrice(input.Price);
            product.SetStockCount(input.StockCount);
            product.SetImageName(input.ImageName);

            return ObjectMapper.Map<Product, ProductDto>(product);
        }

        // [Authorize(ProductManagementPermissions.Products.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _productRepository.DeleteAsync(id);
        }

        private async Task NormalizeMaxResultCountAsync(PagedAndSortedResultRequestDto input)
        {
            var maxPageSize = (await SettingProvider.GetOrNullAsync(ProductManagementSettings.MaxPageSize))?.To<int>();
            if (maxPageSize.HasValue && input.MaxResultCount > maxPageSize.Value)
            {
                input.MaxResultCount = maxPageSize.Value;
            }
        }

        public async Task ChangeStockCountAsync(Guid id, int newCount)
        {
            await _distributedEventBus.PublishAsync(
                new StockCountChangedEto
                {
                    Id = id,
                    NewCount = newCount
                }
            );
        }
    }
}
