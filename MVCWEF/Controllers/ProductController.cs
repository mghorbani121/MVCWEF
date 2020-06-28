using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using MVCWEF.Models;

namespace MVCWEF.Controllers
{
    public class ProductController : Controller
    {
        string connectionstring = @"Data Source = DESKTOP-61HHC6B; Initial Catalog = MvcCrudDB; Integrated Security = True";

        [HttpGet]
        public ActionResult Index()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionstring))
            {
                sqlCon.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Product",sqlCon);
                adapter.Fill(dt);
            }
                return View(dt);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new ProductModel());
        }


        [HttpPost]
        public ActionResult Create(ProductModel productModel)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionstring))
            {
                sqlCon.Open();
                string query = "INSERT INTO Product values(@pName,@price,@pCount)";
                SqlCommand cmd = new SqlCommand(query,sqlCon);

                cmd.Parameters.Add("@pName",  SqlDbType.NVarChar, 50).Value = productModel.ProductName;
                cmd.Parameters.Add("@price",  SqlDbType.Int).Value = productModel.Price;
                cmd.Parameters.Add("@pCount", SqlDbType.Int).Value = productModel.Count;
                cmd.ExecuteNonQuery();

                //cmd.Parameters["@price"].Precision = 2;
            }
                return RedirectToAction("Index");
        }
                   
        
        public ActionResult Edit(int id)
        {
                ProductModel productModel = new ProductModel();
                DataTable dt = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionstring))
            {
                sqlCon.Open();
                string query = "SELECT * FROM Product WHERE ProductID = @ProductID";
                SqlDataAdapter adapter = new SqlDataAdapter(query,sqlCon);
                adapter.SelectCommand.Parameters.Add("@ProductID", SqlDbType.Int).Value = id;
              //  adapter.SelectCommand.Parameters.AddWithValue("@productID", id);
                adapter.Fill(dt);
            }
            if(dt.Rows.Count == 1)
            {
                productModel.ProductID = Convert.ToInt32(dt.Rows[0][0].ToString());
                productModel.ProductName = dt.Rows[0][1].ToString();
                productModel.Price = Convert.ToInt32(dt.Rows[0][2].ToString());
                productModel.Count = Convert.ToInt32(dt.Rows[0][3].ToString());

                return View(productModel);
            }
            else
                return RedirectToAction("Index");
        }

        
        [HttpPost]
        public ActionResult Edit(int id, ProductModel productModel)
        {  
            DataTable dt = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionstring))
            {
                sqlCon.Open();
                string query = "update Product set ProductName = @ProductName, Price = @price, Count = @count where ProductID = @ProductID";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = productModel.ProductID;
                cmd.Parameters.Add("@ProductName", SqlDbType.NVarChar, 50).Value = productModel.ProductName;
                cmd.Parameters.Add("@Price", SqlDbType.Int).Value = productModel.Price;
                cmd.Parameters.Add("@count", SqlDbType.Int).Value = productModel.Count;
                cmd.ExecuteNonQuery();
            }
                return RedirectToAction("Index");
        }

        
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionstring))
            {
                sqlCon.Open();
                string query = "delete from Product where ProductID = @ProductID";
                SqlCommand cmd = new SqlCommand(query,sqlCon);
                cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = id;
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
