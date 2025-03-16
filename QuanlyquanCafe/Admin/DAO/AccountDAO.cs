using QuanlyquanCafe.Admin.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanlyquanCafe.Admin.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;
        public static AccountDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new AccountDAO();
                return AccountDAO.instance;
            }
            private set
            {
                AccountDAO.instance = value;
            }
        }
        private AccountDAO() { }
        public bool Login(string email, string passWord)
        {
            string query = "Users_Login @email , @passWord";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] {email, passWord});
            return result.Rows.Count > 0;
        }

        public List<Account> GetListAccount()
        {
            List<Account> listAcc = new List<Account>();
            string query = "SELECT uid, UserName, FullName, Role, Phone, Email FROM Users";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow dr in data.Rows)
            {
                Account acc = new Account(dr);
                listAcc.Add(acc);
            }
            return listAcc;
        }

        public string GetRoleByID(int id)
        {
            string query = "SELECT Role FROM dbo.Users WHERE uid = " + id;
            object result = DataProvider.Instance.ExecuteScalar(query);
            if (result != null)
            {
                return result.ToString();
            }
            else
            {
                // Handle the case when no role is found
                return "No role found";
            }
        }
    }  
}
