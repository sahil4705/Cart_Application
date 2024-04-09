using Microsoft.AspNetCore.Identity;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Cart_Application_MVC.Models
{
    public class ProductModel
    {
        [Required(ErrorMessage = "Enter ProductId")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Enter ProductName")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Enter CategoryId")]
        public string CategoryId { get; set; }

        [Required(ErrorMessage = "Enter Price")]
        public int Price { get; set; }

        [Required (ErrorMessage ="Enter QuantityAvailable")]
        public string QuantityAvailable { get; set; }

        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProductDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;");

        public bool Insert(ProductModel product)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Prod_Details VALUES(@ProductId, @ProductName, @CategoryId, @Price, @QuantityAvailable)", con);
            cmd.Parameters.AddWithValue("@ProductId",product.ProductId);
            cmd.Parameters.AddWithValue("@ProductName",product.ProductName);
            cmd.Parameters.AddWithValue("@CategoryId",product.CategoryId);
            cmd.Parameters.AddWithValue("@Price",product.Price);
            cmd.Parameters.AddWithValue("@QuantityAvailable", product.QuantityAvailable);

            int i=cmd.ExecuteNonQuery();
            if (i >=1)
            {
                return true;
            }
            return false;
        }

        public List<ProductModel> GetProducts()
        {
            List<ProductModel> prod_list = new List<ProductModel> ();
            List<CategoryModel> categ_list = new List<CategoryModel>();
            con.Open();

            SqlDataAdapter da = new SqlDataAdapter("SELECT *FROM Prod_Details", con);

            DataSet ds = new DataSet();

            da.Fill(ds);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                prod_list.Add(new ProductModel
                {
                    ProductId = Convert.ToInt32(dr["ProductID"].ToString()),
                    ProductName = dr["ProductName"].ToString(),
                    CategoryId = dr["CategoryId"].ToString(),
                    Price = Convert.ToInt32(dr["Price"].ToString()),
                    QuantityAvailable = dr["QuantityAvailable"].ToString()
                });
            }
            con.Close();
            return prod_list;
        }
    }
}
