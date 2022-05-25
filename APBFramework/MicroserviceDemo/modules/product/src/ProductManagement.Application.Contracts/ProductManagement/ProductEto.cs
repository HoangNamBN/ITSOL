using System;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace ProductManagement
{
    public class ProductEto : EntityEto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ImageName { get; set; }
        public float Price { get; set; }
    }
}
