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

        Boolean Enable_edit;
              
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
            textBoxInterfaceName.Text = "";
            textBoxProcesDir.Text = "";
            textBoxArchiveDir.Text = "";
            comboBoxTimespan.SelectedIndex = 0;
            checkBoxRecursive.Checked = false;
            textBoxRetention.Text = "0";
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
                    comboBoxInterfaceName.Items.Add(item.InterfaceName!=null ? item.InterfaceName : ""); 
                }
            }
        }

        private void FillData()
        {
            Enable_edit = false;
            CleanupForm();
            
            FillComboBox();
            textBoxInterfaceName.Visible = false;
            comboBoxInterfaceName.Visible = true;
            comboBoxInterfaceName.Text = Parameters[counter].InterfaceName;
            textBoxProcesDir.Text = Parameters[counter].ProcesDir;
            textBoxArchiveDir.Text = Parameters[counter].ArchiveDir;
            comboBoxTimespan.SelectedIndex = Parameters[counter].Timespan; 
            checkBoxRecursive.Checked = Parameters[counter].Recursive;
            textBoxRetention.Text = Parameters[counter].Retention.ToString();
            textBoxInclude.Text = Parameters[counter].Include;
            textBoxExclude.Text = Parameters[counter].Exclude;
            textBoxComments.Text = Parameters[counter].Comments;
            ParameterNo.Text = (counter + 1).ToString();
            Enable_edit = true;
        }

       
        private void ValidateForm()
        {
                errorProvider1.Clear();           
                if (textBoxProcesDir.Text == "")
                {
                    errorProvider1.SetError(textBoxProcesDir, textBoxProcesDir.Name + "Needs to contain a value.");
                }

                if (comboBoxInterfaceName.Text == "" && comboBoxInterfaceName.Visible == true || textBoxInterfaceName.Text == "" && textBoxInterfaceName.Visible == true)
                {
                    textBoxInterfaceName.Visible = true;
                    comboBoxInterfaceName.Visible = false;
                    errorProvider1.SetError(textBoxInterfaceName, textBoxInterfaceName.Name + "Needs to contain a value.");                    
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
            if (errorProvider1.GetError(textBoxProcesDir) == "" && errorProvider1.GetError(textBoxRetention) == "" && errorProvider1.GetError(comboBoxInterfaceName) == "" && errorProvider1.GetError(textBoxInterfaceName) == "") 
            {
                return true; } else 
            {
                return false; 
            }
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
                Enable_edit = false;
                counter = (Parameters.Count());
                CleanupForm();             
                ParameterNo.Text = (Parameters.Count() + 1).ToString();
                Array.Resize(ref Parameters, counter + 1);
                Parameters[counter] = new Parameter() { };
                Parameters[counter].Timespan = 1;
                FillComboBox();
                ValidateForm();
                Enable_edit = true;
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            //Zootje, maar het werkt... misschien nog eens naar kijken
            CleanupForm();
            if (Parameters.Count() > 0) 
            { 
                Parameters = Parameters.Where((source, index) => index != counter).ToArray();
            }
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
        public void boxChanged()
        {
            double num;
            if (Enable_edit)
            {                
                Parameters[counter].InterfaceName = comboBoxInterfaceName.Text;
                Parameters[counter].Comments = textBoxComments.Text;
                Parameters[counter].ProcesDir = textBoxProcesDir.Text;
                Parameters[counter].ArchiveDir = textBoxArchiveDir.Text;
                if (double.TryParse(textBoxRetention.Text, out num)) { Parameters[counter].Retention = Convert.ToInt16(textBoxRetention.Text); }
                Parameters[counter].Recursive = checkBoxRecursive.Checked;
                Parameters[counter].Include = textBoxInclude.Text;
                Parameters[counter].Exclude = textBoxExclude.Text;
                Parameters[counter].Timespan = comboBoxTimespan.SelectedIndex;
            }  
            ValidateForm();         
        }

        private void comboBoxTimespan_SelectedIndexChanged(object sender, EventArgs e)
        {
            boxChanged();
        }

        private void textBoxProcesDir_TextChanged(object sender, EventArgs e)
        {
            boxChanged();
        }        
               
        private void textBoxRetention_TextChanged(object sender, EventArgs e)
        {
            boxChanged();
        }

        private void comboBoxInterfaceName_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxInterfaceName.Text == "")
            {
                textBoxInterfaceName.Visible = true;
                comboBoxInterfaceName.Visible = false;
            }
            else
            {
                boxChanged();
            }     
        }

        private void textBoxArchiveDir_TextChanged(object sender, EventArgs e)
        {
            boxChanged();     
        }

        private void textBoxComments_TextChanged(object sender, EventArgs e)
        {
            boxChanged();
        }

        private void checkBoxRecursive_CheckedChanged(object sender, EventArgs e)
        {
            boxChanged();
        }

        private void textBoxInclude_TextChanged(object sender, EventArgs e)
        {
            boxChanged();
        }

        private void textBoxExclude_TextChanged(object sender, EventArgs e)
        {
            boxChanged();
        }


        private void textBoxInterfaceName_TextChanged(object sender, EventArgs e)
        {
            if (Enable_edit)
            { 
                if (textBoxInterfaceName.Text != "") 
                {
                    comboBoxInterfaceName.Text = textBoxInterfaceName.Text;
                    textBoxInterfaceName.Text = "";
                    textBoxInterfaceName.Visible = false;
                    comboBoxInterfaceName.Visible = true;
                    comboBoxInterfaceName.Select();
                    //cursor verder laten gaan met de combobox.
                    comboBoxInterfaceName.Select(comboBoxInterfaceName.Text.Length, 0);     
                } 
                else { errorProvider1.SetError(textBoxInterfaceName, textBoxInterfaceName.Name + "Needs to contain a value."); }
            }
        }
    }
}
