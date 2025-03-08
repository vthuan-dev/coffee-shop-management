using QuanlyquanCafe.Admin.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanlyquanCafe.Admin.DAO
{
    public class MenuDAO
    {
        private static MenuDAO instance;

        public static MenuDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new MenuDAO();
                return MenuDAO.instance;
            }
            private set
            {
                MenuDAO.instance = value;
            }
        }

        private MenuDAO() { }
        public List<MenuDTO> GetListMenu()
        {
            List<MenuDTO> listMenu = new List<MenuDTO>();
            string query = "SELECT m.id, m.Name [Tên], Price [Giá], mc.Name [Loại], CategoryID " +
                "FROM Menu as m, MenuCategory as mc\r\nwhere m.CategoryID = mc.id";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow dr in data.Rows)
            {
                MenuDTO menu = new MenuDTO(dr);
                listMenu.Add(menu);
            }
            return listMenu;
        }

        public bool AddNewItem(string name, float price, int category)
        {
            string query = string.Format("Insert into Menu Values (N'{0}', {1}, {2})", name, price, category);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool UpdateItem(int id, string name, float price, int category)
        {
            string query = string.Format("UPDATE Menu SET Name = N'{0}', Price = {1}, CategoryID = {2} WHERE id = {3}", name, price, category, id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool DeleteItem(int id)
        {
            BillInfoDAO.Instance.DeleteBillInfoByMenuID(id);
            string query = string.Format("Delete from Menu where id = {0}", id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public List<MenuDTO> SearchMenuByName(string name)
        {
            List<MenuDTO> listMenu = new List<MenuDTO>();
            string query = string.Format("SELECT m.id, m.Name [Tên], Price [Giá], mc.Name [Loại], CategoryID" +
                "FROM Menu as m, MenuCategory as mc\r\nwhere m.CategoryID = mc.id and m.Name like N'%{0}%'", name);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow dr in data.Rows)
            {
                MenuDTO menu = new MenuDTO(dr);
                listMenu.Add(menu);
            }
            return listMenu;
        }
    }
}
