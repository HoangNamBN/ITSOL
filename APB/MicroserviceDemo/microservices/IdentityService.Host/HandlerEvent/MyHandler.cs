using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using ProductManagement;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;

namespace IdentityService.Host
{
    // được gọi đến khi sự kiện StockCountChangeEto được gọi đến. Cụ thể ở đây được là được gọi đến khi Update Product
    public class MyHandler : IDistributedEventHandler<EntityUpdatedEto<ProductEto>>, ITransientDependency
    {
        public ILogger<MyHandler> Logger { get; set; }

        public async Task HandleEventAsync(EntityUpdatedEto<ProductEto> eventData)
        {
            var id = eventData.Entity.Id;
            var code = eventData.Entity.Code;
            var name = eventData.Entity.Name;
            var imageName = eventData.Entity.ImageName;
            var price = eventData.Entity.Price;
            Logger.LogInformation($"Handled distributed event for a new tenant creation. TenantId: {id}");
            Logger.LogInformation($"Handled distributed event for a new tenant creation. code: {code}");
            Logger.LogInformation($"Handled distributed event for a new tenant creation. name: {name}");
            Logger.LogInformation($"Handled distributed event for a new tenant creation. imageName: {imageName}");
            Logger.LogInformation($"Handled distributed event for a new tenant creation. price: {price}");
        }
    }
}
