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
    public partial class AddWindow : Form
    {

        WindowRepository controlWindow = new WindowRepository();
        public AddWindow()
        {
            InitializeComponent();

            dataGridView1.AutoGenerateColumns = false;            
        }

        private void AddWindow_Load(object sender, EventArgs e)
        {
            LoadWindowName();
            dataGridView1.DataSource = controlWindow.window;
        }


        private void SaveWindowName()
        {
            var text = Helper.ConvertObjectToXML(controlWindow);
            System.IO.File.WriteAllText(@"F:\\WindowName.xml", text);

        }

        private void LoadWindowName()
        {

            if (File.Exists(@"F:\\WindowName.xml"))
            {
                var txxx = System.IO.File.ReadAllText(@"F:\\WindowName.xml");
                controlWindow = Helper.ConvertXMLDataToEntity<WindowRepository>(txxx);
            }
        }

        private void btnAddd_Click(object sender, EventArgs e)
        {
            controlWindow.window.Add(new Window());
            SaveWindowName();
            LoadWindowName();
            dataGridView1.DataSource = controlWindow.window;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveWindowName();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            controlWindow.window.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
            SaveWindowName();
            LoadWindowName();
            dataGridView1.DataSource = controlWindow.window;
        }
    }
}
