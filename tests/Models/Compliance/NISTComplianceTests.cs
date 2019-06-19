using Xunit;
using openstig_api_compliance.Models.Compliance;

namespace tests.Models
{
    public class NISTComplianceTests
    {
        [Fact]
        public void Test_NewNISTComplianceIsValid()
        {
            NISTCompliance nc = new NISTCompliance();
            Assert.True(nc != null);
        }
    
        [Fact]
        public void Test_NISTComplianceWithDataIsValid()
        {
            NISTCompliance nc = new NISTCompliance();

            // test things out
            Assert.True(nc != null);
        }
    }
}
