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
        public Account(int id, string userName, string passWord, string fullName, string email, string role)
        {
            this.Id = id;
            this.UserName = userName;
            this.PassWord = passWord;
            this.FullName = fullName;
            this.Email = email;
            this.Role = role;
        }

        public Account(DataRow row)
        {
            this.Id = (int)row["id"];
            this.UserName = row["userName"].ToString();
            this.PassWord = row["passWord"].ToString();
            this.FullName = row["fullName"].ToString();
            this.Email = row["email"].ToString();
            this.Role = row["role"].ToString();
        }
        private int id;
        private string role;
        private string email;
        private string fullName;
        private string passWord;
        private string userName;

        public string UserName { get => userName; set => userName = value; }
        public string PassWord { get => passWord; set => passWord = value; }
        public string FullName { get => fullName; set => fullName = value; }
        public string Email { get => email; set => email = value; }
        public string Role { get => role; set => role = value; }
        public int Id { get => id; set => id = value; }
    }
}
