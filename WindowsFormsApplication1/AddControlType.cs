using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
   
    public partial class AddControlType : Form
    {
        ControlTypeRepo controltype = new ControlTypeRepo();
        public AddControlType()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;  
        }

        private void btnAddd_Click(object sender, EventArgs e)
        {
             controltype.controltype.Add(new ControlType());
            SaveControlType();
            LoadControlType();
            dataGridView1.DataSource = controltype.controltype;
        }

        private void AddControlType_Load(object sender, EventArgs e)
        {
            LoadControlType();
            dataGridView1.DataSource = controltype.controltype;
        }



        private void SaveControlType()
        {
            var text = Helper.ConvertObjectToXML(controltype);
            System.IO.File.WriteAllText(@"F:\\Controltype.xml", text);

        }

        private void LoadControlType()
        {

            if (File.Exists(@"F:\\Controltype.xml"))
            {
                var txxx = System.IO.File.ReadAllText(@"F:\\Controltype.xml");
                controltype = Helper.ConvertXMLDataToEntity<ControlTypeRepo>(txxx);
            }
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            
            controltype.controltype.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
            SaveControlType();
            LoadControlType();
            dataGridView1.DataSource = controltype.controltype;
        
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            SaveControlType();
        
        }
    }

    
}
