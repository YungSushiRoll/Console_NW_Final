using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext() : base("name=NWContext") { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public void DisplayProducts()
        {
            var db = new NorthwindContext();

            Console.WriteLine("-------------------------------\n" +
                "////////////PRODUCTS///////////\n" +
                "-------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("*  *Discontinued*  *");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("for discontinued products\n" +
                "-------------------------------\n");
            try
            {
                List<Product> products = db.Products.OrderBy(p => p.ProductId).ToList();
                logger.Info(products.Count() + " Product(s) returned");

                Console.WriteLine("All Products in the database:\n" +
                    "-------------------------------\n");
                foreach (var product in products)
                {
                    if (product.Discontinued == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("{0}) {1} *  *Discontinued*  *", product.ProductId, product.ProductName);
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {

                        Console.WriteLine("{0}) {1}", product.ProductId, product.ProductName);
                    }
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }            
        }

        public void AddProduct(Product product)
        {
            this.Products.Add(product);
            this.SaveChanges();
        }
        public void AddCategory(Category category)
        {
            this.Categories.Add(category);
            this.SaveChanges();
        }
        public void DeleteProduct(Product product)
        {
            this.Products.Remove(product);
            this.SaveChanges();
        }
        public void DeleteCategory(Category category)
        {
            this.Categories.Remove(category);
            this.SaveChanges();
        }
    }
}
