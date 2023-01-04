using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyCrud.Pages.baleros
{
    public class CrearBaleroModel : PageModel
    {
        public String ErrorMensaje = "";
        public string GuardadoMensaje = "";
        public BalerosInfo balerosInfo = new BalerosInfo();
        public void OnGet()
        {
        }

        public void OnPost()
        {
            balerosInfo.Marca = Request.Form["marca"];//son los nombres que se puso en el name de los input de CrearBalero
            balerosInfo.Nombre = Request.Form["codigo"];
            balerosInfo.Precio = Convert.ToDecimal(Request.Form["precio"]);

            if(balerosInfo.Marca.Length == 0 || balerosInfo.Nombre.Length == 0 )
            {
                ErrorMensaje = "Todos los campos deben de ser rellenados";
                return;
            }

            // Guardar un nuevo registro en la base de datos

            try
            {
                string conexion = "Data Source=.;Initial Catalog=crudmvc;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(conexion))
                {
                    connection.Open();
                    string sql = "INSERT INTO Baleros (Marca, Nombre, Precio) VAlUES (@Marca, @Nombre, @Precio);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Marca", balerosInfo.Marca);
                        command.Parameters.AddWithValue("@Nombre", balerosInfo.Nombre);
                        command.Parameters.AddWithValue("@Precio", balerosInfo.Precio);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMensaje = ex.Message;
                throw;
            }

            balerosInfo.Marca = "";
            balerosInfo.Nombre = "";

            GuardadoMensaje = "Nuevo Registro Creado Correctamente";

            Response.Redirect("/baleros/Index");
        }
    }
}
