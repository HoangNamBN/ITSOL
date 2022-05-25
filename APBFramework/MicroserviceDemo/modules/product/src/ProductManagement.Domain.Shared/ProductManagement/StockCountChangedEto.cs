using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.EventBus;

namespace ProductManagement
{
    [EventName("ProductManagement.Products.StockChange")]
    public class StockCountChangedEto
    {
        public Guid Id { get; set; }
        public int NewCount { get; set; }
    }
}
