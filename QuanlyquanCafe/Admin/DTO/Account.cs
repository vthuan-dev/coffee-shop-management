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
        public Account(int id, string password, string role, string fullName, string email, string phone)
        {
            this.Id = id;
            this.Password = password;
            this.Role = role;
            this.FullName = fullName;
            this.Phone = phone;
            this.Email = email;
            
        }

        public Account(DataRow row)
        {
            this.Id = (int)row["uid"];
            this.Password = row["Password"].ToString();
            this.Role = row["Role"].ToString();
            this.FullName = row["FullName"].ToString();
            this.Phone = row["Phone"].ToString();
            this.Email = row["Email"].ToString();
            
        }

        private int id;
        private string password;
        private string role;
        private string fullName;
        private string phone;
        private string email;
        

        public int Id { get => id; set => id = value; }
        public string Password { get => password; set => password = value; }
        public string Role { get => role; set => role = value; }
        public string FullName { get => fullName; set => fullName = value; }
        
        public string Phone { get => phone; set => phone = value; }

        public string Email { get => email; set => email = value; }
    }
}
