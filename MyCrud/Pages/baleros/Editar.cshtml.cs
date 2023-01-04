using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyCrud.Pages.baleros
{
    public class EditarModel : PageModel
    {
        public BalerosInfo balerosInfo = new BalerosInfo();
        public String ErrorMensaje = "";
        public String GuardadoMensaje = "";

        public void OnGet()
        {
            string Id = Request.Query["Id"];

            try
            {
                string conexion = "Data Source=.;Initial Catalog=crudmvc;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(conexion))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Baleros WHERE Id=@Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", Id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                balerosInfo.Id = "" + reader.GetInt32(0);
                                balerosInfo.Marca = reader.GetString(1);
                                balerosInfo.Nombre = reader.GetString(2);
                                balerosInfo.Precio = reader.GetDecimal(3);
                                balerosInfo.Fecha_create = reader.GetDateTime(4).ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMensaje = ex.Message;
                throw;
            }
        }

        public void OnPost() 
        {
            balerosInfo.Id = Request.Form["Id"];
            balerosInfo.Marca = Request.Form["Marca"];
            balerosInfo.Nombre = Request.Form["Nombre"];
            balerosInfo.Precio = Convert.ToDecimal(Request.Form["Precio"]);


            if(balerosInfo.Id.Length == 0 || balerosInfo.Marca.Length == 0 || balerosInfo.Nombre.Length == 0)
            {
                ErrorMensaje = "Todos los campos necesitan ser rellenados";
                return;
            }

            try
            {
                String conexion = "Data Source=.;Initial Catalog=crudmvc;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(conexion))
                {
                    connection.Open();
                    string sql = "UPDATE Baleros SET Marca = @Marca, Nombre = @Nombre, Precio = @Precio WHERE Id = @Id;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                            command.Parameters.AddWithValue("@Marca", balerosInfo.Marca);
                            command.Parameters.AddWithValue("@Nombre", balerosInfo.Nombre);
                            command.Parameters.AddWithValue("@Precio", balerosInfo.Precio);
                            command.Parameters.AddWithValue("@Id", balerosInfo.Id);

                            command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMensaje = ex.Message;
                return;
                throw;
            }
            
            Response.Redirect("/baleros/Index");
        }
    }
}
