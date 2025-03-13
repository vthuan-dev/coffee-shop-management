using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanlyquanCafe.Admin.DAO
{
    public class BillInfoDAO
    {
        private static BillInfoDAO instance;

        private BillInfoDAO() { }

        public static BillInfoDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new BillInfoDAO();
                return BillInfoDAO.instance;
            }
            private set
            {
                BillInfoDAO.instance = value;
            }
        }

        public void DeleteBillInfoByMenuID(int id)
        {
            string query = string.Format("Delete BillInfo where MenuID = {0}", id);
            DataProvider.Instance.ExecuteNonQuery(query);
        }
    }
}
