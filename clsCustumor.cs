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
    public class clsCustumor
    {   public enum enMode { addnew =0,upadte =1};
        public enMode mode = enMode.addnew;
        public int CustumorID { get; set; }
        public string Name { get; set; }
        public string Email  { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string password { get; set; }

        public clsCustumor()
        {
            this.CustumorID = 0;
            this.Name = "";
            this.Email = "";
            this.Phone = "";
            this.Address = "";
            this.Username = "";
            this.password = "";
            this.mode = enMode.addnew;

        }
        public clsCustumor(int custumorID,string Name,string Email,string Phone,string Address,string username,string password)
        {
            this.CustumorID = custumorID;
            this.Name = Name;
            this.Email = Email;
            this.Phone = Phone;
            this.Address = Address;
            this.Username = username;
            this.password = password;
            this.mode = enMode.upadte;

        }
        public static clsCustumor Find(int custumorID)
        {
            string name = "", email = "", phone = "", addres = "", username = "", password = "";
            if (clsCustomorData.GetCustumorByID(custumorID, ref name, ref email, ref phone, ref addres, ref username, ref password))
                return new clsCustumor(custumorID,name,email,phone,addres,username,password);
            else
                return null;
           
        }
        public static clsCustumor Find(string username,string password)
        {
            string name = "", email = "", phone = "", adress = "";
            int custumorID = -1;
            if (clsCustomorData.GetCustumorByUsernameAndPassword(username, password, ref custumorID, ref name, ref email, ref phone, ref adress))
                return new clsCustumor(custumorID, name, email, phone, adress, username, password);
            else return null;

        }
        private bool _AddnewCustumor()
        {
            this.CustumorID = clsCustomorData.AddNewCustumor(Name, Email, Phone, Address, Username, password);
            return CustumorID != 0;
        }
        private bool _UpdateCustumor()
        {
            return clsCustomorData.UpdateCustumor(CustumorID, Name, Email, Phone, Address, Username, password);
        }
        public bool Save()
        {   switch(mode)
            {  case enMode.addnew:
                    if (_AddnewCustumor())
                    {
                        mode = enMode.upadte;
                        return true;
                    }
                    else return false;
                case enMode.upadte:
                    return _UpdateCustumor();
            }
            return false;
            
        }

        public DataTable GetAllCustumor()
        {
            return clsCustomorData.GetAllCustumor();
        }
        public bool deleteCustumor(int custumorID)
        {
            return clsCustomorData.DeleteCustumor(custumorID);
        }
        public static bool IsCustumorExistByUsername(string username)
        {
           return clsCustomorData.IsCustumorExistByUsername(username);
        }

    }
}
