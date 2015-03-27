using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArchiveUI
{

    public partial class Form1 : Form
    {

                 
        public Form1()
        {
            InitializeComponent();
            counter = 0;
            Parameters = GetXMLData();
            FillData();
        }

 
        //Hier komende de akties aan de knoppen
        //Navigatie
        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (Parameters.Count()-1 > counter)
            {
                counter++;
                FillData();
            } 
        }

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            if (counter > 0)
            {
                counter--;
                FillData();
            }
        }

        private void comboBoxInterfaceName_SelectionChangeCommitted(object sender, EventArgs e)
        {
             counter = comboBoxInterfaceName.SelectedIndex;
             FillData();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            CleanupForm();
            ParameterNo.Text = (Parameters.Count()+1).ToString();
            counter = Parameters.Count();
            Array.Resize(ref Parameters, counter + 1);
            Parameters[counter] = new Parameter() { };
            FillComboBox();
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            //Zootje, maar het werkt...
            CleanupForm();
            if (Parameters.Count() > 0) { Parameters = Parameters.Where((source, index) => index != counter).ToArray(); }
            if (counter == Parameters.Count() && counter > 0) { counter--; }        
            if (Parameters.Count() > 0) { FillData(); }
            if (Parameters.Count() == 0)
            {
                Array.Resize(ref Parameters, 1);
                Parameters[0] = new Parameter() { };
            }
        }

        //Menu
        private void toolStripSave_Click(object sender, EventArgs e)
        {
            WriteXMLData();            
        }

        private void toolStripMenuLoad_Click(object sender, EventArgs e)
        {
            //XML opnieuw inladen
            CleanupForm();
            Parameters = GetXMLData();
            FillData();
        }

        private void toolStripExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripAbout_Click(object sender, EventArgs e)
        {
            new FormAbout().ShowDialog();
        }

        //Aanpassingen in de velden valideren en bijwerken, kijken of dit makkelijker/korter kan in C#...
        private void comboBoxInterfaceName_Validated(object sender, EventArgs e)
        {
            Parameters[counter].InterfaceName = comboBoxInterfaceName.Text;
            FillComboBox();
        }

        private void textBoxProcesDir_Validated(object sender, EventArgs e)
        {
            Parameters[counter].ProcesDir = textBoxProcesDir.Text;
        }

        private void textBoxArchiveDir_Validated(object sender, EventArgs e)
        {
            Parameters[counter].ArchiveDir = textBoxArchiveDir.Text;
        }

        private void comboBoxTimespan_Validated(object sender, EventArgs e)
        {
            Parameters[counter].Timespan = comboBoxTimespan.Text;
        }

        private void checkBoxRecursive_Validated(object sender, EventArgs e)
        {
            if (checkBoxRecursive.Checked == true) { Parameters[counter].Recursive = "true"; } else { Parameters[counter].Recursive = "false"; }            
        }

        private void textBoxRetention_Validated(object sender, EventArgs e)
        {
            Parameters[counter].Retention = textBoxRetention.Text;
        }

        private void textBoxInclude_Validated(object sender, EventArgs e)
        {
            Parameters[counter].Include = textBoxInclude.Text;
        }

        private void textBoxExclude_Validated(object sender, EventArgs e)
        {
            Parameters[counter].Exclude = textBoxExclude.Text;
        }

        private void textBoxComments_Validated(object sender, EventArgs e)
        {
            Parameters[counter].Comments = textBoxComments.Text;
        }


    }
}
