using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace Cart_Application_MVC.Models
{
    public class CategoryModel
    {
        [Key]
        public int Category_Id { get; set; }
        
        [Required (ErrorMessage ="Enter CategoryName")]
        public string Category_Name { get; set;}


        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProductDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;");

        public bool InsertCategory(CategoryModel category)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Category_Details VALUES(@Category_Name)", con);
            cmd.Parameters.AddWithValue("@Category_Name", category.Category_Name);

            int i = cmd.ExecuteNonQuery();
            if (i >= 1)
            {
                return true;
            }
            return false;
        }

        public List<CategoryModel> GetCategory()
        {
            List<CategoryModel> Catgory_list = new List<CategoryModel>();
            con.Open();

            SqlDataAdapter da = new SqlDataAdapter("SELECT *FROM Category_Details", con);

            DataSet ds = new DataSet();

            da.Fill(ds);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Catgory_list.Add(new CategoryModel
                {
                    Category_Id = Convert.ToInt32(dr["Category_Id"].ToString()),
                    Category_Name = dr["Category_Name"].ToString()
                });
            }
            con.Close();
            return Catgory_list;
        }
    }
}
