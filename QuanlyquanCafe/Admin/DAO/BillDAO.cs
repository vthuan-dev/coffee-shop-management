using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanlyquanCafe.Admin.DAO
{
    public class BillDAO
    {

        private static BillDAO instance;

        public static BillDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new BillDAO();
                return BillDAO.instance;
            }
            private set
            {
                BillDAO.instance = value;
            }
        }

        private BillDAO() { }
        public DataTable GetListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            return DataProvider.Instance.ExecuteQuery("exec Bill_GetListBillByDate @checkIn , @checkOut", new object[] { checkIn, checkOut});
        }

        public void DeleteBillByTableID(int id)
        {
            string query = string.Format("Delete BillInfo where BillID in (select id from Bill where TableID = {0})", id);
            string query1 = string.Format("Delete Bill where TableID = {0}", id);
            DataProvider.Instance.ExecuteNonQuery(query);
            DataProvider.Instance.ExecuteNonQuery(query1);
        }
    }
}
