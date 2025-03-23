using System;
using System.Collections.Generic;
using System.Data;

namespace QuanlyquanCafe.Admin.DAO
{
    public class BillInfoDAO
    {
        private static BillInfoDAO instance;

        public static BillInfoDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new BillInfoDAO();
                return instance;
            }
            private set
            {
                instance = value;
            }
        }

        private BillInfoDAO() { }

        // Thêm món vào bill
        public bool InsertBillInfo(int billID, int menuID, int quantity)
        {
            try
            {
                // Kiểm tra xem món đã có trong bill chưa
                string checkQuery = "SELECT id, Quantity FROM BillInfo WHERE BillID = @billID AND MenuID = @menuID";
                DataTable data = DataProvider.Instance.ExecuteQuery(checkQuery, new object[] { billID, menuID });
                
                if (data.Rows.Count > 0)
                {
                    // Nếu món đã tồn tại, cập nhật số lượng
                    int id = Convert.ToInt32(data.Rows[0]["id"]);
                    int oldQuantity = Convert.ToInt32(data.Rows[0]["Quantity"]);
                    int newQuantity = oldQuantity + quantity;
                    
                    if (newQuantity <= 0)
                    {
                        // Nếu số lượng <= 0, xóa món khỏi bill
                        return DeleteBillInfo(id);
                    }
                    else
                    {
                        // Cập nhật số lượng
                        string updateQuery = "UPDATE BillInfo SET Quantity = @quantity WHERE id = @id";
                        return DataProvider.Instance.ExecuteNonQuery(updateQuery, new object[] { newQuantity, id }) > 0;
                    }
                }
                else if (quantity > 0)
                {
                    // Nếu món chưa có trong bill, thêm mới
                    string insertQuery = "INSERT INTO BillInfo (BillID, MenuID, Quantity) VALUES (@billID, @menuID, @quantity)";
                    return DataProvider.Instance.ExecuteNonQuery(insertQuery, new object[] { billID, menuID, quantity }) > 0;
                }
                
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thêm món vào bill: " + ex.Message);
            }
        }

        // Xóa một món trong bill
        public bool DeleteBillInfo(int id)
        {
            string query = "DELETE FROM BillInfo WHERE id = @id";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { id }) > 0;
        }

        // Xóa tất cả món trong bill
        public bool DeleteBillInfoByBillID(int billID)
        {
            string query = "DELETE FROM BillInfo WHERE BillID = @billID";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { billID }) > 0;
        }

        // Xóa món trong tất cả bill (khi xóa món khỏi menu)
        public bool DeleteBillInfoByMenuID(int menuID)
        {
            string query = "DELETE FROM BillInfo WHERE MenuID = @menuID";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { menuID }) > 0;
        }

        // Lấy danh sách món trong bill
        public DataTable GetBillDetailsByBillID(int billID)
        {
            string query = @"
                SELECT bi.id, bi.MenuID, m.Name AS [Món], bi.Quantity AS [Số lượng], 
                       m.Price AS [Giá], (m.Price * bi.Quantity) AS [Thành tiền]
                FROM BillInfo bi
                JOIN Menu m ON bi.MenuID = m.id
                WHERE bi.BillID = @billID";
                
            return DataProvider.Instance.ExecuteQuery(query, new object[] { billID });
        }
    }
}
