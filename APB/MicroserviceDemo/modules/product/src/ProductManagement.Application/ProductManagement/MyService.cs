//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Volo.Abp.DependencyInjection;
//using Volo.Abp.EventBus.Distributed;

//namespace ProductManagement
//{
//    public class MyService : ITransientDependency
//    {
//        private readonly IDistributedEventBus _distributedEventBus;

//        public MyService(IDistributedEventBus distributedEventBus)
//        {
//            _distributedEventBus = distributedEventBus;
//        }

//        public virtual async Task ChangeStockCountAsync(Guid id, int newCount)
//        {
//            await _distributedEventBus.PublishAsync(
//                new StockCountChangedEto
//                {
//                    Id = id,
//                    NewCount = newCount
//                }
//            ); ;
//        }
//    }
//}
