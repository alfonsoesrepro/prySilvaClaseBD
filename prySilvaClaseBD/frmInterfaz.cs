using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace prySilvaClaseBD
{
    public partial class frmInterfaz : Form
    {
        private CConexion conexionActual;

        public frmInterfaz()
        {
            InitializeComponent();
        }

        private void frmInterfaz_Load(object sender, EventArgs e)
        {
            // Inicializar controles
            cboDatabases.Items.Clear();
            cboTables.Items.Clear();
            dgvData.DataSource = null;

            // Poblar combobox buscando archivos en la carpeta ClaseBD del proyecto
            PopulateDatabasesFromClaseBDFolder();
        }

        private void PopulateDatabasesFromClaseBDFolder()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var candidateDirs = new List<string>
            {
                Path.Combine(baseDir, "ClaseBD"),
                Path.GetFullPath(Path.Combine(baseDir, "..", "..", "ClaseBD")),
                Path.GetFullPath(Path.Combine(baseDir, "..", "ClaseBD"))
            };

            var added = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var dir in candidateDirs)
            {
                try
                {
                    if (Directory.Exists(dir))
                    {
                        var mdbs = Directory.GetFiles(dir, "*.mdb");
                        var accdbs = Directory.GetFiles(dir, "*.accdb");
                        foreach (var f in mdbs.Concat(accdbs))
                        {
                            if (!added.Contains(f))
                            {
                                added.Add(f);
                                cboDatabases.Items.Add(f);
                            }
                        }
                    }
                }
                catch
                {
                    // ignorar errores
                }
            }

            if (cboDatabases.Items.Count > 0)
                cboDatabases.SelectedIndex = 0;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Access Databases (*.mdb;*.accdb)|*.mdb;*.accdb|All files (*.*)|*.*";
                ofd.Title = "Seleccionar archivo de base de datos";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string ruta = ofd.FileName;
                    if (!cboDatabases.Items.Contains(ruta))
                        cboDatabases.Items.Add(ruta);
                    cboDatabases.SelectedItem = ruta;
                }
            }
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            if (cboDatabases.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un archivo de base de datos primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string ruta = cboDatabases.SelectedItem.ToString();
            string ext = Path.GetExtension(ruta)?.ToLowerInvariant();

            // Lista de proveedores a intentar en orden de preferencia
            var providersToTry = new List<string>();
            if (ext == ".accdb")
            {
                providersToTry.Add("Provider=Microsoft.ACE.OLEDB.16.0;");
                providersToTry.Add("Provider=Microsoft.ACE.OLEDB.12.0;");
            }
            // Siempre agregar Jet como último recurso (solo .mdb es soportado oficialmente)
            providersToTry.Add("Provider=Microsoft.Jet.OLEDB.4.0;");

            if (conexionActual != null)
            {
                conexionActual.Desconectar();
                conexionActual = null;
            }

            conexionActual = new CConexion();
            bool conectado = false;
            string usedProvider = null;
            string lastError = null;

            foreach (var prov in providersToTry)
            {
                try
                {
                    string tryConn = prov + "Data Source=" + ruta + ";";
                    // asegurar desconexión antes de intentar
                    conexionActual.Desconectar();
                    if (conexionActual.Conectar(tryConn))
                    {
                        conectado = true;
                        usedProvider = prov;
                        break;
                    }
                    else
                    {
                        lastError = conexionActual.ObtenerError();
                    }
                }
                catch (Exception ex)
                {
                    lastError = ex.Message;
                }
            }

            if (conectado)
            {
                if (!string.IsNullOrEmpty(usedProvider) && usedProvider.IndexOf("ACE", StringComparison.OrdinalIgnoreCase) >= 0 && usedProvider.IndexOf("16.0") >= 0)
                {
                    // opcional: informar si se conectó con ACE 16.0
                    // MessageBox.Show("Conectado usando ACE 16.0.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (!string.IsNullOrEmpty(usedProvider) && usedProvider.IndexOf("Jet", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    // Si se conectó con Jet y archivo es .accdb, advertir que el resultado puede ser limitado
                    if (ext == ".accdb")
                    {
                        MessageBox.Show("Se ha conectado usando el proveedor Jet como alternativa. Tenga en cuenta que Jet no soporta correctamente archivos .accdb; instale ACE si experimenta problemas.", "Conexión alternativa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        // conectado con Jet y es .mdb: todo bien
                    }
                }

                try
                {
                    cboTables.Items.Clear();
                    dgvData.DataSource = null;

                    DataTable tablas = conexionActual.CNN.GetSchema("Tables");
                    foreach (DataRow row in tablas.Rows)
                    {
                        string tableType = row["TABLE_TYPE"].ToString();
                        string tableName = row["TABLE_NAME"].ToString();
                        if (string.Equals(tableType, "TABLE", StringComparison.OrdinalIgnoreCase))
                        {
                            if (!tableName.StartsWith("MSys", StringComparison.OrdinalIgnoreCase))
                                cboTables.Items.Add(tableName);
                        }
                    }

                    if (cboTables.Items.Count > 0)
                        cboTables.SelectedIndex = 0;
                    else
                        MessageBox.Show("No se encontraron tablas en la base de datos.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener tablas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                string err = lastError ?? conexionActual?.ObtenerError() ?? "";
                // Mensaje instructivo si parece ser problema de proveedor
                if (err.IndexOf("Microsoft.ACE.OLEDB", StringComparison.OrdinalIgnoreCase) >= 0
                    || err.IndexOf("ACE.OLEDB", StringComparison.OrdinalIgnoreCase) >= 0
                    || err.IndexOf("no está registrado", StringComparison.OrdinalIgnoreCase) >= 0
                    || err.IndexOf("not registered", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    MessageBox.Show(
                        "No se puede conectar: el proveedor OLEDB no está registrado o hay un conflicto de 32/64 bits.\n\n" +
                        "Posibles soluciones:\n" +
                        "1) Instalar 'Microsoft Access Database Engine' (2016 o 2010). Asegúrate de instalar la versión (32/64-bit) que coincida con la plataforma de la aplicación.\n" +
                        "2) Cambiar la plataforma objetivo del proyecto en Visual Studio a x86 (Proyecto -> Propiedades -> Compilar -> Plataforma objetivo = x86) si instalaste la versión de 32 bits.\n" +
                        "3) Si el archivo es .accdb, instala ACE; Jet no soporta .accdb.",
                        "Proveedor no instalado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("No se pudo conectar: " + err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                conexionActual = null;
            }
        }

        private void cboTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (conexionActual == null) return;
            if (cboTables.SelectedItem == null) return;

            string tabla = cboTables.SelectedItem.ToString();
            try
            {
                string sql = $"SELECT * FROM [{tabla}]";
                using (OleDbDataAdapter da = new OleDbDataAdapter(sql, conexionActual.CNN))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvData.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
