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

        //Hier de functies voor het vullen van de velden
        //Leegmaken en vullen van het formulier
        private void CleanupForm()
        {
            comboBoxInterfaceName.Text = "";
            textBoxProcesDir.Text = "";
            textBoxArchiveDir.Text = "";
            comboBoxTimespan.Text = "";
            checkBoxRecursive.Checked = false;
            textBoxRetention.Text = "";
            textBoxInclude.Text = "";
            textBoxExclude.Text = "";
            textBoxComments.Text = "";
        }

        private void FillComboBox()
        {
            comboBoxInterfaceName.Items.Clear();
            if (Parameters.Count() > 1)
            {
                foreach (Parameter item in Parameters)
                {
                    if (item.InterfaceName != null) { comboBoxInterfaceName.Items.Add(item.InterfaceName); } else { comboBoxInterfaceName.Items.Add(""); }
                }
            }
        }

        private void FillData()
        {
            
            CleanupForm();
            ParameterNo.Text = (counter + 1).ToString();
            FillComboBox();
            comboBoxInterfaceName.Text = Parameters[counter].InterfaceName;
            textBoxProcesDir.Text = Parameters[counter].ProcesDir;
            textBoxArchiveDir.Text = Parameters[counter].ArchiveDir;
            comboBoxTimespan.Text = Parameters[counter].Timespan; 
            if (Parameters[counter].Recursive == "true") { checkBoxRecursive.Checked = true; }
            textBoxRetention.Text = Parameters[counter].Retention;
            textBoxInclude.Text = Parameters[counter].Include;
            textBoxExclude.Text = Parameters[counter].Exclude;
            textBoxComments.Text = Parameters[counter].Comments;
            ValidateForm();
            
        }

       
        private void ValidateForm()
        {
                errorProvider1.Clear();

            
                if (textBoxProcesDir.Text == "")
                {
                    errorProvider1.SetError(textBoxProcesDir, textBoxProcesDir.Name + "Needs to contain a value.");
                }
                
                if (comboBoxInterfaceName.Text == "")
                {
                    errorProvider1.SetError(comboBoxInterfaceName, comboBoxInterfaceName.Name + "Needs to contain a value.");
                }
                double num;
                if ( !double.TryParse(textBoxRetention.Text, out num))
                {
                    errorProvider1.SetError(textBoxRetention, textBoxRetention.Name + "Needs to contain a value.");
                }
             }

        private Boolean Valid()
        { 
            ValidateForm();
            if (errorProvider1.GetError(textBoxProcesDir) == "") { return true; } else { return true; }
        }

        //Hier komende de akties aan de knoppen
        //Navigatie
        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (Parameters.Count()-1 > counter && Valid())
            {
                counter++;
                FillData();
            }
            //textBoxComments.Text = errorProvider1.GetError(textBoxProcesDir);

        }

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            if (counter > 0 && Valid())
            {
                counter--;
                FillData();
            }
        }

        private void comboBoxInterfaceName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Valid())
            {
                counter = comboBoxInterfaceName.SelectedIndex;
                FillData();
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (Valid())
            {
                CleanupForm();
                ParameterNo.Text = (Parameters.Count() + 1).ToString();
                counter = Parameters.Count();
                Array.Resize(ref Parameters, counter + 1);
                Parameters[counter] = new Parameter() { };
                FillComboBox();
            }
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
            if (Valid())
            {
                WriteXMLData();
            }
        }

        private void toolStripMenuLoad_Click(object sender, EventArgs e)
        {
            //XML opnieuw inladen
            CleanupForm();
            counter = 0;
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

        private void textBoxProcesDir_TextChanged(object sender, EventArgs e)
        {           
            Parameters[counter].ProcesDir = textBoxProcesDir.Text;
            ValidateForm(); 
        }

        private void comboBoxTimespan_SelectedIndexChanged(object sender, EventArgs e)
        {
            Parameters[counter].Timespan = comboBoxTimespan.Text;
            //ValidateForm(); 
        }

        private void textBoxRetention_TextChanged(object sender, EventArgs e)
        {
            if ((counter+1).ToString() == ParameterNo.Text)
            { 
                Parameters[counter].Retention = textBoxRetention.Text;
                textBoxComments.Text = Parameters[1].Retention;
                ValidateForm();
            }
        }
    }
}
