using Xunit;
using openstig_api_compliance.Models.Compliance;
using System;

namespace tests.Models
{
    public class ComplianceRecordTests
    {
        [Fact]
        public void Test_NewComplianceRecordIsValid()
        {
            ComplianceRecord cr = new ComplianceRecord();
            Assert.True(cr != null);
        }
    
        [Fact]
        public void Test_ComplianceRecordWithDataIsValid()
        {
            ComplianceRecord cr = new ComplianceRecord();
            cr.artifactId = "23423423423423423423";
            cr.title = "mytitle";
            cr.stigType = "Google Chrome";
            cr.stigRelease = "Version 1";
            cr.status = "valid";
            cr.hostName = "myHost";
            cr.updatedOn = DateTime.Now;
            // test things out
            Assert.True(cr != null);
            Assert.True (!string.IsNullOrEmpty(cr.artifactId));
            Assert.True (!string.IsNullOrEmpty(cr.title));
            Assert.True (!string.IsNullOrEmpty(cr.stigType));
            Assert.True (!string.IsNullOrEmpty(cr.stigRelease));
            Assert.True (!string.IsNullOrEmpty(cr.status));
            Assert.True (!string.IsNullOrEmpty(cr.hostName));
            Assert.True (!string.IsNullOrEmpty(cr.updatedOn.ToShortDateString()));
        }
    }
}
