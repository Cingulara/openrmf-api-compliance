using System;
using System.Collections.Generic;

namespace openstig_api_compliance.Models.Artifact
{

    public class STIGS {

        public STIGS (){
            iSTIG = new iSTIG();
        }

        public iSTIG iSTIG { get; set; }
    }
}