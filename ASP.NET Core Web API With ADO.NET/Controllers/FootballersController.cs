using ASP.NET_Core_Web_API_With_ADO.NET.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ASP.NET_Core_Web_API_With_ADO.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FootballersController : Controller
    {
        //ConnectionString Ayarları
        private IConfiguration Configuration;
        public FootballersController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        SqlConnection conString;
        SqlCommand cmd;

        //Yaşa göre futbolcu sıralayarak getirme
        [HttpGet("age/sorted")]
        public JsonResult GetFootballerSortedByAge()
        {
            conString = new SqlConnection(Configuration.GetConnectionString("WebAPIADONETConnecttion"));
            cmd = new SqlCommand("SELECT * FROM Footballer ORDER BY Age ASC", conString);

            DataTable dt = new DataTable();
            conString.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            conString.Close();

            if (dt.Rows.Count > 0)
            {
                List<Footballer> footballerList = new List<Footballer>();
                foreach (DataRow dr in dt.Rows)
                {
                    Footballer footballer = new Footballer
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        FootballerName = dr["FootballerName"].ToString(),
                        Age = Convert.ToInt32(dr["Age"]),
                        Address = dr["Address"].ToString()
                    };
                    footballerList.Add(footballer);
                }
                return Json(footballerList);
            }
            return Json("Futbolcu bulunamadı.");
        }

        //İsme göre futbolcu sıralayarak getirme
        [HttpGet("name/sorted")]
        public JsonResult GetFootballerSortedByName()
        {
            conString = new SqlConnection(Configuration.GetConnectionString("WebAPIADONETConnecttion"));
            cmd = new SqlCommand("SELECT * FROM Footballer ORDER BY FootballerName ASC", conString);

            DataTable dt = new DataTable();
            conString.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            conString.Close();

            if (dt.Rows.Count > 0)
            {
                List<Footballer> footballerList = new List<Footballer>();
                foreach (DataRow dr in dt.Rows)
                {
                    Footballer footballer = new Footballer
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        FootballerName = dr["FootballerName"].ToString(),
                        Age = Convert.ToInt32(dr["Age"]),
                        Address = dr["Address"].ToString()
                    };
                    footballerList.Add(footballer);
                }
                return Json(footballerList);
            }
            return Json("Futbolcu bulunamadı.");
        }

        //Tüm Futolcuları getirme
        [HttpGet]
        public JsonResult GetAllFootballer()
        {
            List<Footballer> footballerList = new List<Footballer>();
            conString = new SqlConnection(Configuration.GetConnectionString("WebAPIADONETConnecttion"));
            DataTable dt = new DataTable();
            cmd = new SqlCommand("Select * from Footballer", conString);
            conString.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Footballer entity = new Footballer();
                entity.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                entity.FootballerName = dt.Rows[i]["FootballerName"].ToString();
                entity.Age = Convert.ToInt32(dt.Rows[i]["Age"]);
                entity.Address = dt.Rows[i]["Address"].ToString();
            }
            conString.Close();
            return Json(dt);
        }

        //Id'ye göre futbolcu göre getirme
        [HttpGet("{id}")]
        public JsonResult GetOneFootballerById(int id)
        {
            Footballer footballer = new Footballer();
            conString = new SqlConnection(Configuration.GetConnectionString("WebAPIADONETConnecttion"));
            DataTable dt = new DataTable();
            cmd = new SqlCommand("Select * from Footballer WHERE Id = @Id", conString);
            cmd.Parameters.AddWithValue("@Id", id);
            conString.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                footballer.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                footballer.FootballerName = dt.Rows[0]["FootballerName"].ToString();
                footballer.Age = Convert.ToInt32(dt.Rows[0]["Age"]);
                footballer.Address = dt.Rows[0]["Address"].ToString();
                return Json(footballer);
            }
            else
            {
                return Json("Verilen Id'ye sahip Futbolcu bulunamadı.");
            }                
        }

        //İsme göre futbolcu göre getirme
        [HttpGet("name/{footballerName}")]
        public JsonResult GetOneFootballerByName(string footballerName)
        {
            conString = new SqlConnection(Configuration.GetConnectionString("WebAPIADONETConnecttion"));
            cmd = new SqlCommand("SELECT * FROM Footballer WHERE FootballerName = @FootballerName", conString);
            cmd.Parameters.AddWithValue("@FootballerName", footballerName);

            DataTable dt = new DataTable();
            conString.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            conString.Close();

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                Footballer footballer = new Footballer
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    FootballerName = dr["FootballerName"].ToString(),
                    Age = Convert.ToInt32(dr["Age"]),
                    Address = dr["Address"].ToString()
                };
                return Json(footballer);
            }
                return Json("Bu isimde bir Futbolcu bulunamadı.");
        }

        //Yaşa göre futbolcu getrime
        [HttpGet("age/{age}")]
        public JsonResult GetOneFootballerByAge(int age)
        {
            conString = new SqlConnection(Configuration.GetConnectionString("WebAPIADONETConnecttion"));
            cmd = new SqlCommand("SELECT * FROM Footballer WHERE Age = @Age", conString);
            cmd.Parameters.AddWithValue("@Age", age);

            DataTable dt = new DataTable();
            conString.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            conString.Close();

            if (dt.Rows.Count > 0)
            {
                List<Footballer> footballerList = new List<Footballer>();
                foreach (DataRow dr in dt.Rows)
                {
                    Footballer footballer = new Footballer
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        FootballerName = dr["FootballerName"].ToString(),
                        Age = Convert.ToInt32(dr["Age"]),
                        Address = dr["Address"].ToString()
                    };
                    footballerList.Add(footballer);
                }
                return Json(footballerList);
            }
                return Json("Belirtilen yaşa uygun Futbolcu bulunamadı.");
        }

        //18 yaşından büyük futbolcuları getirme
        [HttpGet("above18")]
        public JsonResult GetFootballerAbove18()
        {
            conString = new SqlConnection(Configuration.GetConnectionString("WebAPIADONETConnecttion"));
            cmd = new SqlCommand("SELECT * FROM Footballer WHERE Age > 18", conString);

            DataTable dt = new DataTable();
            conString.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            conString.Close();

            if (dt.Rows.Count > 0)
            {
                List<Footballer> footballerList = new List<Footballer>();
                foreach (DataRow dr in dt.Rows)
                {
                    Footballer footballer = new Footballer
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        FootballerName = dr["FootballerName"].ToString(),
                        Age = Convert.ToInt32(dr["Age"]),
                        Address = dr["Address"].ToString()
                    };
                    footballerList.Add(footballer);
                }
                return Json(footballerList);
            }
                return Json("18 yaş üstü Futbolcu bulunamadı.");
        }

        //Yeni futbolcu ekleme
        [HttpPost]
        public JsonResult CreateOneFootballer(Footballer footballer)
        {
            if (footballer.Age < 15)
            {
                return Json("Futbolcu oluşturulamadı. 15 yaşından küçük öğrenciler kayıt yapamaz.");
            }

            conString = new SqlConnection(Configuration.GetConnectionString("WebAPIADONETConnecttion"));
            cmd = new SqlCommand("INSERT INTO Footballer (FootballerName, Age, Address) VALUES (@FootballerName, @Age, @Address)", conString);
            cmd.Parameters.AddWithValue("@FootballerName", footballer.FootballerName);
            cmd.Parameters.AddWithValue("@Age", footballer.Age);
            cmd.Parameters.AddWithValue("@Address", footballer.Address);

            conString.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            conString.Close();

            if (rowsAffected > 0)
            {
                return Json("Yeni Futbolcu başarıyla oluşturuldu.");
            }
            return Json("Futbolcu oluşturulamadı.");
        }

        //Futbolcu bilgilerini güncelleme
        [HttpPut("{id}")]
        public JsonResult UpdateOneFootballer(int id, Footballer footballer)
        {
            conString = new SqlConnection(Configuration.GetConnectionString("WebAPIADONETConnecttion"));
            cmd = new SqlCommand("UPDATE Footballer SET FootballerName = @FootballerName, Age = @Age, Address = @Address WHERE Id = @Id", conString);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@FootballerName", footballer.FootballerName);
            cmd.Parameters.AddWithValue("@Age", footballer.Age);
            cmd.Parameters.AddWithValue("@Address", footballer.Address);

            conString.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            conString.Close();

            if (rowsAffected > 0)
            {
                return Json("Futbolcu bilgileri başarıyla güncellendi.");
            }            
                return Json("Futbolcu bilgileri güncellenemedi.");       
        }

        //Futbolcu bilgilerini silme
        [HttpDelete("{id}")]
        public JsonResult DeleteOneFootballer(int id)
        {
            conString = new SqlConnection(Configuration.GetConnectionString("WebAPIADONETConnecttion"));
            cmd = new SqlCommand("DELETE FROM Footballer WHERE Id = @Id", conString);
            cmd.Parameters.AddWithValue("@Id", id);

            conString.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            conString.Close();

            if (rowsAffected > 0)
            {
                return Json("Futbolcu başarıyla silindi.");
            }
                return Json("Futbolcu silinemedi.");
        }
    }
}
