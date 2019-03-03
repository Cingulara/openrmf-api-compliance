using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace openstig_api_compliance.Models.Compliance
{
    [Serializable]
    public class NISTControl
    {
        public NISTControl () {
            complianceRecords = new List<ComplianceRecord>();
        }

        // the control is the major piece to use here, AC-1, AU-9, etc.
        public string control { get; set;}

        // the index is the major control with all extra dots, dashes, and sub paragraphs
        public string index { get; set; }
        public string title { get; set; }
        public int version { get; set; }
        public string location { get; set; }
        public string CCI { get; set; }
        public List<ComplianceRecord> complianceRecords { get; set; }
    }

}