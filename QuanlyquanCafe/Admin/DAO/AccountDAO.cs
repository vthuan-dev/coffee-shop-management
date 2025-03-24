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
        public string Login(string email, string password)
        {
            string query = "SELECT Role FROM Users WHERE Email = @email AND Password = @password";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { email, password });
            
            if (result.Rows.Count > 0)
            {
                return result.Rows[0]["Role"].ToString();
            }
            return null;
        }

        public List<Account> GetListAccount()
        {
            List<Account> listAcc = new List<Account>();
            string query = "SELECT * FROM Users";
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

        public bool AddNewItem(string Role, string FullName, string Phone, string Email)
        {
            string defaultPassword = "1";  // Đặt mật khẩu mặc định
            string query = "Insert into Users (Password, Role, FullName, Phone, Email) Values (@Password, @Role, @FullName, @Phone, @Email)";

            // Khai báo tham số
            object[] parameters = new object[] { defaultPassword, Role, FullName, Phone, Email };

            // Gọi ExecuteNonQuery để thực thi câu lệnh SQL
            int result = DataProvider.Instance.ExecuteNonQuery(query, parameters);

            return result > 0;
        }


        public bool UpdateItem(int id, string Role, string FullName, string Phone, string Email)
        {
            string query = "UPDATE Users SET Role = @Role, FullName = @FullName, Phone = @Phone, Email = @Email WHERE uid = @id";
            object[] parameters = { Role, FullName, Phone, Email, id };
            int result = DataProvider.Instance.ExecuteNonQuery(query, parameters);

            return result > 0;
        }

        public bool DeleteItem(int id)
        {
            string query = string.Format("Delete from Users where uid = {0}", id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public List<Account> SearchEmployeeByName(string name)
        {
            List<Account> listEmployee = new List<Account>();

            // Câu truy vấn SQL tìm kiếm nhân viên theo tên
            string query = string.Format(
                "SELECT * FROM Users WHERE FullName LIKE N'%{0}%'",
                name);

            // Thực thi câu truy vấn và lấy dữ liệu
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            // Duyệt qua từng dòng kết quả và thêm vào danh sách nhân viên
            foreach (DataRow dr in data.Rows)
            {
                Account emp = new Account(dr); 
                listEmployee.Add(emp);
            }

            return listEmployee;
        }

        public bool Register(string password, string role, string fullName, string phone, string email)
        {
            string query = "INSERT INTO Users (Password, Role, FullName, Phone, Email) VALUES (@password, @role, @fullName, @phone, @email)";
            object[] parameters = new object[] { password, role, fullName, phone, email };
            int result = DataProvider.Instance.ExecuteNonQuery(query, parameters);
            return result > 0;
        }

        public bool IsEmailOrPhoneExists(string email, string phone)
        {
            string query = "SELECT COUNT(*) FROM Users WHERE Email = @email OR Phone = @phone";
            object[] parameters = new object[] { email, phone };
            int count = Convert.ToInt32(DataProvider.Instance.ExecuteScalar(query, parameters));
            return count > 0;
        }

    }
}
