using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanlyquanCafe.Admin.DTO
{
    public class MenuDTO
    {
        public MenuDTO(int id, string name, float price, string category, int categoryID)
        {
            this.id = id;
            this.Name = name;
            this.Price = price;
            this.Category = category;
            this.categoryID = categoryID;
            this.CategoryID = categoryID;
        }

        public MenuDTO(DataRow row) {
            this.id = (int)row["id"];
            this.Name = row["Tên"].ToString();
            this.Price = (float)Convert.ToDouble(row["Giá"].ToString());
            this.Category = row["Loại"].ToString();
            this.CategoryID = (int)row["CategoryID"];
        }
        private int id;
        private string name;
        private float price;
        private string category;
        private int categoryID;

        public string Name { get => name; set => name = value; }
        public float Price { get => price; set => price = value; }
        public string Category { get => category; set => category = value; }
        public int Id { get => id; set => id = value; }
        public int CategoryID { get => categoryID; set => categoryID = value; }
    }
}
