using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanlyquanCafe.Admin.DTO
{
    public class Account
    {
        public Account(int id, string userName, string phone, string fullName, string email, string role)
        {
            this.Id = id;
            this.UserName = userName;
            //this.PassWord = passWord;
            this.FullName = fullName;
            this.Email = email;
            this.Role = role;
        }

        public Account(DataRow row)
        {
            this.Id = (int)row["uid"];
            this.UserName = row["UserName"].ToString();
            //this.PassWord = row["passWord"].ToString();
            this.Phone = row["Phone"].ToString();
            this.FullName = row["FullName"].ToString();
            this.Email = row["Email"].ToString();
            this.Role = row["Role"].ToString();
        }
        private int id;
        private string role;
        private string email;
        private string phone;
        private string fullName;
        //private string passWord;
        private string userName;

        public int Id { get => id; set => id = value; }
        public string UserName { get => userName; set => userName = value; }
        //public string PassWord { get => passWord; set => passWord = value; }
        public string FullName { get => fullName; set => fullName = value; }
        public string Email { get => email; set => email = value; }
        public string Role { get => role; set => role = value; }
        
        public string Phone { get => phone; set => phone = value; }
    }
}
