using System;

namespace openrmf_api_compliance.Models.Compliance
{
    [Serializable]
    public class NISTControl
    {
        public NISTControl () {
        }

        // the control is the major piece to use here, AC-1, AU-9, etc.
        public string control { get; set;}

        // the index is the major control with all extra dots, dashes, and sub paragraphs
        public string index { get; set; }
        // This is the title of the index from the NIST site (tbd)
        public string title { get; set; }
        public string version { get; set; }
        public string location { get; set; }
        public string CCI { get; set; }
    }
}