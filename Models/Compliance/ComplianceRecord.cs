using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using openstig_api_compliance.Models.Artifact;

namespace openstig_api_compliance.Models.Compliance
{
    [Serializable]
    public class ComplianceRecord
    {
        public ComplianceRecord () {
            // any initialization here
        }
        public string artifactId { get; set; }
        public string title { get; set; }
        public STIGtype type { get; set; }
        public string typeTitle { get { return Enum.GetName(typeof(STIGtype), type);} }
        public string vulnId { get; set; }
        public string vulnTitle { get; set;}
        public string stigId { get; set; }
        public string status { get; set; }
        public string severity { get; set;}

        // the last time this was updated
        public DateTime updatedOn { get; set; }
    }

}