using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prySilvaClaseBD
{
    public class CConexion
    {
        // Propiedades públicas para ser accedidas por otras clases del proyecto
        public OleDbConnection CNN;
        public DataSet DS;

        // Propiedad privada para la gestión de errores
        private string ERROR = "";

        // Constructor: inicializa las propiedades
        public CConexion()
        {
            CNN = null;
            DS = null;
            ERROR = "";
        }

        // Método para establecer la conexión
        public bool Conectar(string cadenaConexion)
        {
            bool resultado = false;
            CNN = new OleDbConnection();
            CNN.ConnectionString = cadenaConexion;
            try
            {
                CNN.Open();
                // Se instancia el DataSet que servirá de contenedor de tablas en memoria
                DS = new DataSet();
                resultado = true;
            }
            catch (Exception ex)
            {
                ERROR = ex.Message;
            }
            return resultado;
        }

        // Método para cerrar la conexión de forma segura
        public bool Desconectar()
        {
            bool resultado = false;
            try
            {
                if (CNN != null && CNN.State == ConnectionState.Open)
                {
                    CNN.Close();
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                ERROR = ex.Message;
            }
            return resultado;
        }

        // Método para recuperar el último error ocurrido
        public string ObtenerError()
        {
            return ERROR;
        }
    }
}
