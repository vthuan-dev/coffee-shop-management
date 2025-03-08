using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanlyquanCafe.Admin.DTO
{
    public class BillInfo
    {
        public BillInfo(int id, int idBill, int idMenu, int count)
        {
            this.id = id;
            this.idBill = idBill;
            this.idMenu = idMenu;
            this.count = count;
        }

        public BillInfo(DataRow row) {
            this.id = (int)row["id"];
            this.idBill = (int)row["idBill"];
            this.idMenu = (int)row["idMenu"];
            this.count = (int)row["count"];
        }   
        private int id;
        private int idBill;
        private int idMenu;
        private int count;

        public int Id { get => id; set => id = value; }
        public int IdBill { get => idBill; set => idBill = value; }
        public int IdMenu { get => idMenu; set => idMenu = value; }
        public int Count { get => count; set => count = value; }
    }
}
