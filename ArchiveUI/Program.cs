using System;
using System.Collections.Generic;
using System.IO;
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
    
        public class Parameter
        {
            public string InterfaceName { get; set; }
            public string ProcesDir { get; set; }
            public string ArchiveDir { get; set; }
            public int Timespan { get; set; }
            public bool Recursive { get; set; }
            public int Retention { get; set; }
            public string Include { get; set; }
            public string Exclude { get; set; }
            public string Comments { get; set; }
        }
        
        private Parameter[] GetXMLData()
        {
            file = "archive.xml";
            if (File.Exists(file)) {

           
                int c = 0;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(file);
                Parameter[] Params = new Parameter[xmlDoc.DocumentElement.ChildNodes.Count]; //Deze wordt later nog opgeschoond van lege records
                foreach (XmlNode xmlNode in xmlDoc.DocumentElement.ChildNodes)
                {
                    if (xmlNode.HasChildNodes)
                    {
                        string[] p = new string[6];
                        bool rec = new bool();
                        int tspa = new int();
                        int ret = new int();
                        for (int i = 0; i < xmlNode.ChildNodes.Count; i++)
                        {
                            switch (xmlNode.ChildNodes[i].LocalName.ToString())
                            {
                                case "InterfaceName":
                                    p[0] = xmlNode.ChildNodes[i].InnerText;
                                    break;
                                case "ProcesDir":
                                    p[1] = xmlNode.ChildNodes[i].InnerText;
                                    break;
                                case "ArchiveDir":
                                    p[2] = xmlNode.ChildNodes[i].InnerText;
                                    break;
                                case "TimeSpan":
                                    switch (xmlNode.ChildNodes[i].InnerText.ToString())
                                    {
                                        case "D": 
                                            tspa = 0;
                                            break;
                                        case "W":
                                            tspa = 1;
                                            break;
                                        case "M":
                                            tspa = 2;
                                            break;
                                        case "Y":
                                            tspa = 3;
                                            break;
                                        default:
                                            tspa = 0;
                                            break;
                                    }
                                    break;
                                case "Recursive":
                                    if (xmlNode.ChildNodes[i].InnerText.ToLower() == "true") {rec=true;} else {rec=false;}
                                    break;
                                case "Retention":
                                    ret = Convert.ToInt16(xmlNode.ChildNodes[i].InnerText);
                                    break;
                                case "Include":
                                    if (p[3] != null) { p[3] += "\r\n"; }
                                    p[3] = p[3] + xmlNode.ChildNodes[i].InnerText;
                                    break;
                                case "Exclude":
                                    if (p[4] != null) { p[4] += "\r\n"; }
                                    p[4] = p[4] + xmlNode.ChildNodes[i].InnerText;
                                    break;
                                case "Comments":
                                    p[5] = xmlNode.ChildNodes[i].InnerText;
                                    break;
                            }
                        }

                        Params[c] = new Parameter()
                        {
                            InterfaceName = p[0],
                            ProcesDir = p[1],
                            ArchiveDir = p[2],
                            Timespan = tspa,
                            Recursive = rec,
                            Retention = ret,
                            Include = p[3],
                            Exclude = p[4],
                            Comments = p[5]
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
            else 
            {
                Parameter[] Params = new Parameter[1];
                Params[0] = new Parameter() {};
                return Params; 
            }
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
    }
}
