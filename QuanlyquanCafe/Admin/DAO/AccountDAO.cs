using QuanlyquanCafe.Admin.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

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
        public string Login(string email, string password)
        {
            string query = "SELECT Role FROM Users WHERE Email = @email AND Password = @password";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { email, password });

            if (result.Rows.Count > 0)
            {
                return result.Rows[0]["Role"].ToString(); // Trả về vai trò của user (Admin, Waiter, ...)
            }

            return null; // Nếu không tìm thấy user, trả về null
        }
        public bool IsEmailOrPhoneExists(string email, string phone)
        {
            string checkQuery = "SELECT COUNT(*) FROM Users WHERE Email = @email OR Phone = @phone";
            DataTable checkResult = DataProvider.Instance.ExecuteQuery(checkQuery, new object[] { email, phone });

            return Convert.ToInt32(checkResult.Rows[0][0]) > 0; // Trả về true nếu đã tồn tại
        }
        public bool Register(string password, string role, string fullName, string phone, string email)
        {
            string insertQuery = "INSERT INTO Users ( Password,Role,FullName,Phone,Email ) VALUES ( @password , @role , @fullName , @phone , @email )";
            int rowsAffected = DataProvider.Instance.ExecuteNonQuery(insertQuery,
            new object[] { password, role, fullName, phone, email }); // Sửa thứ tự

            return rowsAffected > 0;
        }






        public List<Account> GetListAccount()
        {
            List<Account> listAcc = new List<Account>();
            string query = "SELECT uid, FullName, Role, Phone, Email FROM Users";
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
