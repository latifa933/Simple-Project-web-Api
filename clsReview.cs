using StoreDataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBisness
{
    public class clsReview
    {
     public enum enMode { addnew =1,update =2 }
     public enMode mode = enMode.addnew;
    public int ReviewID { get; set; }
    public int ProductID { get; set; }
    public int CustumorID { get; set; }
    public string ReviewText { get; set; }
    public byte Rating { get; set; }
    public DateTime ReviewDate { get; set; }
    
    public clsReview()
        {
            ReviewID = 0;
            ProductID = 0;
            CustumorID = 0;
            ReviewText = "";
            Rating = 0;
            ReviewDate = DateTime.Now;
            mode = enMode.addnew;
        }

    public clsReview(int reviewID,int productID,int custumorID,string reviewText,byte rating,DateTime reviewDate)
        {
            ReviewID = reviewID;
            ProductID = productID;
            ReviewText = reviewText;
            Rating = rating;
            ReviewDate = reviewDate;
            mode = enMode.update;
        }
    public static clsReview Find(int reviewID)
        {
            int productID = 0, custumor = 0;
            string reviText = "";
            byte rate = 0;
            DateTime reviDate = DateTime.Now;
            if (clsReviewData.GetReviewByID(reviewID, ref productID, ref custumor, ref reviText, ref rate, ref reviDate))
            {
                return new clsReview(reviewID, productID, custumor, reviText, rate, reviDate);
            }
            else
                return null;
        }
    public static clsReview Find(int custumorID,int productID)
        {
            int reviID = 0;
            string reviText = "";
            byte rate = 0;
            DateTime reviDate = DateTime.Now;

            if (clsReviewData.GetReviewByCustumorIDAndProductID(custumorID, productID, ref reviID, ref reviText, ref rate, ref reviDate))
                return new clsReview(reviID, productID, custumorID, reviText, rate, reviDate);
            else return null;

        }

    public static DataTable GetAllReview()
        {
            return clsReviewData.GetAllReview();
        }
    public static DataTable GetAllReviewforProduct(int productID)
        {
            return clsReviewData.GetAllReviewForProduct(productID);
        }
    private bool _AddNewReview()
        {
            ReviewID = clsReviewData.AddNewReview(ProductID, CustumorID, ReviewText, Rating, ReviewDate);
            return ReviewID != 0;
        }

     private bool _UpdateReview()
        {
            return clsReviewData.updateReview(ReviewID, ProductID, CustumorID, ReviewText, Rating, ReviewDate);
        }
    public bool Save()
        {
            switch(mode)
            {
                case enMode.addnew:
                    if (_AddNewReview())
                    {
                        mode = enMode.addnew;
                        return true;
                    }
                    else
                        return false;
                case enMode.update:
                    return _UpdateReview();

            }
            return false;

        }

    public static bool delete(int revID)
        {
            return clsReviewData.DeleteReview(revID);
        }
    public bool delete()
        {
            return clsReviewData.DeleteReview(ReviewID);
        }


    }
}
