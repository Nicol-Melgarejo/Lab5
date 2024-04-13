using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LAB05
{
    /// <summary>
    /// Lógica de interacción para Listar.xaml
    /// </summary>
    public partial class Listar : Window
    {
        public Listar()
        {
            InitializeComponent();
            string connectionString = "Data Source=LAB1504-24\\SQLEXPRESS;Initial Catalog=Nepunobd; User Id=User01;Password=123456";

            List<Lista> empleados = new List<Lista>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("USP_ListarEmpleados", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int IdEmpleado = reader.GetInt32(reader.GetOrdinal("IdEmpleado"));
                                string Apellidos = reader.GetString(reader.GetOrdinal("Apellidos"));
                                string Nombre = reader.GetString(reader.GetOrdinal("Nombre"));
                                string cargo = reader.GetString(reader.GetOrdinal("cargo"));

                                empleados.Add(new Lista { IdEmpleado = IdEmpleado, Apellidos = Apellidos, Nombre = Nombre, cargo = cargo });
                            }
                        }
                    }
                }

                dataGridEmpleados.ItemsSource = empleados;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public class Lista
        { 
            public int IdEmpleado { get; set; }
            public string Apellidos { get; set; }
            public string Nombre { get; set; }
            public string cargo { get; set; }

    }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window ventanaActual = Window.GetWindow(this);
            ventanaActual.Close();
        }
    }
}