using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Security.Cryptography;

namespace cms_app.Models
{
    public class LoginModalData
    {
        public string userId { get; set; }
        public string userCode { get; set; }
        public string userRole { get; set; }
        public string CompanyCode { get; set; }
    }


    public class LoginModal
    {
        public DataTable LoginCredential(String userName, String password)
        {
            DataTable dsData = new DataTable();
            ExecuteOperation op = new ExecuteOperation();
            dsData = op.checkUserLogin(userName, password);
            return dsData;
        }

        public static string GenerateMD5(string plaintext)
        {
            MD5 shaM;
            try
            {
                System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
                byte[] data = ue.GetBytes(plaintext);
                shaM = MD5.Create();
                byte[] result = shaM.ComputeHash(data);
                return Convert.ToBase64String(result);
            }
            finally
            {
                shaM = null;

            }
        }
        
    }
}