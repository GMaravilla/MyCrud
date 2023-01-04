using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyCrud.Pages.baleros
{
    public class IndexModel : PageModel
    {

        public List<BalerosInfo> listaBaleros = new List<BalerosInfo>();
        public void OnGet()
        {
            try
            {
                string conexion = "Data Source=.;Initial Catalog=crudmvc;Integrated Security=True";

                using (SqlConnection connection= new SqlConnection(conexion))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Baleros";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                BalerosInfo balerosInfo = new BalerosInfo();
                                balerosInfo.Id = "" + reader.GetInt32(0);
                                balerosInfo.Marca= reader.GetString(1);
                                balerosInfo.Nombre= reader.GetString(2);
                                balerosInfo.Precio= reader.GetDecimal(3);
                                balerosInfo.Fecha_create = reader.GetDateTime(4).ToString();

                                listaBaleros.Add(balerosInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error: " + ex.ToString());
                throw;
            }
        }
    }

    public class BalerosInfo
    {
        public string Id;
        public string Marca;
        public string Nombre;
        public decimal Precio;
        public string Fecha_create;

    }
}
