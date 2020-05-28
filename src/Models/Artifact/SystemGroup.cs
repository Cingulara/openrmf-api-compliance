// Copyright (c) Cingulara LLC 2019 and Tutela LLC 2019. All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE Version 3, 29 June 2007 license. See LICENSE file in the project root for full license information.

using System;

namespace openrmf_api_compliance.Models.Artifact
{
    [Serializable]
    public class SystemGroup
    {
        public SystemGroup () {
        }

        public string InternalId { get; set; }

        public string title { get; set;}
        public string description { get; set;}
        public int numberOfChecklists { get; set;}
        
        // stores the raw XML of the Nessus file from a scan
        public string rawNessusFile { get; set;}
        public string nessusFilename { get; set;}

        // attribute to gain control on datetime serialization
        public DateTime? lastComplianceCheck { get; set;}

        public DateTime created { get; set; }

        public DateTime? updatedOn { get; set; }

        public Guid createdBy { get; set; }
        public Guid? updatedBy { get; set; }
    }
}