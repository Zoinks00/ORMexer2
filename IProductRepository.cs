using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMexer2
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
       
        
        
        //create method
        void CreateProduct(string name, double price, int categoryID, byte onSale, string stockLevel);
            
        //delete method
        void DeleteProduct(int productID);
        //below methods will update a different columns field
        void UpdateProductName(int productID, string updateName);
        void UpdatePrice(int productID, double updatePrice);
        void UpdateCategory(int productID, int updateCatID);
        void UpdateSale(int productID, byte updateSale);
        void UpdateProductStock(int productID, string updateStock);
           
    }
}
