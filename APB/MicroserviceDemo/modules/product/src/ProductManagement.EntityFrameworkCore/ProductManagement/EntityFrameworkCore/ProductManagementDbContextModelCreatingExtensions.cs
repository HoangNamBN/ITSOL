using System;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Classes;
using ProductManagement.Customers;
using ProductManagement.Students;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace ProductManagement.EntityFrameworkCore
{
    public static class ProductManagementDbContextModelCreatingExtensions
    {
        public static void ConfigureProductManagement(
            this ModelBuilder builder,
            Action<ProductManagementModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new ProductManagementModelBuilderConfigurationOptions();

            optionsAction?.Invoke(options);
            
            builder.Entity<Product>(b =>
            {
                b.ToTable(options.TablePrefix + "Products", options.Schema);

                b.ConfigureConcurrencyStamp();
                b.ConfigureExtraProperties();
                b.ConfigureAudited();

                b.Property(x => x.Code).IsRequired().HasMaxLength(ProductConsts.MaxCodeLength);
                b.Property(x => x.Name).IsRequired().HasMaxLength(ProductConsts.MaxNameLength);
                b.Property(x => x.ImageName).HasMaxLength(ProductConsts.MaxImageNameLength);

                b.HasIndex(q => q.Code);
                b.HasIndex(q => q.Name);
            });

            builder.Entity<Customer>(b =>
            {
                b.ToTable(options.TablePrefix + "Customers", options.Schema);

                b.ConfigureConcurrencyStamp();
                b.ConfigureExtraProperties();
                b.ConfigureAudited();

                b.Property(x => x.FristName).IsRequired().HasMaxLength(CustomerConsts.MaxFristName);
                b.Property(x => x.LastName).IsRequired().HasMaxLength(CustomerConsts.MaxLastName);
                b.Property(x => x.Email).HasMaxLength(CustomerConsts.MaxEmail);
            });

            builder.Entity<Student>(b =>
            {
                b.ToTable(options.TablePrefix + "Students", options.Schema);

                b.ConfigureConcurrencyStamp();
                b.ConfigureExtraProperties();
                b.ConfigureAudited();

                b.Property(x => x.StudentName).IsRequired().HasMaxLength(StudentConsts.MaxStudentName);

                b.HasOne<Class>().WithMany().HasForeignKey(x => x.ClassId).IsRequired().OnDelete(DeleteBehavior.Cascade);

                // b.HasOne(x => x.Class).WithMany(x => x.Students).HasForeignKey(x => x.CurrentClassId).IsRequired();
            });

            builder.Entity<Class>(b =>
            {
                b.ToTable(options.TablePrefix + "Classes", options.Schema);

                b.ConfigureConcurrencyStamp();
                b.ConfigureExtraProperties();
                b.ConfigureAudited();

                b.Property(x => x.ClassName).IsRequired().HasMaxLength(StudentConsts.MaxClassName);

                b.HasIndex(x => x.ClassName);
            });
        }
    }
}