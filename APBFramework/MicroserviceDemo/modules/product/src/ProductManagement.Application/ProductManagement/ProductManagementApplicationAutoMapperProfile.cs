using AutoMapper;
using ProductManagement.Classes;
using ProductManagement.Customers;
using ProductManagement.Students;
using System.Reflection;

namespace ProductManagement
{
    public class ProductManagementApplicationAutoMapperProfile : Profile
    {
        public ProductManagementApplicationAutoMapperProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Customer, CustomerDto>();
            CreateMap<Class, ClassDto>();
            CreateMap<Student, StudentDto>().IgnoreAllNonExisting();
        }
    }

    public static class IgnoreAllNonExist
    {
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> expression)
        {
            var flags = BindingFlags.Public | BindingFlags.Instance;
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination).GetProperties(flags);

            foreach(var property in destinationType)
            {
                if(sourceType.GetProperty(property.Name, flags) == null)
                {
                    expression.ForMember(property.Name, opt => opt.Ignore());
                }
            }
            return expression;
        }
    }
}