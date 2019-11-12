using System.Collections.Generic;
using System.Xml.Serialization;

namespace openrmf_api_compliance.Models.Artifact
{

    public class iSTIG {

        public iSTIG (){
            STIG_INFO = new STIG_INFO();
            VULN = new List<VULN>();
        }

        public STIG_INFO STIG_INFO { get; set; }

        [XmlElement("VULN")]
        public List<VULN> VULN { get; set; }
    }
}