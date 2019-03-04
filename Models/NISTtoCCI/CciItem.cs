using System;
using System.Collections.Generic;

namespace openstig_api_compliance.Models.NISTtoCCI
{
    [Serializable]
    public class CciItem
    {
        public CciItem () {
            references = new List<CciReference>();
        }
        public string cciId { get; set; }
        public string status { get; set; }
        public string publishDate { get; set; }
        public string contributor { get; set; }
        public string definition { get; set; }
        public string type { get; set; }
        public string parameter { get; set; }
        public string note { get; set; }
        public List<CciReference> references { get; set; }
    }

    public class CciReference
    {
        public CciReference()
        {}
        
        public string creator { get; set; }
        public string title { get; set; }
        public string version { get; set; }
        public string location { get; set; }
        public string index { get; set; }
        public string majorControl { get; set; }
    }
}