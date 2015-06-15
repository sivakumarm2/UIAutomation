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
    public partial class Form1 : Form
    {
        ControlRepository objconfig = new ControlRepository();
        WindowRepository controlWindow = new WindowRepository();
        ControlTypeRepo controltype = new ControlTypeRepo();
        

        public Form1()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;   
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadOjectConfig();
            dataGridView1.DataSource = objconfig.controls;

        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveOjectConfig();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var newControl = new ControlInfo();
            newControl.controlId = objconfig.contorlNextId;

            objconfig.controls.Add(newControl);            
            SaveOjectConfig();
            LoadOjectConfig();
            dataGridView1.DataSource = objconfig.controls;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
               
                objconfig.controls.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
                SaveOjectConfig();
                LoadOjectConfig();
                dataGridView1.DataSource = objconfig.controls;
            
            }           

        }

        private void SaveOjectConfig()
        {
            var text = Helper.ConvertObjectToXML(objconfig);
            System.IO.File.WriteAllText(@"F:\\ObjectRepository.xml", text);
        
        }

        private void LoadOjectConfig()
        {
            LoadWindow();
            
           
            if (File.Exists(@"F:\\ObjectRepository.xml"))
            {
                var txxx = System.IO.File.ReadAllText(@"F:\\ObjectRepository.xml");
                objconfig = Helper.ConvertXMLDataToEntity<ControlRepository>(txxx);
                var windows=objconfig.controls.Where(w=>w.ControlType=="Window");
                var abc = objconfig.controls.Where(c => windows.Contains(c)).ToList();
                var ll = abc.Count();

            }
            LoadControlType();
        
        }

        private static string getObjectPath(int ControlId,ControlRepository objconfig)
        {
            string stPath="";
            bool stWindow=false;
            int controlCount=0;
          
            do
            {
                controlCount++;
                var objControl = objconfig.controls.First(c => c.controlId == ControlId);
                stPath = stPath + objControl.controlName + "/";
                ControlId = objControl.parentId;
                if (objControl.parentId == 0)
                {
                    stWindow = true;
                }

            }
            while (stWindow == false);//|| controlCount <= 10

            return stPath.TrimEnd('/');

        }

        private void LoadWindow()
        {

            if (File.Exists(@"F:\\WindowName.xml"))
            {
                var txxx = System.IO.File.ReadAllText(@"F:\\WindowName.xml");
                controlWindow = Helper.ConvertXMLDataToEntity<WindowRepository>(txxx);
            }

            ((DataGridViewComboBoxColumn)dataGridView1.Columns["Column1"]).DataSource = controlWindow.window;
            ((DataGridViewComboBoxColumn)dataGridView1.Columns["Column1"]).DisplayMember = "Name";
           
        }

        private void LoadControlType()
        {
            if (File.Exists(@"F:\\Controltype.xml"))
            {
                var txxx = System.IO.File.ReadAllText(@"F:\\Controltype.xml");
                controltype = Helper.ConvertXMLDataToEntity<ControlTypeRepo>(txxx);
            }

            ((DataGridViewComboBoxColumn)dataGridView1.Columns["Column2"]).DataSource = controltype.controltype;
            ((DataGridViewComboBoxColumn)dataGridView1.Columns["Column2"]).DisplayMember = "Type";

            var parents = (from x in objconfig.controls
                          select (new { Text = Form1.getObjectPath(x.controlId,objconfig), Id = x.controlId })).ToList();
            parents.Insert(0, new { Text = "", Id = 0 });

            ((DataGridViewComboBoxColumn)dataGridView1.Columns["Column4"]).DataSource = parents;
            ((DataGridViewComboBoxColumn)dataGridView1.Columns["Column4"]).DisplayMember = "Text";
            ((DataGridViewComboBoxColumn)dataGridView1.Columns["Column4"]).ValueMember = "Id";


        }

     

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var obj=objconfig.controls[this.dataGridView1.SelectedRows[0].Index];
            MessageBox.Show(getObjectPath(obj.controlId,objconfig));
        }

      


        
    }
}
