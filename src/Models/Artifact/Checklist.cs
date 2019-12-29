// Copyright (c) Cingulara LLC 2019 and Tutela LLC 2019. All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE Version 3, 29 June 2007 license. See LICENSE file in the project root for full license information.

namespace openrmf_api_compliance.Models.Artifact
{

    public class CHECKLIST {

        public CHECKLIST (){
            ASSET = new ASSET();
            STIGS = new STIGS();
        }

        public ASSET ASSET { get; set; }
        public STIGS STIGS { get; set; }
    }
}