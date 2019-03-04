using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using openstig_api_compliance.Models.Compliance;
using openstig_api_compliance.Models.NISTtoCCI;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace openstig_api_compliance.Classes
{
    public static class NistCciGenerator
    {        
        public static List<CciItem> LoadNistToCci() {
            List<CciItem> cciList = new List<CciItem>();
            XmlDocument xmlDoc = new XmlDocument();
            // get the file path for the CCI to NIST listing
            var ccipath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/U_CCI_List.xml";
            if (File.Exists(ccipath)) {
                xmlDoc.LoadXml(File.ReadAllText(ccipath));
                XmlNodeList itemList = xmlDoc.GetElementsByTagName("cci_item");
            
                foreach (XmlElement child in itemList) {
                    // get the SI_DATA record for SID_DATA and SID_NAME and then return them
                    // each SI_DATA has 2

                }
            }
            return cciList;
        }

    }
}