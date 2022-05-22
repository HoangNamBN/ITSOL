using System;
using System.Linq;

namespace demolinq
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string[] Colors { get; set; }

        public int Brand { get; set; }

        public Product(int id, string name, double price, string[] colors, int brand)
        {
            Id = id;
            Name = name;
            Price = price;
            Colors = colors;
            Brand = brand;
        }

        override public string ToString()
        {
            return $"{Id, 3} {Name, 12} {Price, 2}, {string.Join(",", Colors)}, {Brand, 4}";
        }
    }

    public class Brand
    {
        public string Name { get; set; }
        
        public int ID { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Product p = new Product(1, "Abc", 1000, new string[] { "Xanh", "Do" }, 3);
            Console.WriteLine(p);
            Console.ReadKey();
        }
    }
}