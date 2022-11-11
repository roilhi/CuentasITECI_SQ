using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Xml.Linq;

namespace CuentasITECI_SQ
{
    public partial class Form1 : MetroForm 
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            tbLastName.Select();
            tbLastName.Focus();
            MessageBox.Show("Ingresa los apellidos del estudiante");
        }

        private string ClaveCarrera(string clave)
        {
            string Carrera = "";
            if (clave == "LAITE")
            {
                Carrera = "LICENCIADO EN ADMINISTRACIÓN DE EMPRESAS Y LOGÍSTICA";
            }
            else if (clave == "LCEITE")
            {
                Carrera = "LICENCIADO EN CIENCIAS DE LA EDUCACION";
            }
            else if (clave == "IDITE")
            {
                Carrera = "INGENIERIA INDUSTRIAL";
            }
            else if (clave == "LDITE")
            {
                Carrera = "LICENCIADO EN DERECHO";
            }
            else if (clave == "LFITE")
            {
                Carrera = "LICENCIADO EN FILOSOFÍA";
            }
            else if (clave == "LMAITE")
            {
                Carrera = "LICENCIADO EN MEDIOS AUDIOVISUALES";
            }
            else if (clave == "LNITE")
            {
                Carrera = "LICENCIADO EN NEGOCIOS INTERNACIONALES";
            }
            else if (clave == "LCRITE")
            {
                Carrera = "LICENCIADO EN CRIMINOLOGÍA";
            }
            return Carrera;
        }
        private void tbLastName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tbLastName.Text = tbLastName.Text.Trim().ToUpper();
                MessageBox.Show("Ingresa los nombres del estudiante");
                tbName.Select();
                tbName.Focus();
            }
        }
        private void tbName_KeyDown(object sender, KeyEventArgs e) 
                {
                    if (e.KeyCode == Keys.Enter) 
                    {
                        tbName.Text = tbName.Text.Trim().ToUpper();
                        // cluster mongo sistemas UNIENS
                        // mongodb+srv://uniens:uniens22@cluster0.ex5nn.mongodb.net/?retryWrites=true&w=majority?connect=replicaSet
                        // var settings = MongoClientSettings.FromConnectionString("mongodb+srv://itecidb2:iteci2021@clusteriteci.rnxhk.mongodb.net/Prepa_ITECI_Ens?connect=replicaSet");
                        string wName = tbLastName.Text + " " + tbName.Text;
                        var settings = MongoClientSettings.FromConnectionString("mongodb+srv://uniens:uniens22@cluster0.ex5nn.mongodb.net/?retryWrites=true&w=majority?connect=replicaSet");
                        var client = new MongoClient(settings);
                        var database = client.GetDatabase("studSQ");
                        var collection = database.GetCollection<BsonDocument>("infoMails");
                        var filter = Builders<BsonDocument>.Filter.Eq("wholeName", wName);
                        var BsonDoc = collection.Find(filter).FirstOrDefault();
               try
                {
                    //string MoodleUser = BsonDoc["MoodleUser"].AsString;
                    //tbUser.Text = MoodleUser;
                    if (BsonDoc != null)
                    {
                        string password = BsonDoc["password"].AsString;
                        tbPassword.Text = password;
                        string email = BsonDoc["mail"].AsString;
                        tbemail.Text = email;
                        string matInt = BsonDoc["matInt"].AsString;
                        tbMat.Text = matInt;
                        string clave = BsonDoc["carrera"].AsString;
                        tbCarrera.Text = ClaveCarrera(clave);
                    }
                    else 
                    {
                        MetroMessageBox.Show(this, "El usuario no existe, favor de verificar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                        }
                        catch
                        {
                            MetroMessageBox.Show(this, "Error en la conexión con la base de datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                }
        
        private void btnBorrar_Click(object sender, EventArgs e)
        {
            tbLastName.Text = " ";
            tbName.Text = " ";
            tbemail.Text = " ";
            tbPassword.Text = " ";
            tbMat.Text = " ";
            tbCarrera.Text = " ";
            tbMat.Text = " ";
        }
    }
}
