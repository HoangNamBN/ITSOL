using ProductManagement;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus.Distributed;

namespace ProductService.Host
{
    // được gọi đến khi sự kiện StockCountChangeEto được gọi đến. Cụ thể ở đây được là được gọi đến khi Update Product
    public class MyHandler : IDistributedEventHandler<EntityUpdatedEto<ProductEto>>, ITransientDependency
    {
        public async Task HandleEventAsync(EntityUpdatedEto<ProductEto> eventData)
        {
            var id = eventData.Entity.Id;
        }
    }
}
