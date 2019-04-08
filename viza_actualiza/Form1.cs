using System;
using System.Windows.Forms;
using System.Configuration;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

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
                        return;
                    }
                    else
                    {
                        if(pasa_detalle(dtd) == false)
                        {
                            MessageBox.Show("NO se inserto los datos del detalle");
                            return;
                        }
                    }
                    MessageBox.Show("proceso concluido con exito");
                    Application.Exit();
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
            StringBuilder sCommand = new StringBuilder("INSERT INTO docvtas (" +
                "usercaja,fechope,tipcam,local,docvta,servta,corrvta,doccli,numdcli,direc1,direc2,grem,pedido," +
                "observ,observ2,moneda,subtot,igv,doctot,status,dia,nomcli,telef,codcli,clidv,useranul,fechanul,marca1," +
                "dist,cdr,impreso,mail,tasaigv,simboloMon,autorizsunat,leyenda1,leyenda2,leyenda3,leyenda4,leyenda5,leyenda6," +
                "tipoDocumento,tipoMoneda,numeDocEmi,tipoDocEmi,razonSocEmi,correoEmisor,codiLocAnEmi,tipoDocAdq,totalImp," +
                "totValVtaNetGrav,totalIgv,totalVenta,tipoOperac,nomComEmi,ubigeoEmi,direcEmis,urbEmisor,provinEmi,departEmi," +
                "distriEmi,paisEmisor,razSocAdq,codLeyen1,texLeyen1,codAux401,texAux401,horaEmis,ubigeoAdq,distAdq,provAdq," +
                "dptoAdq,paisAdq,codTipoImp,nomTipoImp,tipoTributo,medPagoAdq,monMedPag1,numOpeFdP,enviado,fectran,medPagAd2," +
                "monMedPag2,numOpeFP2,medPagAd3,monMedPag3,numOpeFP3) VALUES ");
            using (MySqlConnection mConnection = new MySqlConnection(DB_TARJ_STR))
            {
                mConnection.Open();
                List<string> Rows = new List<string>();
                for (int i=0; i<dtc.Rows.Count; i++)
                {
                    DataRow dtcr = dtc.Rows[i];
                    Rows.Add(string.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}', '{9}', '{10}', '{11}', '{12}', '" +
                        "{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}', '{21}', '{22}', '{23}', '{24}', '{25}', '{26}', '{27}', '" +
                        "{28}', '{29}', '{30}', '{31}', '{32}', '{33}', '{34}', '{35}', '{36}', '{37}', '{38}', '{39}', '{40}', '" +
                        "{41}', '{42}', '{43}', '{44}', '{45}', '{46}', '{47}', '{48}', '{49}', '" +
                        "{50}', '{51}', '{52}', '{53}', '{54}', '{55}', '{56}', '{57}', '{58}', '{59}', '" +
                        "{60}', '{61}', '{62}', '{63}', '{64}', '{65}', '{66}', '{67}', '{68}', '{69}', '{70}', '" +
                        "{71}', '{72}', '{73}', '{74}', '{75}', '{76}', '{77}', '{78}', '{79}', '{80}', '{81}', '" +
                        "{82}', '{83}', '{84}', '{85}', '{86}')",
                        MySqlHelper.EscapeString(dtcr[0].ToString()), MySqlHelper.EscapeString(dtcr[1].ToString()),
                        MySqlHelper.EscapeString(dtcr[2].ToString()), MySqlHelper.EscapeString(dtcr[3].ToString()),
                        MySqlHelper.EscapeString(dtcr[4].ToString()), MySqlHelper.EscapeString(dtcr[5].ToString()),
                        MySqlHelper.EscapeString(dtcr[6].ToString()), MySqlHelper.EscapeString(dtcr[7].ToString()),
                        MySqlHelper.EscapeString(dtcr[8].ToString()), MySqlHelper.EscapeString(dtcr[9].ToString()),
                        MySqlHelper.EscapeString(dtcr[10].ToString()), MySqlHelper.EscapeString(dtcr[11].ToString()),
                        MySqlHelper.EscapeString(dtcr[12].ToString()), MySqlHelper.EscapeString(dtcr[13].ToString()),
                        MySqlHelper.EscapeString(dtcr[14].ToString()), MySqlHelper.EscapeString(dtcr[15].ToString()),
                        MySqlHelper.EscapeString(dtcr[16].ToString()), MySqlHelper.EscapeString(dtcr[17].ToString()),
                        MySqlHelper.EscapeString(dtcr[18].ToString()), MySqlHelper.EscapeString(dtcr[19].ToString()),
                        MySqlHelper.EscapeString(dtcr[20].ToString()), MySqlHelper.EscapeString(dtcr[21].ToString()),
                        MySqlHelper.EscapeString(dtcr[22].ToString()), MySqlHelper.EscapeString(dtcr[23].ToString()),
                        MySqlHelper.EscapeString(dtcr[24].ToString()), MySqlHelper.EscapeString(dtcr[25].ToString()),
                        MySqlHelper.EscapeString(dtcr[26].ToString()), MySqlHelper.EscapeString(dtcr[27].ToString()),
                        MySqlHelper.EscapeString(dtcr[28].ToString()), MySqlHelper.EscapeString(dtcr[29].ToString()),
                        MySqlHelper.EscapeString(dtcr[30].ToString()), MySqlHelper.EscapeString(dtcr[31].ToString()),
                        MySqlHelper.EscapeString(dtcr[32].ToString()), MySqlHelper.EscapeString(dtcr[33].ToString()),
                        MySqlHelper.EscapeString(dtcr[34].ToString()), MySqlHelper.EscapeString(dtcr[35].ToString()),
                        MySqlHelper.EscapeString(dtcr[36].ToString()), MySqlHelper.EscapeString(dtcr[37].ToString()),
                        MySqlHelper.EscapeString(dtcr[38].ToString()), MySqlHelper.EscapeString(dtcr[39].ToString()),
                        MySqlHelper.EscapeString(dtcr[40].ToString()), MySqlHelper.EscapeString(dtcr[41].ToString()),
                        MySqlHelper.EscapeString(dtcr[42].ToString()), MySqlHelper.EscapeString(dtcr[43].ToString()),
                        MySqlHelper.EscapeString(dtcr[44].ToString()), MySqlHelper.EscapeString(dtcr[45].ToString()),
                        MySqlHelper.EscapeString(dtcr[46].ToString()), MySqlHelper.EscapeString(dtcr[47].ToString()),
                        MySqlHelper.EscapeString(dtcr[48].ToString()), MySqlHelper.EscapeString(dtcr[49].ToString()),
                        MySqlHelper.EscapeString(dtcr[50].ToString()), MySqlHelper.EscapeString(dtcr[51].ToString()),
                        MySqlHelper.EscapeString(dtcr[52].ToString()), MySqlHelper.EscapeString(dtcr[53].ToString()),
                        MySqlHelper.EscapeString(dtcr[54].ToString()), MySqlHelper.EscapeString(dtcr[55].ToString()),
                        MySqlHelper.EscapeString(dtcr[56].ToString()), MySqlHelper.EscapeString(dtcr[57].ToString()),
                        MySqlHelper.EscapeString(dtcr[58].ToString()), MySqlHelper.EscapeString(dtcr[59].ToString()),
                        MySqlHelper.EscapeString(dtcr[60].ToString()), MySqlHelper.EscapeString(dtcr[61].ToString()),
                        MySqlHelper.EscapeString(dtcr[62].ToString()), MySqlHelper.EscapeString(dtcr[63].ToString()),
                        MySqlHelper.EscapeString(dtcr[64].ToString()), MySqlHelper.EscapeString(dtcr[55].ToString()),
                        MySqlHelper.EscapeString(dtcr[66].ToString()), MySqlHelper.EscapeString(dtcr[67].ToString()),
                        MySqlHelper.EscapeString(dtcr[68].ToString()), MySqlHelper.EscapeString(dtcr[69].ToString()),
                        MySqlHelper.EscapeString(dtcr[70].ToString()), MySqlHelper.EscapeString(dtcr[71].ToString()),
                        MySqlHelper.EscapeString(dtcr[72].ToString()), MySqlHelper.EscapeString(dtcr[73].ToString()),
                        MySqlHelper.EscapeString(dtcr[74].ToString()), MySqlHelper.EscapeString(dtcr[75].ToString()),
                        MySqlHelper.EscapeString(dtcr[76].ToString()), MySqlHelper.EscapeString(dtcr[77].ToString()),
                        MySqlHelper.EscapeString(dtcr[78].ToString()), MySqlHelper.EscapeString(dtcr[79].ToString()),
                        MySqlHelper.EscapeString(dtcr[80].ToString()), MySqlHelper.EscapeString(dtcr[81].ToString()),
                        MySqlHelper.EscapeString(dtcr[82].ToString()), MySqlHelper.EscapeString(dtcr[83].ToString()),
                        MySqlHelper.EscapeString(dtcr[84].ToString()), MySqlHelper.EscapeString(dtcr[85].ToString()),
                        MySqlHelper.EscapeString(dtcr[86].ToString())
                        ));
                }
                sCommand.Append(string.Join(",", Rows));
                sCommand.Append(";");
                using (MySqlCommand myCmd = new MySqlCommand(sCommand.ToString(), mConnection))
                {
                    myCmd.CommandTimeout = 300;
                    myCmd.CommandType = CommandType.Text;
                    myCmd.ExecuteNonQuery();
                    retorna = true;
                }
            }
            return retorna;
        }
        private bool pasa_detalle(DataTable dtd)    // inserta datos del detalle
        {
            bool retorna = false;
            StringBuilder sCommand = new StringBuilder("INSERT INTO detavtas (" +
                "tipdv,servta,corvta,codprd,descrip,precio,cantid,total,precdol,totdol,local,pedido,status,marca1,tasaigv," +
                "tipoDocEmi,numeDocEmi,tipoDocumento,serieNumero,numOrdenItem,cantidad,unidadMed,descripcion,impUniSinImp," +
                "impUniConImp,codImpUniConImp,impTotalImp,montoBaseIgv,codigoIgv,identifIgv,nombreIgv,tipoTribIgv,importeIgv," +
                "codRazonExo,impTotSinImp,codigoProducto,codigoProductoSunat) VALUES ");
            using (MySqlConnection mConnection = new MySqlConnection(DB_TARJ_STR))
            {
                mConnection.Open();
                List<string> Rows = new List<string>();
                for (int i = 0; i < dtd.Rows.Count; i++)
                {
                    DataRow dtcr = dtd.Rows[i];
                    Rows.Add(string.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '" +
                        "{15}', '{16}', '{17}', '{18}', '{19}', '{20}', '{21}', '{22}', '{23}', '" +
                        "{24}', '{25}', '{26}', '{27}', '{28}', '{29}', '{30}', '{31}', '{32}', '" +
                        "{33}', '{34}', '{35}', '{36}')",
                        MySqlHelper.EscapeString(dtcr[0].ToString()), MySqlHelper.EscapeString(dtcr[1].ToString()),
                        MySqlHelper.EscapeString(dtcr[2].ToString()), MySqlHelper.EscapeString(dtcr[3].ToString()),
                        MySqlHelper.EscapeString(dtcr[4].ToString()), MySqlHelper.EscapeString(dtcr[5].ToString()),
                        MySqlHelper.EscapeString(dtcr[6].ToString()), MySqlHelper.EscapeString(dtcr[7].ToString()),
                        MySqlHelper.EscapeString(dtcr[8].ToString()), MySqlHelper.EscapeString(dtcr[9].ToString()),
                        MySqlHelper.EscapeString(dtcr[10].ToString()), MySqlHelper.EscapeString(dtcr[11].ToString()),
                        MySqlHelper.EscapeString(dtcr[12].ToString()), MySqlHelper.EscapeString(dtcr[13].ToString()),
                        MySqlHelper.EscapeString(dtcr[14].ToString()), MySqlHelper.EscapeString(dtcr[15].ToString()),
                        MySqlHelper.EscapeString(dtcr[16].ToString()), MySqlHelper.EscapeString(dtcr[17].ToString()),
                        MySqlHelper.EscapeString(dtcr[18].ToString()), MySqlHelper.EscapeString(dtcr[19].ToString()),
                        MySqlHelper.EscapeString(dtcr[20].ToString()), MySqlHelper.EscapeString(dtcr[21].ToString()),
                        MySqlHelper.EscapeString(dtcr[22].ToString()), MySqlHelper.EscapeString(dtcr[23].ToString()),
                        MySqlHelper.EscapeString(dtcr[24].ToString()), MySqlHelper.EscapeString(dtcr[25].ToString()),
                        MySqlHelper.EscapeString(dtcr[26].ToString()), MySqlHelper.EscapeString(dtcr[27].ToString()),
                        MySqlHelper.EscapeString(dtcr[28].ToString()), MySqlHelper.EscapeString(dtcr[29].ToString()),
                        MySqlHelper.EscapeString(dtcr[30].ToString()), MySqlHelper.EscapeString(dtcr[31].ToString()),
                        MySqlHelper.EscapeString(dtcr[32].ToString()), MySqlHelper.EscapeString(dtcr[33].ToString()),
                        MySqlHelper.EscapeString(dtcr[34].ToString()), MySqlHelper.EscapeString(dtcr[35].ToString()),
                        MySqlHelper.EscapeString(dtcr[36].ToString())
                        ));
                }
                sCommand.Append(string.Join(",", Rows));
                sCommand.Append(";");
                using (MySqlCommand myCmd = new MySqlCommand(sCommand.ToString(), mConnection))
                {
                    myCmd.CommandTimeout = 300;
                    myCmd.CommandType = CommandType.Text;
                    myCmd.ExecuteNonQuery();
                    retorna = true;
                }
            }
            return retorna;
        }
    }
}
