using System;
using System.Windows.Forms;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace viza_actualiza
{
    public partial class Form1 : Form
    {
        // own database connection
        static string serv = ConfigurationManager.AppSettings["serv"].ToString();
        static string port = ConfigurationManager.AppSettings["port"].ToString();
        static string usua = ConfigurationManager.AppSettings["user"].ToString();
        static string cont = ConfigurationManager.AppSettings["pass"].ToString();
        static string data = ConfigurationManager.AppSettings["data"].ToString();
        static string ctl = ConfigurationManager.AppSettings["ConnectionLifeTime"].ToString();
        // target database connection
        static string srvtar = ConfigurationManager.AppSettings["srvtar"].ToString();
        static string ptotar = ConfigurationManager.AppSettings["ptotar"].ToString();
        static string usrtar = ConfigurationManager.AppSettings["usrtar"].ToString();
        static string pswtar = ConfigurationManager.AppSettings["pswtar"].ToString();
        static string bd_tar = ConfigurationManager.AppSettings["bd_tar"].ToString();
        //
        string DB_CONN_STR = "server=" + serv + ";port=" + port + ";uid=" + usua + ";pwd=" + cont + ";database=" + data +
                        ";ConnectionLifeTime=" + ctl + ";";
        string DB_TARJ_STR = "server=" + srvtar + ";port=" + ptotar + ";uid=" + usrtar + ";pwd=" + pswtar + ";database=" + bd_tar +
                        ";ConnectionLifeTime=" + ctl + ";";

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // nothing to do
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var aa = MessageBox.Show("Confirma que desea pasar los datos", "Confirma",MessageBoxButtons.YesNo);
            if(aa == DialogResult.Yes)
            {
                string concabeza = "select * from docvtas";
                string condetalle = "select * from detavtas";
                DataTable dtc = new DataTable();
                DataTable dtd = new DataTable();
                MySqlConnection conn = new MySqlConnection(DB_CONN_STR);
                conn.Open();
                if(conn.State == ConnectionState.Open)
                {
                    MySqlCommand micon = new MySqlCommand(concabeza, conn);
                    MySqlDataAdapter da = new MySqlDataAdapter(micon);
                    da.Fill(dtc);
                    //
                    micon = new MySqlCommand(condetalle, conn);
                    da = new MySqlDataAdapter(micon);
                    da.Fill(dtd);
                    //
                    if(pasa_cabeza(dtc) == false)
                    {
                        MessageBox.Show("NO se inserto los datos de cabecera");
                    }
                    else
                    {
                        if(pasa_detalle(dtd) == false)
                        {
                            MessageBox.Show("NO se inserto los datos del detalle");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo conectar al servidor propio", "Error de conectividad");
                }
            }
        }
        private bool pasa_cabeza(DataTable dtc)     // inserta los datos de cabecera
        {
            bool retorna = false;

            return retorna;
        }
        private bool pasa_detalle(DataTable dtd)    // inserta datos del detalle
        {
            bool retorna = false;

            return retorna;
        }
    }
}
