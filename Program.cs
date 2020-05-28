using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using FinalProject.Models;

namespace FinalProject
{
    class Program
    {

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            logger.Info("Program Started");

            String option;
            Menu menu = new Menu();

            do
            {
                menu.MainMenu();
                option = Console.ReadLine();
                switch(option)
                {
                    case "1":
                        {
                            menu.ProductsMenu();
                            String option1 = Console.ReadLine();
                            switch(option1)
                            {
                                case "1":
                                    {
                                        var db = new NorthwindContext();

                                        db.DisplayProducts();

                                    }
                                    break;
                                case "2":
                                    {
                                        var db = new NorthwindContext();
                                        
                                        bool isValidProId = false;
                                        var prodChoice = "";

                                        do
                                        {
                                            menu.ExistingProducts();
                                            prodChoice = Console.ReadLine();
                                            switch(prodChoice)
                                            {
                                                case "1":
                                                    var products = db.Products;
                                                    var proId = "";
                                                    do
                                                    {
                                                        db.DisplayProducts();
                                                        Console.WriteLine("\nWhich Product do you want to view?\n" +
                                                            "-------------------------------\n");
                                                        proId = Console.ReadLine();
                                                        int ID = int.Parse(proId);
                                                        if (isValidProId == false)
                                                        {
                                                            foreach (var id in products)
                                                            {
                                                                if (id.ProductId == ID)
                                                                {
                                                                    isValidProId = true;
                                                                }
                                                            }
                                                        }
                                                        if (isValidProId == true)
                                                        {
                                                            var product = db.Products.Where(p => p.ProductId == ID);
                                                            foreach (var p in product)
                                                            {
                                                                Console.WriteLine
                                                                    (
                                                                        ("Product Id: {0}\n" +
                                                                        "Product name: {1}\n" +
                                                                        "Quantity per unit: {2}\n" +
                                                                        "Reorder Level: {3}\n" +
                                                                        "Supplier Id: {4}\n" +
                                                                        "Unit Price: {5}\n" +
                                                                        "Units in stock: {6}\n" +
                                                                        "Units in order: {7}\n" +
                                                                        "Discontinued: {8}\n") , p.ProductId, p.ProductName, p.QuantityPerUnit, p.ReorderLevel, p.SupplierID, p.UnitPrice, p.UnitsInStock, p.UnitsOnOrder, p.Discontinued
                                                                    );
                                                            }
                                                        }
                                                        if (isValidProId == false)
                                                        {
                                                            isValidProId = false;
                                                            logger.Warn("Invalid selection... pick again..");
                                                        }
                                                    } while (isValidProId == false);
                                                    break;
                                                case "2":
                                                    var disProducts = db.Products.Where(p => p.Discontinued == true);
                                                    foreach (var dp in disProducts)
                                                    {
                                                        Console.ForegroundColor = ConsoleColor.Red;
                                                        Console.WriteLine("{0}\n", dp.ProductName + " * * Discontinued * *");
                                                        Console.ForegroundColor = ConsoleColor.Gray;
                                                    }
                                                    break;
                                                case "3":
                                                    var actProducts = db.Products.Where(p => p.Discontinued == false);
                                                    foreach (var ap in actProducts)
                                                    {
                                                        Console.ForegroundColor = ConsoleColor.Green;
                                                        Console.WriteLine("{0}\n", ap.ProductName + " * * Active * *");
                                                        Console.ForegroundColor = ConsoleColor.Gray;
                                                    }
                                                    break;
                                            }
                                        } while (!prodChoice.Equals("4"));
                                    }
                                    break;
                                case "3":
                                    {
                                        var db = new NorthwindContext();

                                        Console.WriteLine("Enter the name of the product\n");
                                        var name = Console.ReadLine();

                                        Console.WriteLine("What kind of Product is {0}?", name);
                                        Console.WriteLine("-------------------------------");
                                        var categories = db.Categories;
                                        var catId = "";
                                        bool isValidID = false;
                                        do
                                        {
                                            foreach (var category in categories)
                                            {
                                                Console.WriteLine("{0}) {1}", category.CategoryId, category.CategoryName);
                                            }
                                            catId = Console.ReadLine();

                                            if (isValidID == false)
                                            {
                                                foreach (var id in categories)
                                                {
                                                    if (id.CategoryId == int.Parse(catId))
                                                    {
                                                        isValidID = true;
                                                    }
                                                }
                                            }
                                            if (isValidID == false)
                                            {
                                                isValidID = false;
                                                logger.Warn("Invalid selection... pick again..");
                                            }
                                        } while (isValidID == false);

                                        Console.WriteLine("Who is the Supplier for that product?");
                                        Console.WriteLine("-------------------------------");
                                        var suppliers = db.Suppliers;
                                        var supId = "";
                                        isValidID = false;
                                        do
                                        {
                                            foreach (var supplier in suppliers)
                                            {
                                                Console.WriteLine("{0}) {1}", supplier.SupplierId, supplier.CompanyName);
                                            }
                                            supId = Console.ReadLine();

                                            if (isValidID == false)
                                            {
                                                foreach (var id in suppliers)
                                                {
                                                    if (id.SupplierId == int.Parse(supId))
                                                    {
                                                        isValidID = true;
                                                    }
                                                }
                                            }
                                            if (isValidID == false)
                                            {
                                                isValidID = false;
                                                logger.Warn("Invalid selection... pick again..");
                                            }
                                        } while (isValidID == false);

                                        var newProduct = new Product 
                                        { 
                                            ProductName = name,
                                            CategoryID = int.Parse(catId),
                                            SupplierID = 3
                                        };

                                        db.AddProduct(newProduct);
                                        logger.Info("{0} added to Products..", newProduct.ProductName);
                                    }
                                    break;
                                case "4":
                                    var editdb = new NorthwindContext();
                                    var editQuery = editdb.Products;
                                    var editId = "";
                                    bool isValidEdit = false;
                                    do
                                    {
                                        editdb.DisplayProducts();
                                        Console.WriteLine("\nWhich Product do you want to edit?");
                                        editId = Console.ReadLine();
                                        if(isValidEdit == false)
                                        {
                                            foreach (var id in editQuery)
                                            {
                                                if (id.ProductId == int.Parse(editId))
                                                {
                                                    isValidEdit = true;
                                                }
                                            }
                                        }
                                        var ID = int.Parse(editId);
                                        if(isValidEdit == true)
                                        {
                                            var edit = editdb.Products.Where(e => e.ProductId == ID);
                                            bool isValidDetail = false;
                                            do
                                            {
                                                menu.ProductDetails();
                                                var editChoice = Console.ReadLine();
                                                switch(editChoice)
                                                {
                                                    case "1":
                                                        Console.WriteLine("Enter new Product name\n" +
                                                            "-------------------------------\n");
                                                        var newName = Console.ReadLine();
                                                        try
                                                        {
                                                            foreach (var ed in edit)
                                                            {
                                                                ed.ProductName = newName;
                                                                logger.Info("Product name changed to \"{0}\"", newName);
                                                            };
                                                            editdb.SaveChanges();
                                                        } catch (Exception e)
                                                        {
                                                            logger.Warn("Unable to change product name, try again...");
                                                            logger.Warn(e.Message);
                                                        }
                                                        break;
                                                    case "2":
                                                        Console.WriteLine("Enter new Quantity per Unit\n" +
                                                            "-------------------------------\n");
                                                        var newQPU = Console.ReadLine();
                                                        try
                                                        {
                                                            foreach (var ed in edit)
                                                            {
                                                                ed.QuantityPerUnit = newQPU;
                                                                logger.Info("Quantity per Unit changed to \"{0}\"", newQPU);
                                                            };
                                                            editdb.SaveChanges();
                                                        }
                                                        catch (Exception e)
                                                        {
                                                            logger.Warn("Unable to change Quantity per Unit, try again...");
                                                            logger.Warn(e.Message);
                                                        }
                                                        break;
                                                    case "3":
                                                        Console.WriteLine("Enter new Unit Price\n" +
                                                            "-------------------------------\n");
                                                        var newUP = Console.ReadLine();
                                                        try
                                                        {
                                                            foreach (var ed in edit)
                                                            {
                                                                ed.UnitPrice = decimal.Parse(newUP);
                                                                logger.Info("Unit Price changed to \"${0}\"", decimal.Parse(newUP));
                                                            };
                                                            editdb.SaveChanges();
                                                        }
                                                        catch (Exception e)
                                                        {
                                                            logger.Warn("Unable to change Units in Stock, try again...");
                                                            logger.Warn(e.Message);
                                                        }
                                                        break;
                                                    case "4":
                                                        Console.WriteLine("Enter new Units in Stock\n" +
                                                            "-------------------------------\n");
                                                        var newUS = Console.ReadLine();
                                                        try
                                                        {
                                                            foreach (var ed in edit)
                                                            {
                                                                ed.UnitsInStock = short.Parse(newUS);
                                                                logger.Info("Units in Stock changed to \"{0}\"", short.Parse(newUS));
                                                            };
                                                            editdb.SaveChanges();
                                                        }
                                                        catch (Exception e)
                                                        {
                                                            logger.Warn("Unable to change Units in Stock, try again...");
                                                            logger.Warn(e.Message);
                                                        }
                                                        break;
                                                    case "5":
                                                        Console.WriteLine("Enter new Units on Order\n" +
                                                            "-------------------------------\n");
                                                        var newUO = Console.ReadLine();
                                                        try
                                                        {
                                                            foreach (var ed in edit)
                                                            {
                                                                ed.UnitsOnOrder = short.Parse(newUO);
                                                                logger.Info("Units on Order changed to \"{0}\"", short.Parse(newUO));
                                                            };
                                                            editdb.SaveChanges();
                                                        }
                                                        catch (Exception e)
                                                        {
                                                            logger.Warn("Unable to change Units on Order, try again...");
                                                            logger.Warn(e.Message);
                                                        }
                                                        break;
                                                    case "6":
                                                        Console.WriteLine("Enter new Reorder level\n" +
                                                            "-------------------------------\n");
                                                        var newRL = Console.ReadLine();
                                                        try
                                                        {
                                                            foreach (var ed in edit)
                                                            {
                                                                ed.ReorderLevel = short.Parse(newRL);
                                                                logger.Info("Reorder level changed to \"{0}\"", short.Parse(newRL));
                                                            };
                                                            editdb.SaveChanges();
                                                        }
                                                        catch (Exception e)
                                                        {
                                                            logger.Warn("Unable to change Reorder level, try again...");
                                                            logger.Warn(e.Message);
                                                        }
                                                        break;
                                                    case "7":
                                                        Console.WriteLine("Is this item Discontinued Y/N?\n" +
                                                            "-------------------------------\n");
                                                        var newD = Console.ReadLine();
                                                        if (newD.ToUpper().Equals("Y"))
                                                        {
                                                            try
                                                            {
                                                                foreach (var ed in edit)
                                                                {
                                                                    ed.Discontinued = true;
                                                                    logger.Info(ed.ProductName + " is now Discontinued");
                                                                };
                                                                editdb.SaveChanges();
                                                            }
                                                            catch (Exception e)
                                                            {
                                                                logger.Warn("Unable to change Discontinued, try again...");
                                                                logger.Warn(e.Message);
                                                            }
                                                        }
                                                        else if (newD.ToUpper().Equals("N"))
                                                        {
                                                            try
                                                            {
                                                                foreach (var ed in edit)
                                                                {
                                                                    ed.Discontinued = false;
                                                                    logger.Info(ed.ProductName + " is now Active");
                                                                };
                                                                editdb.SaveChanges();
                                                            }
                                                            catch (Exception e)
                                                            {
                                                                logger.Warn("Unable to change Discontinued, try again...");
                                                                logger.Warn(e.Message);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            logger.Warn("Not valid response");
                                                        }
                                                        break;
                                                    case "8":
                                                        isValidDetail = true;
                                                        break;
                                                }

                                            } while (isValidDetail == false);
                                        }
                                    } while (isValidEdit == false);
                                    break;
                                case "5":
                                    //delete product i think
                                    var deleteDb = new NorthwindContext();
                                    var deleteQuery = deleteDb.Products;
                                    Boolean isValidProductID = false;
                                    deleteDb.DisplayProducts();
                                    Console.WriteLine("\nChoose a Product you would like to delete");
                                    var input = Console.ReadLine();
                                    int proID = int.Parse(input);

                                    if (isValidProductID == false)
                                    {
                                        foreach (var item in deleteQuery)
                                        {
                                            if (item.ProductId == proID)
                                            {
                                                isValidProductID = true;
                                            }
                                        }

                                        if (isValidProductID == true)
                                        {
                                            var deleteProduct = deleteDb.Products.SingleOrDefault(p => p.ProductId == proID);
                                            Console.WriteLine("\nAre you sure you want to delete " + deleteProduct.ProductName + "? Y/N");

                                            if (Console.ReadLine().ToUpper().Equals("Y"))
                                            {
                                                deleteDb.DeleteProduct(deleteProduct);
                                                logger.Info("Product \"{0}\" removed from Products", deleteProduct.ProductName);
                                            }
                                        }
                                        else
                                        {
                                            logger.Info("Not a valid Product ID");
                                        }
                                    }
                                    else
                                    {
                                        logger.Info("Not a valid Integer");
                                    }
                                    break;
                            }
                        }
                        break;
                    case "2":
                        var catDB = new NorthwindContext();
                        var catResp = "";
                        do
                        {
                            menu.ExistingCategories();
                            catResp = Console.ReadLine();
                            switch(catResp)
                            {
                                case "1":
                                    var categories = catDB.Categories;
                                    menu.CategoryHeader();
                                    foreach (var category in categories)
                                    {
                                        Console.WriteLine("{0}) {1} - {2}", category.CategoryId, category.CategoryName, category.Description);
                                    }
                                    break;
                                case "2":
                                    var proByCat = catDB.Categories.Include("Products").OrderBy(p => p.CategoryId);
                                    menu.CategoryHeader();
                                    foreach (var category in proByCat)
                                    {
                                        Console.WriteLine("\n{0}) {1} - {2}\n", category.CategoryId, category.CategoryName, category.Description);
                                        foreach(Product product in category.Products)
                                        {
                                            Console.WriteLine("- {0}", product.ProductName);
                                        }
                                    }
                                    break;
                                case "3":
                                    var c = catDB.Categories;
                                    var catId = "";
                                    bool isValidID = false;
                                    do
                                    {
                                        menu.CategoryHeader();
                                        foreach (var category in c)
                                        {
                                            Console.WriteLine("{0}) {1}", category.CategoryId, category.CategoryName);
                                        }
                                        catId = Console.ReadLine();

                                        if (isValidID == false)
                                        {
                                            foreach (var id in c)
                                            {
                                                if (id.CategoryId == int.Parse(catId))
                                                {
                                                    isValidID = true;
                                                }
                                            }
                                        }
                                        var pID = int.Parse(catId);
                                        if (isValidID == true)
                                        {
                                            var catPro = catDB.Categories.Where(p => p.CategoryId == pID);
                                            var sp = catDB.Products.Where(s => s.CategoryID == pID);
                                            foreach (var p in catPro)
                                            {
                                                Console.WriteLine("\n{0} - {1}", p.CategoryName, p.Description);
                                                Console.WriteLine("-------------------------------\n");
                                            }
                                            foreach (var item in sp)
                                            {
                                                Console.WriteLine("{0}", item.ProductName);
                                            }
                                        }
                                        if (isValidID == false)
                                        {
                                            isValidID = false;
                                            logger.Warn("Invalid selection... pick again..");
                                        }
                                    } while (isValidID == false);
                                    break;
                                case "4":
                                    // add new cat
                                    var nc = catDB.Categories.OrderBy(n => n.CategoryId);
                                    var catName = "";
                                    bool isValidCat = true;
                                    do
                                    {
                                        Console.WriteLine("Enter name of new Category\n");
                                        catName = Console.ReadLine();

                                        foreach (var n in nc)
                                        {
                                            if(n.CategoryName.ToUpper().Equals(catName))
                                            {
                                                isValidCat = false;
                                            }
                                        }
                                        if(isValidCat == false)
                                        {
                                            logger.Warn("Category name {0} already exists - enter a different name" , catName);
                                        }
                                        if(isValidCat == true)
                                        {
                                            var newCat = new Category
                                            { CategoryName = catName };
                                            catDB.AddCategory(newCat);
                                            logger.Info("Category added - {0}", catName);
                                        }
                                        
                                    } while (isValidCat == false);
                                    break;
                                case "5":
                                    // edit cat
                                    var editCat = new NorthwindContext();
                                    var ec = editCat.Categories;
                                    bool isVcatId = false;
                                    do
                                    {
                                        menu.CategoryHeader();
                                        foreach (var category in ec)
                                        {
                                            Console.WriteLine("{0}) {1}", category.CategoryId, category.CategoryName);
                                        }
                                        Console.WriteLine("\nChoose a Category to edit\n" +
                                            "-------------------------------\n");
                                        var catInput = Console.ReadLine();
                                        int ID = int.Parse(catInput);
                                        if(isVcatId == false)
                                        {
                                            foreach (var id in ec)
                                            {
                                                if (id.CategoryId == ID)
                                                {
                                                    isVcatId = true;
                                                }
                                            }
                                        }
                                        if (isVcatId == true)
                                        {
                                            var catEdit = editCat.Categories.Where(e => e.CategoryId == ID);
                                            var catChoice = "";
                                            do
                                            {
                                                Console.WriteLine("1) Edit Category Name");
                                                Console.WriteLine("2) Edit Category Description");
                                                Console.WriteLine("3) Exit");
                                                catChoice = Console.ReadLine();
                                                switch (catChoice)
                                                {
                                                    case "1":
                                                        Console.WriteLine("Enter new Category name\n" +
                                                            "-------------------------------\n");
                                                        var newCatName = Console.ReadLine();
                                                        try
                                                        {
                                                            foreach (var ed in catEdit)
                                                            {
                                                                ed.CategoryName = newCatName;
                                                                logger.Info("Category name changed to \"{0}\"", newCatName);
                                                            };
                                                            editCat.SaveChanges();
                                                        }
                                                        catch (Exception e)
                                                        {
                                                            logger.Warn("Unable to change category name, try again...");
                                                            logger.Warn(e.Message);
                                                        }
                                                        break;
                                                    case "2":
                                                        Console.WriteLine("Enter new Category description\n" +
                                                       "-------------------------------\n");
                                                        var newCatDesc = Console.ReadLine();
                                                        try
                                                        {
                                                            foreach (var ed in catEdit)
                                                            {
                                                                ed.Description = newCatDesc;
                                                                logger.Info("Category description changed to \"{0}\"", newCatDesc);
                                                            };
                                                            editCat.SaveChanges();
                                                        }
                                                        catch (Exception e)
                                                        {
                                                            logger.Warn("Unable to change category description, try again...");
                                                            logger.Warn(e.Message);
                                                        }
                                                        break;
                                                }
                                            } while (!catChoice.Equals("3"));
                                        }
                                    } while (isVcatId == false);
                                    break;
                                case "6":
                                    // delete cat
                                    var deleteCat = new NorthwindContext();
                                    var deleteQuery = deleteCat.Categories;
                                    bool isValidCategoryID = false;
                                    do
                                    {
                                        menu.CategoryHeader();
                                        foreach (var category in deleteQuery)
                                        {
                                            Console.WriteLine("{0}) {1}", category.CategoryId, category.CategoryName);
                                        }
                                        Console.WriteLine("\nChoose a Category you wish to delete");
                                        var delInput = Console.ReadLine();
                                        int catID = int.Parse(delInput);

                                        foreach (var item in deleteQuery)
                                        {
                                            if (item.CategoryId == catID)
                                            {
                                                isValidCategoryID = true;
                                            }
                                        }

                                        if(isValidCategoryID == true)
                                        {
                                            var deleteCategory = deleteCat.Categories.SingleOrDefault(p => p.CategoryId == catID);
                                            Console.WriteLine("Are you sure you want to delete " + deleteCategory.CategoryName + "? Y/N");

                                            if (Console.ReadLine().ToUpper().Equals("Y"))
                                            {
                                                Console.WriteLine("\nIf you delete " + deleteCategory.CategoryName + " you will delete all Products in this category");
                                                Console.WriteLine("Would you like to:");
                                                Console.WriteLine("1) Delete all Products within " + deleteCategory.CategoryName + "?");
                                                Console.WriteLine("2) Change the Category of all Products within " + deleteCategory.CategoryName + "?");
                                                var deleteChoice = Console.ReadLine();
                                                switch (deleteChoice)
                                                {
                                                    case "1":
                                                        {
                                                            var deleteProducts = deleteCat.Products.Where(p => p.CategoryID == catID);

                                                            List<Product> productArray = new List<Product>();

                                                            foreach (var p in deleteProducts)
                                                            {
                                                                productArray.Add(p);

                                                            }

                                                            foreach (var p in productArray)
                                                            {
                                                                deleteCat.DeleteProduct(p);
                                                            }

                                                            deleteCat.DeleteCategory(deleteCategory);
                                                            logger.Info("{0} successfully removed from Categories", deleteCategory.CategoryName);
                                                            break;
                                                        }
                                                    case "2":
                                                        {
                                                            bool isValid = false;
                                                            do
                                                            {
                                                                menu.CategoryHeader();
                                                                foreach (var category in deleteQuery)
                                                                {
                                                                    Console.WriteLine("{0}) {1}", category.CategoryId, category.CategoryName);
                                                                }
                                                                Console.WriteLine("\nChoose wish category you wish to replace " + deleteCategory.CategoryName + " with");
                                                                String catIDchoice = Console.ReadLine();
                                                                int newCatID = int.Parse(catIDchoice);
                                                                if (isValid == false)
                                                                {
                                                                    foreach (var item in deleteQuery)
                                                                    {
                                                                        if (item.CategoryId == newCatID && newCatID != catID)
                                                                        {
                                                                            isValid = true;
                                                                        }
                                                                    }
                                                                }

                                                                if (isValid == true)
                                                                {
                                                                    var changeProducts = deleteCat.Products.Where(p => p.CategoryID == catID);

                                                                    foreach (var p in changeProducts)
                                                                    {
                                                                        p.CategoryID = newCatID;
                                                                    }
                                                                    deleteCat.DeleteCategory(deleteCategory);
                                                                    logger.Info("{0} successfully removed from Categories", deleteCategory.CategoryName);
                                                                }
                                                                else
                                                                {
                                                                    logger.Info("Not a valid CategoryID");
                                                                }
                                                            } while (isValid == false);                                                            
                                                            break;
                                                        }
                                                }
                                            }
                                        }
                                    } while (isValidCategoryID == false);
                                    break;
                            }
                        } while (!catResp.Equals("7"));
                        break;
                }
            } while (!option.Equals("3"));
        }
    }
}
