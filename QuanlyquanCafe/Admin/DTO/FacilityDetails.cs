using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanlyquanCafe.Admin.DTO
{
    public class FacilityDetails
    {   
        public FacilityDetails(string location, string status)
        {
            this.Location = location;
            this.Status = status;
        }
        public string Location { get; set; }
        public string Status { get; set; }

        public FacilityDetails(DataRow row)
        {
            this.Location = row["Location"].ToString();
            this.Status = row["Status"].ToString();
        }
    }
}
