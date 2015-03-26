using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ArchiveUI
{
    static class Program
    {
       
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {         
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
    
    public partial class Form1
    {
        string file;
        int counter;
        Parameter[] Parameters;

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
            if (Parameters.Count() > 1) {
                foreach (Parameter item in Parameters)
                {
                    if (item.InterfaceName != null) { comboBoxInterfaceName.Items.Add(item.InterfaceName); } else { comboBoxInterfaceName.Items.Add(""); }
                }
            }
        }

        private void FillData()
        {
            ParameterNo.Text = (counter + 1).ToString();
            CleanupForm();
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
        }

        public class Parameter
        {
            public string InterfaceName { get; set; }
            public string ProcesDir { get; set; }
            public string ArchiveDir { get; set; }
            public string Timespan { get; set; }
            public string Recursive { get; set; }
            public string Retention { get; set; }
            public string Include { get; set; }
            public string Exclude { get; set; }
            public string Comments { get; set; }
        }
        
        private Parameter[] GetXMLData()
        {
            file = "archive.xml";
            int c = 0;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(file);
            Parameter[] Params = new Parameter[xmlDoc.DocumentElement.ChildNodes.Count]; //Deze wordt later nog opgeschoond van lege records
            foreach (XmlNode xmlNode in xmlDoc.DocumentElement.ChildNodes)
            {
                if (xmlNode.HasChildNodes)
                {
                    string[] p = new string[9];
                    for (int i = 0; i < xmlNode.ChildNodes.Count; i++)
                    {
                        //Console.WriteLine(xmlNode.ChildNodes[i].Name + ": " + xmlNode.ChildNodes[i].InnerText);
                        switch (xmlNode.ChildNodes[i].LocalName.ToString())
                        {
                            case "InterfaceName":
                                comboBoxInterfaceName.Items.Add(xmlNode.ChildNodes[i].InnerText);
                                p[0] = xmlNode.ChildNodes[i].InnerText;
                                break;
                            case "ProcesDir":
                                p[1] = xmlNode.ChildNodes[i].InnerText;
                                break;
                            case "ArchiveDir":
                                p[2] = xmlNode.ChildNodes[i].InnerText;
                                break;
                            case "Timespan":
                                p[3] = xmlNode.ChildNodes[i].InnerText;
                                break;
                            case "Recursive":
                                p[4] = xmlNode.ChildNodes[i].InnerText;
                                break;
                            case "Retention":
                                p[5] = xmlNode.ChildNodes[i].InnerText;
                                break;
                            case "Include":
                                if (p[6] != null) { p[6] += "\r\n"; }
                                p[6] = p[6] + xmlNode.ChildNodes[i].InnerText;
                                break;
                            case "Exclude":
                                if (p[7] != null) { p[7] += "\r\n"; }
                                p[7] = p[7] + xmlNode.ChildNodes[i].InnerText;
                                break;
                            case "Comments":
                                p[8] = xmlNode.ChildNodes[i].InnerText;
                                break;
                        }
                    }
                    Params[c] = new Parameter()
                    {
                        InterfaceName = p[0],
                        ProcesDir = p[1],
                        ArchiveDir = p[2],
                        Timespan = p[3],
                        Recursive = p[4],
                        Retention = p[5],
                        Include = p[6],
                        Exclude = p[7],
                        Comments = p[8]
                    };
                    c++;
                }
            }
            //Nu de array filteren
            c = 0;
            foreach (Parameter y in Params)
            {
                if (y == null) { Params = Params.Where((source, index) => index != c).ToArray(); } else { c++; }
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

            using (XmlWriter writer = XmlWriter.Create(file, settings))
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
                    writer.WriteElementString("TimeSpan", Parameters[i].Timespan);
                    writer.WriteElementString("Recursive", Parameters[i].Recursive);
                    writer.WriteElementString("Retention", Parameters[i].Retention);

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
    }
}
