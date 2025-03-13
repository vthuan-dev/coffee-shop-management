using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanlyquanCafe.Admin.DTO
{
    public class FacilityCategory
    {
        public FacilityCategory(int id, string name)
        {
            this.id = id;
            this.name = name;
            //this.capacity = capacity;
        }

        public FacilityCategory(DataRow row)
        {
            this.id = (int)row["id"];
            this.name = row["Name"].ToString();
      
        }

        private int id;
        private string name;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
    }
}
