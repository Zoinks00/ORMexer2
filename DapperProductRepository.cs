using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;

namespace ORMexer2
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;

        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            //Using Dapper; added at top access to .Query<t>
            //() holds SQL statment of what to do
            var depos = _connection.Query<Product>("SELECT * FROM products");

            //return values of database using depos variable
            return depos;
        }
      
        //@ before prodName will turn prodName into a usable variable for SQL

        public void CreateProduct(string name, double price, int categoryID, byte onSale, string stockLevel)
        {
            _connection.Execute("INSERT INTO products (Name, Price, CategoryID, onSale, StockLevel) VALUES (@prodName, @prodPrice, @categoryID,@prodOnSale, @prodStockLevel);",
           new { prodName = name, prodPrice = price, categoryID = categoryID, prodOnSale = onSale, prodStockLevel = stockLevel });
        }

        public void DeleteProduct(int productID)
        {  //delete method must include all tables that referance record you are trying to remove
           //from a database
            _connection.Execute("DELETE FROM products WHERE ProductID = @productID;",
                new { productID = productID });

            _connection.Execute("DELETE FROM sales WHERE ProductID = @productID;",
                new { productID = productID });

            _connection.Execute("DELETE FROM reviews WHERE ProductID = @productID;",
                new { productID = productID });
        }


        public void UpdateProductName(int productID, string updateName)
        {
            _connection.Execute("UPDATE products SET Name = @updateNAME WHERE ProductID = @productID;",
                new { updateName = updateName, productID = productID });
        }
        public void UpdatePrice(int productID, double updatePrice)
        {
            _connection.Execute("UPDATE products SET Price = @updatePrice WHERE ProductID = @productID;",
                new { updatePrice = updatePrice, productID = productID });
        }
        public void UpdateCategory(int productID, int updateCatID)
        {
            _connection.Execute("UPDATE products SET CategoryID = @updateCatID WHERE ProductID = @productID;",
                new { updateCatID = updateCatID, productID = productID });
        }
        public void UpdateSale(int productID, byte updateSale)
        {
            _connection.Execute("UPDATE products SET onSale = @updateSale WHERE ProductID = @productID;",
                new { updateSale = updateSale, productID = productID });
        }
        public void UpdateProductStock(int productID, string updateStock)
        {
            _connection.Execute("UPDATE products SET StockLevel = @updateStock WHERE ProductID = @productID;",
                new { updateStock = updateStock, productID = productID });
        }
                
    }
}
