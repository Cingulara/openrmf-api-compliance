using Xunit;
using openstig_api_compliance.Models.Artifact;
using System;

namespace tests.Models
{
    public class STIG_DATATests
    {
        [Fact]
        public void Test_NewSTIG_DATAIsValid()
        {
            STIG_DATA data = new STIG_DATA();
            Assert.True(data != null);
        }
    
        [Fact]
        public void Test_STIG_DATAWithDataIsValid()
        {
            STIG_DATA data = new STIG_DATA();
            data.VULN_ATTRIBUTE = "my attribute";
            data.ATTRIBUTE_DATA = "my data";

            // test things out
            Assert.True(data != null);
            Assert.True(!string.IsNullOrEmpty(data.VULN_ATTRIBUTE));
            Assert.True(!string.IsNullOrEmpty(data.ATTRIBUTE_DATA));
        }
    }
}
