using StoreDataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StoreBisness
{
    public class clsProductCatalog
    { 
        public enum enMode { addnew =1,update =2};
        public enMode mode = enMode.addnew;
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string description { get; set; }
        public float Price { get; set; }
        public int QuantityInStock { get; set; }
        public int CategoryID { get; set; }
        public clsProductCategory categoryInfo { get; set; }

        public clsProductCatalog()
        {
            ProductID = 0;
            ProductName = "";
            description = "";
            Price = 0;
            QuantityInStock = 0;
            CategoryID = 0;

            mode = enMode.addnew;

        }
        public clsProductCatalog(int productID,string productName,string descriptin,float price,int quantityInStock,int categoryID)
        {
            ProductID = productID;
            ProductName = productName;
            description = descriptin;
            Price = price;
            QuantityInStock = quantityInStock;
            CategoryID = categoryID;
            categoryInfo = clsProductCategory.Find(categoryID);
            mode = enMode.update;
        }
        private bool _AddNewProductCatalog()
        {
            ProductID =  clsProductCatalogData.AddNewProduct(ProductName, description, Price, QuantityInStock, CategoryID);
            return ProductID != 0;
        }
        private bool _UpdateProductCatalog()
        {
            return clsProductCatalogData.UpdateProduct(ProductID, ProductName, description, Price, QuantityInStock, CategoryID);
        }
        public bool save()
        {
            switch(mode)
            {
                case enMode.addnew:
                    if (_AddNewProductCatalog())
                    {
                        mode = enMode.update;
                        return true;
                    }
                    else return false;
                case enMode.update:
                    return _UpdateProductCatalog();


            }
            return false;


        }

        public static clsProductCatalog FindProductByID(int productID)
        {
            string productname = "", descri = "";
            float price = 0;
            int quatity = 0, category = 0;
            if (clsProductCatalogData.GetProductInfoByID(productID, ref productname, ref descri, ref price, ref quatity, ref category))
                return new clsProductCatalog(productID, productname, descri, price, quatity, category);
            else return null;
        }
        public static clsProductCatalog FindProductByName(string name)
        {
            int prID = 0; string descri = "";
            float price = 0;
            int quatity = 0, category = 0;
            if (clsProductCatalogData.GetProductInfoByName(name, ref prID, ref descri, ref price, ref quatity, ref category))
                return new clsProductCatalog(prID,name, descri, price, quatity, category);
            else return null;
        }

        public bool deleteProduct()
        {
            return clsProductCatalogData.deleteProduct(ProductID);
        }
        public static bool deleteProduct(int productID)
        {
            return clsProductCatalogData.deleteProduct(productID);
        }
        public DataTable GetAllProductByCategory()
        {
            return clsProductCatalogData.GetAllProductByCategory(CategoryID);
        }
        public static DataTable GetAllProductbyCategory(int categoryID)
        {
            return clsProductCatalogData.GetAllProductByCategory(categoryID);
        }
        public static bool IsProductExistByName(string productname)
        {
            return clsProductCatalogData.IsProductExistByName(productname);
        }

    }
}
