using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanlyquanCafe.Admin.DTO
{
    public class Facility
    {
        public Facility(int id, int facilityCateID, string name, string location, string status)
        {
            this.id = id;
            this.FacilityCateID = facilityCateID;
            this.name = name;
            this.Location = location;
            this.status = status;
            //this.capacity = capacity;
        }

        public Facility(DataRow row)
        {
            this.id = (int)row["id"];
            this.FacilityCateID = (int)row["FacilityCategoryID"];
            this.name = row["Name"].ToString();
            this.Location = row["Location"].ToString();
            this.status = row["Status"].ToString();
            //this.capacity = row["Capacity"] == DBNull.Value ? 0 : Convert.ToInt32(row["Capacity"]);
        }
        private int id;
        private int FacilityCateID;
        private string name;
        private string Location;
        private string status;
        //private int capacity;

        public int Id { get => id; set => id = value; }
        //public int FacilityID1 { get => FacilityID; set => FacilityID = value; }
        public string Name { get => name; set => name = value; }
        public string Location1 { get => Location; set => Location = value; }
        public int FacilityCateID1 { get => FacilityCateID; set => FacilityCateID = value; }
        public string Status { get => status; set => status = value; }
        //public int Capacity { get => capacity; set => capacity = value; }
        //public string Status1 { get => Status; set => Status = value; }
    }
}
