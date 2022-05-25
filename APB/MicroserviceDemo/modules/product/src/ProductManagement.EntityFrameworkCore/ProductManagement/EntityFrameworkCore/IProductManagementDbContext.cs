using Microsoft.EntityFrameworkCore;
using ProductManagement.Classes;
using ProductManagement.Customers;
using ProductManagement.Students;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace ProductManagement.EntityFrameworkCore
{
    [ConnectionStringName("ProductManagement")]
    public interface IProductManagementDbContext : IEfCoreDbContext
    {
        DbSet<Product> Products { get; }
        DbSet<Customer> Customers { get; }
        DbSet<Student> Students { get; }
        DbSet<Class> Classes { get; }
    }
}