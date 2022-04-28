using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace ORMexer2
{
    class Program
    {
        static void Main(string[] args)
        {
            #region  Configuration
            var config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json")
                 .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            // writeline to check if using correct connection string was used
            Console.WriteLine(connString);
            #endregion
            IDbConnection conn = new MySqlConnection(connString);

            //create instance for DapperProductRepository class
            // use conn or name of instance used that stores MySqlConnection
            // place inside ()
            DapperProductRepository repo = new DapperProductRepository(conn);

            Console.WriteLine("Current listing of all Products.");
            var depos = repo.GetAllProducts();
            Print(depos);

            

            Console.WriteLine("Would you like to add a product?");

            string userInput = Console.ReadLine();

            while (userInput.ToLower() == "yes" || userInput.ToLower() == "y")
            {

                Console.WriteLine("Enter new products name:");
                string prodName = Console.ReadLine();
                Console.WriteLine("Enter a numeric value for products price.");
                double prodPrice = double.Parse(Console.ReadLine());
                Console.WriteLine("Enter a categoryID ranging from 1-10 for this product");
                int catID = int.Parse(Console.ReadLine());
                  Console.WriteLine("Is this product on sale?");
                  Console.WriteLine("Enter 0 for no and 1 for yes");
                  byte prodSale = byte.Parse(Console.ReadLine());
                Console.WriteLine("What is the quanity of this product in stock?");
                string qtyStock = Console.ReadLine();
                repo.CreateProduct(prodName, prodPrice, catID, prodSale, qtyStock);
                //display new list of departments
                Print(repo.GetAllProducts());
                // reset userInput and repompte for input             
                Console.WriteLine("Would you like to add another product?");
             
                userInput = Console.ReadLine();
            }// end while 

            Console.WriteLine("Would you like to update a product record?");
            //reset userInput
            userInput = Console.ReadLine();

            while (userInput.ToLower() == "yes" || userInput.ToLower() == "y")
            {
                var prodID = 0;
                //prompt for productID to use for updating a record
                Console.WriteLine("Enter the productID for product  wish to update.");
                prodID = int.Parse(Console.ReadLine());
                Console.WriteLine("Would you like to update a products name?");
                //reset userInput
                userInput = Console.ReadLine();
                if (userInput.ToLower() == "yes" || userInput.ToLower() == "y")
                {
                    //Console.WriteLine("Enter the productID for product name you wish to update.");
                     //prodID = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter new name for this product.");
                    userInput = Console.ReadLine();
                    // use input for 
                    repo.UpdateProductName(prodID, userInput);
                    //display new list of departments
                    //Print(repo.GetAllProducts());
                }//end if updateName

                Console.WriteLine("Would you like to update this products price?");
                //reset userInput
                userInput = Console.ReadLine();
                if (userInput.ToLower() == "yes" || userInput.ToLower() == "y")
                {
                    Console.WriteLine("Enter new price for this product.");
                    var priceInput = double.Parse(Console.ReadLine());
                    // use input for 
                    repo.UpdatePrice(prodID, priceInput);
                  
                } //end if updatePrice

                Console.WriteLine("Would you like to update this products category?");
                //reset userInput
                userInput = Console.ReadLine();
                if (userInput.ToLower() == "yes" || userInput.ToLower() == "y")
                {
                    Console.WriteLine("Enter a numeric value between 1-10 to add to a category.");
                    var catInput = int.Parse(Console.ReadLine());
                    // use input for 
                    repo.UpdateCategory(prodID, catInput);
                    
                } //end if updateCategory

                Console.WriteLine("Would you like to update this products sale status?");
                //reset userInput
                userInput = Console.ReadLine();
                if (userInput.ToLower() == "yes" || userInput.ToLower() == "y")
                {
                    Console.WriteLine("Entering 0 will take product off sale. Entering 1 will put product on sale.");
                    Console.WriteLine("Please enter 0 or 1 now.");
                    var saleInput = byte.Parse(Console.ReadLine());
                    // use input for 
                    repo.UpdateSale(prodID, saleInput);

                } //end if updateSale

                Console.WriteLine("Would you like to update this products stocklevel?");
                //reset userInput
                userInput = Console.ReadLine();
                if (userInput.ToLower() == "yes" || userInput.ToLower() == "y")
                {
                    Console.WriteLine("Enter the new stocklevel for this product.");
                    var stockInput = Console.ReadLine();
                    // use input for 
                    repo.UpdateProductStock(prodID, stockInput);

                } //end if updateStock
                //display all records including updated records
                Console.WriteLine("List of all records including updates.");
               Print(repo.GetAllProducts());
                Console.WriteLine("\n\nWould you like to update another record?");
                // reset userInput and repompte for input
                userInput = Console.ReadLine();
            }// end while

            Console.WriteLine("World you like to delete a record?");
            userInput = Console.ReadLine();

            if (userInput.ToLower() == "yes" || userInput.ToLower() == "y")
            {
               
                Console.WriteLine("Enter the productID for record you wish to delete:");
               var prodID = int.Parse(Console.ReadLine());
                repo.DeleteProduct(prodID);
            }
            Print(repo.GetAllProducts());
            Console.WriteLine("Thank you for stopping bye!!");

        }

        private static void Print(IEnumerable<Product> depos)
        {
            //Foreach to display colums of database 
            // use console.writeline ($" ")
            foreach (var depo in depos)
            {
                // use below writeline to display every field for conntected database
                Console.WriteLine($"ProdID: {depo.ProductID} Name: {depo.Name} Price: {depo.Price} CatID: {depo.CategoryID} OnSale: {depo.OnSale} StockLevel: {depo.StockLevel}");


            }//end foreach
        }// end print method
               
    }
}
