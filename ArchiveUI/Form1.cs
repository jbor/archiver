using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace ArchiveUI
{

    public partial class Form1 : Form
    {
        string path = "archive.xml";
        Boolean Enable_edit;
              
        public Form1()
        {
            InitializeComponent();
            counter = 0;
            //Parameters = GetXMLData();
            Parameters = GetXMLData2();
            FillData();
        }


            int counter;
            //Parameter[] Parameters;
            List<Parameter> Parameters;

            public class Parameter
            {
                //Eigenlijk moet zo'n beetje alles er als een string uitkomen, om in de form velden te worden geprint, 
                //maar voor de validatie is het wel netjes om de items de normale waardes te geven.
                private string interfacename;
                private string procesdir;
                private string archivedir;
                private int timespan;
                private bool recursive;
                private int retention;
                private string include;
                private string exclude;
                private string comments;

                public string InterfaceName { 
                    get 
                    {
                        return interfacename;  
                    }
                    set
                    {
                        interfacename = value;
                    }
                }

                public string Comments
                {
                    get { return comments; }
                    set { comments = value; }
                }

                public string Exclude
                {
                    get { return exclude; }
                    set { exclude = value; }
                }

                public string Include
                {
                    get { return include; }
                    set { include = value; }
                }
                
                public string Retention
                {
                    get { return retention.ToString(); }
                    set { retention = Convert.ToInt32(value); }
                }


                public bool Recursive
                {
                    get { return recursive; }
                    set { recursive = value; }
                }                


                public int Timespan
                {
                    get 
                    {
                        return timespan;                    
                    }
                    set
                    {
                       timespan = value;
                    
                    }
                }
                
                public string ArchiveDir
                {
                    get { return archivedir; }
                    set { archivedir = value; }
                }
                
                public string ProcesDir
                {
                    get { return procesdir; }
                    set { procesdir = value; }
                }

                //Constructor met converter voor Timespan
                public Parameter(string Timespan)
                {
                    switch (Timespan)
                    {
                        case "D":
                            timespan = 0;
                            break;
                        case "W":
                            timespan = 1;
                            break;
                        case "M":
                            timespan = 2;
                            break;
                        case "Y":
                            timespan = 3;
                            break;
                        default:
                            timespan = Convert.ToInt32(Timespan);
                            break;
                    }
                }

            }

            private List<Parameter> GetXMLData2()
            {
                            
                List<Parameter> Params = new List<Parameter>();
                XmlDocument xDoc = new XmlDocument();
                
                xDoc.Load(path);
                XmlNodeList ParametersDataSet = xDoc.SelectNodes("ParametersDataSet/Parameter");
                foreach (XmlElement p in ParametersDataSet)
                {
                    Parameter x = new Parameter(p.SelectSingleNode("TimeSpan").InnerText);
                    x.InterfaceName = p.SelectSingleNode("InterfaceName").InnerText;
                    x.ArchiveDir = p.SelectSingleNode("ArchiveDir").InnerText;
                    x.ProcesDir = p.SelectSingleNode("ProcesDir").InnerText;
                    x.Retention = p.SelectSingleNode("Retention").InnerText;
                    x.Recursive = Convert.ToBoolean(p.SelectSingleNode("Recursive").InnerText);
                    x.Comments = p.SelectSingleNode("Comments").InnerText;
 

                    if (null != p.SelectSingleNode("Include"))
                    {
                        x.Include = p.SelectSingleNode("Include").InnerText.ToString();
                    }

                    if (null != p.SelectSingleNode("Exclude"))
                    {
                       x.Exclude = p.SelectSingleNode("Exclude").InnerText.ToString();
                    }
                   
                    Params.Add(x);
                }
                return Params;
            }

            private void WriteXMLData()
            {
                //Mooie output instellen
                var settings = new XmlWriterSettings();
                settings.OmitXmlDeclaration = true;
                settings.Indent = true;
                settings.NewLineOnAttributes = true;

                using (XmlWriter writer = XmlWriter.Create(path, settings))
                {
                    //Wegschrijven naar XML
                    writer.WriteStartDocument();
                    writer.WriteStartElement("ParametersDataSet");
                    for (int i = 0; i < Parameters.Count(); i++)
                    {
                        writer.WriteStartElement("Parameter");
                        writer.WriteElementString("InterfaceName", Parameters[i].InterfaceName);
                        writer.WriteElementString("ProcesDir", Parameters[i].ProcesDir);
                        writer.WriteElementString("ArchiveDir", Parameters[i].ArchiveDir);
                        switch (Parameters[i].Timespan)
                        {
                            case 0:
                                writer.WriteElementString("TimeSpan", "D");
                                break;
                            case 1:
                                writer.WriteElementString("TimeSpan", "W");
                                break;
                            case 2:
                                writer.WriteElementString("TimeSpan", "M");
                                break;
                            case 3:
                                writer.WriteElementString("TimeSpan", "Y");
                                break;
                            default:
                                writer.WriteElementString("TimeSpan", "D");
                                break;
                        }
                        writer.WriteElementString("Recursive", Parameters[i].Recursive.ToString());
                        writer.WriteElementString("Retention", Parameters[i].Retention.ToString());

                        if (Parameters[i].Include != null)
                        {
                            string[] include = Parameters[i].Include.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string x in include)
                            {
                                writer.WriteElementString("Include", x);
                            }
                        }

                        if (Parameters[i].Exclude != null)
                        {
                            string[] exclude = Parameters[i].Exclude.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string x in exclude)
                            {
                                writer.WriteElementString("Exclude", x);
                            }
                        }
                        writer.WriteElementString("Comments", Parameters[i].Comments);
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
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
            comboBoxTimespan.SelectedIndex = Convert.ToInt16(Parameters[counter].Timespan);
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
  

                Parameters.Add(new Parameter("1") {});

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
                 Parameters.RemoveAt(counter);

            }
            if (counter == Parameters.Count() && counter > 0) { counter--; }        
            if (Parameters.Count() > 0) { FillData(); }
            if (Parameters.Count() == 0)
            {
                Parameters.Add(new Parameter("0"));
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
            Parameters = GetXMLData2();
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
               if (double.TryParse(textBoxRetention.Text, out num)) { Parameters[counter].Retention = Convert.ToInt16(textBoxRetention.Text).ToString(); }
               Parameters[counter].Recursive = checkBoxRecursive.Checked;
                Parameters[counter].Include = textBoxInclude.Text;
                Parameters[counter].Exclude = textBoxExclude.Text;
                Parameters[counter].Timespan = comboBoxTimespan.SelectedIndex;
                Console.WriteLine(comboBoxTimespan.SelectedIndex.ToString());
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
