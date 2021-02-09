using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WSTesting
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            //SqlConnection conexion = new SqlConnection("server=DESKTOP-VHQF2SD ; database=DB_WSTEST ; integrated security = true");
            //conexion.Open();
            
            String ConnectionString = "server=DESKTOP-VHQF2SD ; database=DB_WSTEST ; integrated security = true";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT @@Version AS Version;", connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return string.Format("Estado de la conexion: {0}", reader.GetString(0) );
                        }
                    }
                    reader.Close();
            }
            
            
            return string.Format("You entered: {0}", value);
            

        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
