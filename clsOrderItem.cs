using StoreDataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBisness
{
    public class clsOrderItem
    {
        public enum enMode { addnew=1,update =2};
        public enMode mode = enMode.addnew;
        public int? OrderID { get; set; }
        public int? ProductID { get; set; }
        public int? Quantity { get; set; }
        public float Price { get; set; }

        public clsOrderItem()
        {
            OrderID = null;
            ProductID = null;
            Quantity = null;
            Price = 0;
            mode = enMode.addnew;
        }
        public clsOrderItem(int orderID,int productID,int quantity,float price )
        {
            OrderID = orderID;
            ProductID = productID;
            Quantity = quantity;
            Price = price;
            mode = enMode.update;
        }

        public static clsOrderItem Find(int orderID,int productID)
        {
            int qua = 0;
            float prix = 0;
            if (clsOrderItemData.GetItemByOrderAndProduct(orderID, productID, ref qua, ref prix))
            {
                return new clsOrderItem(orderID, productID, qua, prix);
            }
            else
                return null;
        }
        private bool _AddNewOrderItem()
        {
            return clsOrderItemData.AddNewOrderItem(OrderID, ProductID, Quantity, Price);
        }
        private bool _UpdateOrderItem()
        {
            return clsOrderItemData.UpdateOrderItem(OrderID, ProductID, Quantity, Price);
        }
        public bool save()
        {
            switch(mode)
            {
                case enMode.addnew:
                    if (_AddNewOrderItem())
                    {
                        mode = enMode.update;
                        return true;
                    }
                    else
                        return false;
                case enMode.update:
                    return _UpdateOrderItem();


            }
            return false;


        }
        public bool deleteOrderItem()
        {
            return clsOrderItemData.deleteOrderItem(OrderID, ProductID);
        }
        public static bool deleteOrderItem(int orderID,int productID)
        {
            return clsOrderItemData.deleteOrderItem(orderID, productID);
        }
        public static DataTable GetAllOrderItemForOrder(int orderID)
        {
            return clsOrderItemData.GetAllOrderItemForOrder(orderID);
        }
        public DataTable GetAllOrderItemForThisOrder()
        {
            return clsOrderItemData.GetAllOrderItemForOrder(OrderID);
        }

    }
}
