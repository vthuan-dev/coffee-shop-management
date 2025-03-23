using System;

namespace RestaurantManagement.DTO
{
    public class FacilityDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public int TableID { get; set; } // ID của bàn trong bảng TableFacility

        public FacilityDTO() { }

        public FacilityDTO(int id, string name, string location, string status, int tableID)
        {
            ID = id;
            Name = name;
            Location = location;
            Status = status;
            TableID = tableID;
        }
    }
} 