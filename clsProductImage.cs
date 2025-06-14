using StoreDataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBisness
{
    public class clsProductImage
    {
        public enum enmode { addnew =1,update =2}
        public enmode mode = enmode.addnew;
        public int imageID { get; set; }
        public string ImageURL { get; set; }
        public int productID { get; set; }
        public byte ImageOrder { get; set; }
         public clsProductImage()
        {
            imageID = 0;
            ImageURL = "";
            productID = 0;
            ImageOrder = 0;
            mode = enmode.addnew;
        }
        public clsProductImage(int imgID,string imgurl,int prductid,byte imagorder)
        {
            imageID = imgID;
            ImageURL = imgurl;
            productID = prductid;
            ImageOrder = imagorder;
            mode = enmode.update;

        }
        public static clsProductImage Find(int imgID)
        {
            string URL = "";
            int prductid = 0;
            byte imagOrder = 0;
            if (clsProductImageData.GetProductImageByID(imgID, ref URL, ref prductid, ref imagOrder))
                return new clsProductImage(imgID, URL, prductid, imagOrder);
            else return null;
        }
        public static clsProductImage FindbyProductID(int proID,byte imgOrder)
        {
            string URL = "";
            int imdID = 0;
            
            if (clsProductImageData.GetProductImageByProductID(proID,ref imdID,ref URL,imgOrder))
                return new clsProductImage(imdID, URL,proID,imgOrder);
            else return null;
        }


        public static DataTable GetAllImageForProduct(int produID)
        {
            return clsProductImageData.getAllImageforProduct(produID);
        }
        private bool _AddnewImage()
        {
            imageID = clsProductImageData.addNewImage(ImageURL, productID, ImageOrder);
            return imageID != 0;
        }

        private bool _UpdateImage()
        {
            return clsProductImageData.UpdateImage(imageID, ImageURL, productID, ImageOrder);
        }
        public bool save()
        {
            switch(mode)
            {
                case enmode.addnew:
                    if (_AddnewImage())
                    {
                        mode = enmode.update;
                        return true;
                    }
                    else
                        return false;
                case enmode.update:
                    return _UpdateImage();
            }
            return false;
        }

        public static bool  deleteImage(int imagid)
        {
            return clsProductImageData.deleteImage(imagid);
        }
       public bool delete()
        {
            return clsProductImageData.deleteImage(imageID);
        }



    }
}
