using Xunit;
using openstig_api_compliance.Models.Artifact;
using System;

namespace tests.Models
{
    public class CHECKLISTTests
    {
        [Fact]
        public void Test_NewCHECKLISTIsValid()
        {
            CHECKLIST chk = new CHECKLIST();
            Assert.True(chk != null);
        }
    
        [Fact]
        public void Test_CHECKLISTWithDataIsValid()
        {
            CHECKLIST chk = new CHECKLIST();
            // test things out
            Assert.True(chk != null);
            Assert.True(chk.ASSET != null);
            Assert.True(chk.STIGS != null);
        }
    }
}
