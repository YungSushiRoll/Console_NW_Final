using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class Menu
    {
        public void MainMenu()
        {
            Console.WriteLine("-------------------------------\n" +
                "Welcome to Northwind Traders\n" +
                "-------------------------------");
            Console.WriteLine("1) Products");
            Console.WriteLine("2) Categories");
            Console.WriteLine("3) Exit Program");
        }

        public void CategoryHeader()
        {
            Console.WriteLine("-------------------------------\n" +
                "////////////CATEGORIES///////////\n" +
                "-------------------------------");
        }

        public void ProductsMenu()
        {
            Console.WriteLine("-------------------------------\n" +
                "Welcome to Northwind Traders\n" +
                "-------------------------------");
            Console.WriteLine("1) Display Products");          
            Console.WriteLine("2) Display specific product");
            Console.WriteLine("3) Add new product");
            Console.WriteLine("4) Edit a product");
            Console.WriteLine("5) Delete a product");
            Console.WriteLine("6) Exit");
        }

        public void ExistingProducts()
        {
            Console.WriteLine("-------------------------------\n" +
                "Welcome to Northwind Traders\n" +
                "-------------------------------");
            Console.WriteLine("1) Display a product and details");
            Console.WriteLine("2) Display DISCONTINUED Products");
            Console.WriteLine("3) Display ACTIVE Products");
            Console.WriteLine("4) Exit Menu");
        }
        public void ExistingCategories()
        {
            Console.WriteLine("-------------------------------\n" +
                "Welcome to Northwind Traders\n" +
                "-------------------------------");
            Console.WriteLine("1) Display ALL Categories and descriptions");
            Console.WriteLine("2) Display ALL Categories and related Products");
            Console.WriteLine("3) Display specific Category and related Products");
            Console.WriteLine("4) Add new Category");
            Console.WriteLine("5) Edit Category");
            Console.WriteLine("6) Delete Category");
            Console.WriteLine("7) Exit");
        }
        public void ProductDetails()
        {
            Console.WriteLine("-------------------------------\n" +
                "What would you like to edit?\n" +
                "-------------------------------");
            Console.WriteLine("1) Edit Product Name");
            Console.WriteLine("2) Edit Quantity Per Unit");
            Console.WriteLine("3) Edit Unit Price");
            Console.WriteLine("4) Edit Units In Stock");
            Console.WriteLine("5) Edit Units On Order");
            Console.WriteLine("6) Edit Reorder Level");
            Console.WriteLine("7) Edit Discontinued");
            Console.WriteLine("8) Exit");
        }
    }
}
