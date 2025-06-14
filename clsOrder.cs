using StoreDataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBisness
{
    public class clsOrder
    { public enum enmode { addnew = 1, update = 2 }
        public enmode mode = enmode.addnew;
        public enum enStatus { Pending = 1, processing = 2, Shipped = 3, Delivered = 4, Cancelled = 5, Refunded = 6 }
        public enStatus Status { get; set; }
        public string StatusText()
        {
            switch(Status)
            {
                case enStatus.Pending:
                    return "Pending";
                case enStatus.processing:
                    return "Processing";
                case enStatus.Shipped:
                    return "Shipped";
                case enStatus.Delivered:
                    return "Delivered";
                case enStatus.Cancelled:
                    return "Cancelled";
                case enStatus.Refunded:
                    return "Refunded";
                default:
                    return "";

            }
        }
        
        public int OrderID { get; set; }
        public int CustumorID { get; set; }
        public DateTime OrderDate { get; set; }
        public float TotalAmount { get; set; }
        public clsCustumor CustumorInfo { get; set; }

        public clsOrder()
        {
            OrderID = 0;
            CustumorID = -1;
            OrderDate = DateTime.Now;
            TotalAmount = 0;
            Status = enStatus.Pending;
            mode = enmode.addnew;
        }
        public clsOrder(int orderID,int custumorID,DateTime orderdate,float totalamount,enStatus status)
        {
            OrderID = orderID;
            CustumorID = custumorID;
            OrderDate = orderdate;
            TotalAmount = totalamount;
            Status = status;
            mode = enmode.update;
            CustumorInfo = clsCustumor.Find(custumorID);
        }
        
        private bool _AddNewOrder()
        {
            OrderID = clsOrderData.AddNewOrder(CustumorID, OrderDate, TotalAmount, (byte)Status);
            return OrderID != 0;
        }
        private bool _UpdateOrder()
        {
            return clsOrderData.UpdateOrder(OrderID, CustumorID, OrderDate, TotalAmount, (byte)Status);
        }
        
        public bool save()
        { switch(mode)
            {
                case enmode.addnew:
                    if (_AddNewOrder())
                    {
                        mode = enmode.update;
                        return true;
                    }
                    else
                        return false;
                case enmode.update:
                    return _UpdateOrder();

            }
            return false;
        }

        public static clsOrder FindByID(int orderID)
        {
            int custumorid = 0;DateTime orderdate = DateTime.Now;float amount = 0; byte status = 0;
            if (clsOrderData.GetOrderByID(orderID, ref custumorid, ref orderdate, ref amount, ref status))
                return new clsOrder(orderID, custumorid, orderdate, amount, (enStatus)status);
            else return null;
        }
        public static clsOrder FindbyCustumorID(int custumorID)
        {
            int orderID = -1;DateTime orderdate = DateTime.Now;float amount = 0;byte status = 0;
            if (clsOrderData.GetOrderByCustumorID(custumorID, ref orderID, ref orderdate, ref amount, ref status))
                return new clsOrder(orderID, custumorID, orderdate, amount, (enStatus)status);
            else return null;

        }

        public static bool delete(int orderID)
        {
            return clsOrderData.DeleteOrder(orderID);

        }
        public bool delete()
        {
            return clsOrderData.DeleteOrder(OrderID);
        }
        public int? OrderforCustumor(int custumorID)
        {
            return clsOrderData.GetOrderForCustumorID(custumorID);
        }
        public static DataTable getAllOrders()
        {
            return clsOrderData.GetAllOrders();
        }
        public bool setPending()
        {
         return   clsOrderData.UpdateStatus(OrderID, 1);
        }
        public bool setProcessing()
        {
            return clsOrderData.UpdateStatus(OrderID, 2);
        }
        public bool setShipped()
        {
            return clsOrderData.UpdateStatus(OrderID, 3);
        }
        public bool setDelivered()
        {
            return clsOrderData.UpdateStatus(OrderID, 4);
        }
        public bool setCancelled()
        {
            return clsOrderData.UpdateStatus(OrderID, 5);
        }
        public bool setRefunded()
        {
            return clsOrderData.UpdateStatus(OrderID, 6);
        }
        public bool changeStatus(enStatus status)
        {
            return clsOrderData.UpdateStatus(OrderID, (byte)status);
        }
        



    }
}









    

