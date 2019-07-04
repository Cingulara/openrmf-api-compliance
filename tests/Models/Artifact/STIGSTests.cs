using Xunit;
using openstig_api_compliance.Models.Artifact;
using System;

namespace tests.Models
{
    public class STIGSTests
    {
        [Fact]
        public void Test_NewSTIGSIsValid()
        {
            STIGS data = new STIGS();
            Assert.True(data != null);
        }
    
        [Fact]
        public void Test_STIGSWithDataIsValid()
        {
            STIGS data = new STIGS();

            // test things out
            Assert.True(data != null);
            Assert.True(data.iSTIG != null);
        }
    }
}
