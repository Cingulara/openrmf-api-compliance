using Xunit;
using openrmf_api_compliance.Models.Compliance;
using System;

namespace tests.Models
{
    public class NISTComplianceTests
    {
        [Fact]
        public void Test_NewNISTComplianceIsValid()
        {
            NISTCompliance nc = new NISTCompliance();
            Assert.True(nc != null);
            Assert.True(nc.complianceRecords.Count == 0);
        }
    
        [Fact]
        public void Test_NISTComplianceWithDataIsValid()
        {
            NISTCompliance nc = new NISTCompliance();
            nc.control = "AC-1";
            nc.index = "1.2";
            nc.title = "My Title here";
            nc.version = "23333";
            nc.location = "this location";
            nc.CCI = "34234234234";
            nc.sortString = "AC-01";

            ComplianceRecord cr = new ComplianceRecord();
            cr.artifactId = "23423423423423423423";
            cr.title = "mytitle";
            cr.stigType = "Google Chrome";
            cr.stigRelease = "Version 1";
            cr.status = "valid";
            cr.hostName = "myHost";
            cr.updatedOn = DateTime.Now;

            nc.complianceRecords.Add(cr);
            // test things out
            Assert.True(nc != null);
            Assert.True (!string.IsNullOrEmpty(nc.control));
            Assert.True (!string.IsNullOrEmpty(nc.index));
            Assert.True (!string.IsNullOrEmpty(nc.title));
            Assert.True (!string.IsNullOrEmpty(nc.version));
            Assert.True (!string.IsNullOrEmpty(nc.location));
            Assert.True (!string.IsNullOrEmpty(nc.CCI));
            Assert.True(nc.complianceRecords.Count == 1);
            // test one of the items in the list record
            Assert.True (!string.IsNullOrEmpty(nc.complianceRecords[0].artifactId));
        }
    }
}
