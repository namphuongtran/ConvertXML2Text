using AttendanceApp.Common;
using AttendanceApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AttendanceApp
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnSource_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile;
            openFile = new OpenFileDialog();
            openFile.Filter = "XML files *.xml|*.xml";
            openFile.Title = "Select a file";
            if (openFile.ShowDialog() != DialogResult.OK)
                return;

            txtSource.Text = openFile.FileName;
        }

        private void btnDesctination_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile;
            saveFile = new SaveFileDialog();
            saveFile.Filter = "Text files (*.txt)|*.txt";
            saveFile.Title = "Save Text File";
            if (saveFile.ShowDialog() != DialogResult.OK)
                return;
            txtDestination.Text = saveFile.FileName;
        }

        private void Debug(string message)
        {
            txtDebugBox.AppendText(message + Environment.NewLine);
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            Debug("Started ...");
            Debug("Checking source file ...");

            if (!string.IsNullOrEmpty(txtSource.Text))
            {
                Debug("XML File OK");
            }
            else
            {
                Debug("Invalid XML File");
            }

            if (!string.IsNullOrEmpty(txtDestination.Text))
            {
                Debug("Text File OK");
            }
            else
            {
                Debug("Invalid Text File");
            }

            if (!string.IsNullOrEmpty(txtSource.Text) && !string.IsNullOrEmpty(txtDestination.Text))
            {
                Debug("Converting document ... ");
                List<Tracking> trackings = Utility.ReadXMLFile(txtSource.Text);
                if (trackings != null && trackings.Count > 0)
                {
                    Utility.WriteLog(trackings, txtDestination.Text);
                    Debug("Done!!!");
                }
            }
            else
            {
                Debug("Please check the path of XML file or text file!");
            }            
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }
    }
}
