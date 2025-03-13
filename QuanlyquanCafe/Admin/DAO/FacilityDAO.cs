using QuanlyquanCafe.Admin.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanlyquanCafe.Admin.DAO
{
    public class FacilityDAO
    {
        private static FacilityDAO instance;
        private FacilityDAO() { }
        public static FacilityDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new FacilityDAO();
                return FacilityDAO.instance;
            }
            private set
            {
                FacilityDAO.instance = value;
            }
        }

        public List<Facility> GetListFac()
        {
            List<Facility> listFac = new List<Facility>();
            string query = "SELECT f.*, COALESCE(tf.Status, o.Status) AS Status " +
                "FROM Facility f LEFT JOIN TableFacility tf ON f.id = tf.id " +
                "LEFT JOIN OtherFacility o ON f.id = o.id";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow dr in data.Rows)
            {
                Facility fac = new Facility(dr);
                listFac.Add(fac);
            }
            return listFac;
        }

        public bool AddNewItem(int categoryId, string name, string location, string status)
        {
            // Tạo query cho bảng chính Facility
            string query = string.Format("INSERT INTO Facility (FacilityCategoryID, Name, Location) VALUES ('{0}', N'{1}', N'{2}')", categoryId, name, location);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            if (result > 0)
            {
                // Kiểm tra categoryId và chọn bảng phụ tương ứng
                if (categoryId == 1 || categoryId == 2 || categoryId == 3)
                {
                    // Thêm vào bảng TableFacility
                    string subQuery = string.Format("INSERT INTO TableFacility (id, Status) VALUES ((SELECT MAX(id) FROM Facility), N'{0}')", status);
                    result = DataProvider.Instance.ExecuteNonQuery(subQuery);
                }
                else
                {
                    // Thêm vào bảng OtherFacility
                    string subQuery = string.Format("INSERT INTO OtherFacility (id, Status) VALUES ((SELECT MAX(id) FROM Facility), N'{0}')", status);
                    result = DataProvider.Instance.ExecuteNonQuery(subQuery);
                }
            }

            return result > 0;
        }

        public bool UpdateItem(int id, int categoryId, string name, string location, string status)
        {
            // Lấy ra categoryId hiện tại của Facility để so sánh
            string currentCategoryQuery = string.Format("SELECT FacilityCategoryID FROM Facility WHERE id = {0}", id);
            int currentCategoryId = Convert.ToInt32(DataProvider.Instance.ExecuteScalar(currentCategoryQuery));

            int result = 0;

            // Cập nhật bảng Facility
            string query = string.Format("UPDATE Facility SET FacilityCategoryID = {0}, Name = N'{1}', Location = N'{2}' WHERE id = {3}",
                                         categoryId, name, location, id);
            result = DataProvider.Instance.ExecuteNonQuery(query);

            // Nếu cập nhật bảng chính thành công, tiếp tục xử lý bảng phụ
            if (result > 0)
            {
                // Nếu categoryId thay đổi, cần chuyển mẫu tin từ bảng cũ sang bảng mới
                if (currentCategoryId != categoryId)
                {
                    if (currentCategoryId == 1 || currentCategoryId == 2 || currentCategoryId == 3)
                    {
                        // Nếu mẫu tin hiện đang ở TableFacility (categoryId 1, 2, 3), xóa khỏi TableFacility
                        string deleteQuery = string.Format("DELETE FROM TableFacility WHERE id = {0}", id);
                        result = DataProvider.Instance.ExecuteNonQuery(deleteQuery);
                    }
                    else
                    {               
                        string deleteQuery = string.Format("DELETE FROM OtherFacility WHERE id = {0}", id);
                        result = DataProvider.Instance.ExecuteNonQuery(deleteQuery);
                    }

                    // Thêm vào bảng phụ tương ứng theo categoryId mới
                    if (categoryId == 1 || categoryId == 2 || categoryId == 3)
                    {
                        // Thêm vào TableFacility
                        string subQuery = string.Format("INSERT INTO TableFacility (id, Status) VALUES ({0}, N'{1}')", id, status);
                        result = DataProvider.Instance.ExecuteNonQuery(subQuery);
                    }
                    else
                    {
                        // Thêm vào OtherFacility
                        string subQuery = string.Format("INSERT INTO OtherFacility (id, Status) VALUES ({0}, N'{1}')", id, status);
                        result = DataProvider.Instance.ExecuteNonQuery(subQuery);
                    }
                }
                else
                {
                    // Nếu categoryId không thay đổi, chỉ cần cập nhật bảng phụ tương ứng
                    if (categoryId == 1 || categoryId == 2 || categoryId == 3)
                    {
                        string subQuery = string.Format("UPDATE TableFacility SET Status = N'{0}' WHERE id = {1}", status, id);
                        result = DataProvider.Instance.ExecuteNonQuery(subQuery);
                    }
                    else
                    {
                        string subQuery = string.Format("UPDATE OtherFacility SET Status = N'{0}' WHERE id = {1}", status, id);
                        result = DataProvider.Instance.ExecuteNonQuery(subQuery);
                    }
                }
            }

            return result > 0;
        }

        public bool DeleteItem(int id)
        {
            BillDAO.Instance.DeleteBillByTableID(id);
            string query = "EXEC DeleteFacilityByID @FacilityID";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { id });
            return result > 0;
        }

        public List<Facility> SearchFacByName(string name)
        {
            List<Facility> listFac = new List<Facility>();
            string query = string.Format("SELECT f.*, COALESCE(tf.Status, o.Status) AS Status " +
                "FROM Facility f " +
                "LEFT JOIN TableFacility tf ON f.id = tf.id " +
                "LEFT JOIN OtherFacility o ON f.id = o.id " +
                "WHERE f.name LIKE N'%{0}%'", name);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow dr in data.Rows)
            {
                Facility fac = new Facility(dr);
                listFac.Add(fac);
            }
            return listFac;
        }

    }
}
