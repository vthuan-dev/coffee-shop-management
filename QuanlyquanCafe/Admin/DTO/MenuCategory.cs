using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanlyquanCafe.Admin.DTO
{
    public class MenuCategory
    {
        public MenuCategory(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public MenuCategory(DataRow row)
        {
            this.Id = (int)row["id"];
            this.Name = row["Name"].ToString();
        }
        private int id;
        private string name;
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
    }
}
