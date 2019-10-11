using System;

namespace openrmf_api_compliance.Models.Compliance
{
    [Serializable]
    public class ComplianceRecord
    {
        public ComplianceRecord () {
            // any initialization here
        }
        public string artifactId { get; set; }
        public string title { get; set; }
        public string stigType { get; set; }
        public string stigRelease { get; set; }
        
        public string status { get; set; }
        public string hostName { get; set;}

        // the last time this was updated
        public DateTime updatedOn { get; set; }
    }

}