using StoreDataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBisness
{
    public class clsShipping
    { public enum enmode { addnew =1,update =2}
      public enmode mode = enmode.addnew;
      public enum enShippingStatus {processing =1,OutForDelivry =2,Delivered =3,ReturnToSender =4,OnHold =5,Delayed =6,Lost =7}
      public enShippingStatus Status { get; set; }
      public string StatusText()
        { switch(Status)
            {
                case enShippingStatus.processing:
                    return "Processing";
                case enShippingStatus.OutForDelivry:
                    return "Out For Delivery";
                case enShippingStatus.Delivered:
                    return "Delivered";
                case enShippingStatus.ReturnToSender:
                    return "Return To Sender";
                case enShippingStatus.OnHold:
                    return "On Hold";
                case enShippingStatus.Delayed:
                    return "Delayed";
                case enShippingStatus.Lost:
                    return "Lost";
                default:
                    return "";

                }

        }
      public int ShippingID { get; set; }
      public int OrderID { get; set; }
      public string CarrierName { get; set; }
      public string TrackingNumber { get; set; }
      public DateTime EstimatedDelivryDate { get; set; }
      public DateTime? ActualDelivryDate { get; set; }
      public clsShipping()
        {
            ShippingID = 0;
            OrderID = 0;
            CarrierName = "";
            TrackingNumber = "";
            Status = enShippingStatus.processing;
            EstimatedDelivryDate = DateTime.Now;
            ActualDelivryDate = DateTime.Now;
            mode = enmode.addnew;
        }
     public clsShipping(int shippingID,int orderID,string carrierName,string trackingnumber,enShippingStatus status,DateTime estimdelivDate,DateTime? ActuDelivDate)
        {
            ShippingID = shippingID;
            OrderID = orderID;
            CarrierName = carrierName;
            TrackingNumber = trackingnumber;
            Status = status;
            EstimatedDelivryDate = estimdelivDate;
            ActualDelivryDate = ActuDelivDate;

            mode = enmode.update;
        }
        public static clsShipping FindByID(int shippingID)
        {
            int orderiD = 0;
            string carriNme = "", Tracknum = "";
            byte statu = 0;
            DateTime estim = DateTime.Now;
            DateTime? actual = null;
            if (clsShippingData.GetShippingByID(shippingID, ref orderiD, ref carriNme, ref Tracknum, ref statu, ref estim, ref actual))
                return new clsShipping(shippingID, orderiD, carriNme, Tracknum, (enShippingStatus)statu, estim, actual);
            else
                return null;

        }
        public static clsShipping FindByorderID(int orderID)
        {
            int shippingID = 0;
            string carriNme = "", Tracknum = "";
            byte statu = 0;
            DateTime estim = DateTime.Now;
            DateTime? actual = null;
            if (clsShippingData.GetShippingByOrderID(orderID, ref shippingID, ref carriNme, ref Tracknum,ref statu, ref estim, ref actual))
                return new clsShipping(shippingID,orderID, carriNme, Tracknum, (enShippingStatus)statu, estim, actual);
            else
                return null;


        }



        public static DataTable GetAllShipping()
        {
            return clsShippingData.GetAllShippings();
        }

      private bool _AddNewShipping()
        {
            ShippingID = clsShippingData.AddnewShipping(OrderID, CarrierName, TrackingNumber, (byte)Status, EstimatedDelivryDate, ActualDelivryDate);
            return ShippingID != 0;
        }
      private bool _UpdateShipping()
        {
            return clsShippingData.updateShipping(ShippingID, OrderID, CarrierName, TrackingNumber, (byte)Status, EstimatedDelivryDate, ActualDelivryDate);
        }
      public bool deleteSgipping()
        {
            return clsShippingData.deleteShipping(ShippingID);
        }
      public static bool DeleteShipping(int shipping)
        {
            return clsShippingData.deleteShipping(shipping);
        }
      public bool save()
        {
            switch(mode)
            {
                case enmode.addnew:
                    if (_AddNewShipping())
                    {
                        mode = enmode.update;
                            return true;
                    }
                    else
                        return false;
                case enmode.update:
                    return _UpdateShipping();

            }

            return false;

        }


    }
}
