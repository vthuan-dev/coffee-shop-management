using QuanlyquanCafe.Admin.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanlyquanCafe.Admin.DAO
{
    public class MenuCategoryDAO
    {
        private static MenuCategoryDAO instance;
        public static MenuCategoryDAO Instance
        {
            get { if (instance == null) instance = new MenuCategoryDAO(); return MenuCategoryDAO.instance; }
            private set { MenuCategoryDAO.instance = value; }
        }
        private MenuCategoryDAO() { }
        public List<MenuCategory> GetListMenuCategory()
        {
            List<MenuCategory> list = new List<MenuCategory>();
            string query = "SELECT * FROM dbo.MenuCategory";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                MenuCategory category = new MenuCategory(item);
                list.Add(category);
            }
            return list;
        }

        public MenuCategory GetCategoryByID(int id)
        {
            MenuCategory category = null;
            string query = "SELECT * FROM dbo.MenuCategory WHERE id = " + id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                category = new MenuCategory(item);
                return category;
            }
            return category;
        }
    }
}
