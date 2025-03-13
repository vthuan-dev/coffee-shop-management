using QuanlyquanCafe.Admin.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanlyquanCafe.Admin.DAO
{
    public class FacCategoryDAO
    {
        private static FacCategoryDAO instance;

        private FacCategoryDAO() { }

        public static FacCategoryDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new FacCategoryDAO();
                return FacCategoryDAO.instance;
            }
            private set
            {
                FacCategoryDAO.instance = value;
            }
        }

        public List<FacilityCategory> GetListFacCategory()
        {
            List<FacilityCategory> list = new List<FacilityCategory>();
            string query = "SELECT * FROM dbo.FacilityCategory";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                FacilityCategory category = new FacilityCategory(item);
                list.Add(category);
            }
            return list;
        }

        public FacilityCategory GetFacCategoryByID(int id)
        {
            FacilityCategory category = null;
            string query = "SELECT * FROM dbo.FacilityCategory WHERE id = " + id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                category = new FacilityCategory(item);
                return category;
            }
            return category;
        }

        public List<string> GetLocations()
        {
            string query = "SELECT DISTINCT Location FROM Facility";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data.AsEnumerable().Select(row => row["Location"].ToString()).ToList();
        }

        public FacilityDetails GetFacilityDetailsByID(int facilityID)
        {
            string query = "EXEC GetFacilityDetailsByID @FacilityID";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { facilityID });

            if (data.Rows.Count > 0)
            {
                return new FacilityDetails(data.Rows[0]);
            }
            return null;
        }


    }
}
