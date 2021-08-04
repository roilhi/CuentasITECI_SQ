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
            tbName.Select();
            tbName.Focus();
        }

        private void tbName_KeyDown(object sender, KeyEventArgs e) 
        {
            if (e.KeyCode == Keys.Enter) 
            {
                tbName.Text = tbName.Text.Trim().ToUpper();
                var settings = MongoClientSettings.FromConnectionString("mongodb+srv://itecidb2:iteci2021@clusteriteci.rnxhk.mongodb.net/Prepa_ITECI_Ens?connect=replicaSet");
                //var settings = MongoClientSettings.FromConnectionString("mongodb+srv://itecidb:iteci2021@clusteriteci.rnxhk.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
                var client = new MongoClient(settings);
                var database = client.GetDatabase("UNIENS_Campus_SQ");
                var collection = database.GetCollection<BsonDocument>("Mails_Estudiantes");
                var filter = Builders<BsonDocument>.Filter.Eq("WholeName", tbName.Text.Trim().ToUpper());
                var BsonDoc = collection.Find(filter).FirstOrDefault();
                try 
                {
                    string MoodleUser = BsonDoc["MoodleUser"].AsString;
                    tbUser.Text = MoodleUser;
                    string password = BsonDoc["Password"].AsString;
                    tbPassword.Text = password;
                    string email = BsonDoc["email"].AsString;
                    tbemail.Text = email;
                }
                catch
                {
                    MetroMessageBox.Show(this, "El alumno no existe en los registros de la base de datos o el nombre fue escrito incorrectamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            tbName.Text = " ";
            tbemail.Text = " ";
            tbPassword.Text = " ";
            tbUser.Text = " ";
        }
    }
}
