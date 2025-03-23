using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanlyquanCafe.Admin.DTO;

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
                return instance;
            }
            private set
            {
                instance = value;
            }
        }

        private BillDAO() { }

        // Tạo bill mới
        public int CreateBill(int tableID, int userID)
        {
            try
            {
                string query = "INSERT INTO Bill (TableID, UserID, CheckInDate, Status, Discount, TotalPrice) " +
                               "VALUES (@tableID, @userID, GETDATE(), 0, 0, 0); SELECT SCOPE_IDENTITY()";
                
                object result = DataProvider.Instance.ExecuteScalar(query, new object[] { tableID, userID });
                
                if (result != null)
                {
                    // Cập nhật trạng thái bàn thành "Có người"
                    string updateTable = "UPDATE TableFacility SET Status = N'Có người' WHERE id = @tableID";
                    DataProvider.Instance.ExecuteNonQuery(updateTable, new object[] { tableID });
                    
                    return Convert.ToInt32(result);
                }
                return -1;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tạo hóa đơn: " + ex.Message);
            }
        }

        // Lấy bill chưa thanh toán theo bàn
        public int GetUncheckBillIDByTableID(int tableID)
        {
            try
            {
                string query = "SELECT id FROM Bill WHERE TableID = @tableID AND Status = 0";
                object result = DataProvider.Instance.ExecuteScalar(query, new object[] { tableID });
                
                return result != null ? Convert.ToInt32(result) : -1;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy hóa đơn: " + ex.Message);
            }
        }

        // Thanh toán bill
        public bool CheckOut(int billID, decimal discount = 0)
        {
            try
            {
                // Cập nhật tổng tiền
                UpdateTotalPrice(billID);
                
                // Cập nhật trạng thái đã thanh toán và giảm giá
                string query = "UPDATE Bill SET Status = 1, Discount = @discount WHERE id = @billID";
                int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { discount, billID });
                
                if (result > 0)
                {
                    // Lấy tableID từ bill
                    string getTableQuery = "SELECT TableID FROM Bill WHERE id = @billID";
                    object tableIDObj = DataProvider.Instance.ExecuteScalar(getTableQuery, new object[] { billID });
                    
                    if (tableIDObj != null)
                    {
                        int tableID = Convert.ToInt32(tableIDObj);
                        
                        // Cập nhật trạng thái bàn thành "Trống"
                        string updateTableQuery = "UPDATE TableFacility SET Status = N'Trống' WHERE id = @tableID";
                        DataProvider.Instance.ExecuteNonQuery(updateTableQuery, new object[] { tableID });
                    }
                }
                
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thanh toán: " + ex.Message);
            }
        }

        // Tính tổng tiền bill
        public void UpdateTotalPrice(int billID)
        {
            string query = @"
                UPDATE Bill 
                SET TotalPrice = (
                    SELECT SUM(m.Price * bi.Quantity) 
                    FROM BillInfo bi 
                    JOIN Menu m ON bi.MenuID = m.id 
                    WHERE bi.BillID = @billID
                )
                WHERE id = @billID";
                
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { billID });
        }

        // Lấy chi tiết bill để in
        public DataTable GetBillDetails(int billID)
        {
            string query = @"
                SELECT m.Name AS [Tên món], bi.Quantity AS [Số lượng], 
                       m.Price AS [Đơn giá], (m.Price * bi.Quantity) AS [Thành tiền]
                FROM BillInfo bi
                JOIN Menu m ON bi.MenuID = m.id
                WHERE bi.BillID = @billID";
                
            return DataProvider.Instance.ExecuteQuery(query, new object[] { billID });
        }

        // Lấy thông tin bill để in
        public DataTable GetBillInfo(int billID)
        {
            string query = @"
                SELECT b.id, u.FullName AS [Nhân viên], t.Name AS [Bàn], 
                       b.CheckInDate AS [Ngày], b.TotalPrice AS [Tổng tiền], 
                       b.Discount AS [Giảm giá], 
                       (b.TotalPrice - (b.TotalPrice * b.Discount / 100)) AS [Thanh toán]
                FROM Bill b
                JOIN Users u ON b.UserID = u.uid
                JOIN Facility t ON b.TableID = t.id
                WHERE b.id = @billID";
                
            return DataProvider.Instance.ExecuteQuery(query, new object[] { billID });
        }

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
