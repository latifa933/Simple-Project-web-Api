using StoreDataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBisness
{
    public class clsPayment
    {
      public enum enMode { addnew =1,update =2};
      public enMode mode = enMode.addnew;
      
        public int PaymentID { get; set; }
        public int OrderID { get; set; }
        public float Amount { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime TransactionDate { get; set; }

        public clsPayment()
        {
            PaymentID = 0;
            OrderID = 0;
            Amount = 0;
            PaymentMethod = "";
            TransactionDate = DateTime.Now;
            mode = enMode.addnew;

        }
        public clsPayment(int paymentID,int orderID,float amount,string paymentmethod,DateTime transactiondate)
        {
            PaymentID = paymentID;
            OrderID = orderID;
            Amount = amount;
            PaymentMethod = paymentmethod;
            TransactionDate = transactiondate;
            mode = enMode.update;

        }
        private bool _AddNewPayment()
        {
            PaymentID = clsPaymentData.AddNewPayment(OrderID, Amount, PaymentMethod, TransactionDate);
            return PaymentID != 0;

        }
        private bool _UpdatePayment()
        {
            return clsPaymentData.UpdatePayment(PaymentID, OrderID, Amount, PaymentMethod, TransactionDate);

        }
        public static clsPayment FindByID(int paymentID)
        {
            int orderID = -1;
            float amount = 0;
            string paymentmethod = "";
            DateTime transactionDate = DateTime.Now;
            if (clsPaymentData.GetPaymentByID(paymentID, ref orderID, ref amount, ref paymentmethod, ref transactionDate))
                return new clsPayment(paymentID, orderID, amount, paymentmethod, transactionDate);
            else return null;
        }
        public static clsPayment FindByOrderID(int orderID)
        {
            int paymentID = -1;
            float amount = 0;
            string paymentmethod = "";
            DateTime transactiondate = DateTime.Now;
            if (clsPaymentData.GetPaymentByOrderID(orderID, ref paymentID, ref amount, ref paymentmethod, ref transactiondate))
                return new clsPayment(paymentID, orderID, amount, paymentmethod, transactiondate);

            else
                return null;

        }
        public bool save()
        {
            switch(mode)
            {
                case enMode.addnew:
                    if (_AddNewPayment())
                    {
                        mode = enMode.update;
                        return true;
                    }
                    else
                        return false;
                case enMode.update:
                    return _UpdatePayment();

            }
            return false;
        }
        public bool deletePayment()
        {
            return clsPaymentData.deletePayment(PaymentID);
        }
        public static bool deletePayment(int paymentID)
        {
            return clsPaymentData.deletePayment(paymentID);
        }
        public static DataTable GetAllPaymentForCustumor(int custumorID)
        {
            return clsPaymentData.GetAllPaymentForCustumor(custumorID);
        }

    }
}
