using StoreDataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBisness
{
    public class clsProductCategory
    {
      public enum enMode { addnew =1,update =2}
      public enMode mode = enMode.addnew;
      public int categoryID { get; set; }
      public string categoryName { get; set; }
      public clsProductCategory()
        {
            categoryID = -1;
            categoryName = "";
            mode = enMode.addnew;

        }
        public clsProductCategory(int categID,string categoName)
        {
            categoryID = categID;
            categoryName = categoName;
            mode = enMode.update;
        }

        public static clsProductCategory Find(int categoID)
        {
            string name = "";
            if (clsProductCategoryData.GetProductCategoryByID(categoID, ref name))
                return new clsProductCategory(categoID, name);
            else return null;
        }
        public static clsProductCategory Find(string categoryName)
        {
            int Id = 0;
            if (clsProductCategoryData.GetProductCategoryByName(ref Id,categoryName))
                return new clsProductCategory(Id,categoryName);
            else return null;
        }

        private bool _addnewCategory()
        {
            categoryID = clsProductCategoryData.AddnewProductCategory(categoryName);
            return categoryID != -1;

        }
        private bool _updateCategory()
        {
            return clsProductCategoryData.UpdateProductCategory(categoryID, categoryName);
        }
        public bool save()
        {
            switch(mode)
            {
                case enMode.addnew:
                    if (_addnewCategory())
                    {
                        mode = enMode.update;
                        return true;
                    }
                    else return false;
                case enMode.update:
                    return _updateCategory();
            }
            return false;
        }
        public static bool delete(int categoryID)
        {
            return clsProductCategoryData.deleteCategory(categoryID);
        }
        public bool delete()
        {
            return clsProductCategoryData.deleteCategory(categoryID);
        }

        public static DataTable GetAllCategories()
        {
            return clsProductCategoryData.GetAllCategory();
        }

    }
}
