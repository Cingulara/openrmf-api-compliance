using Xunit;
using openstig_api_compliance.Models.Compliance;

namespace tests.Models
{
    public class NISTControlTests
    {
        [Fact]
        public void Test_NewNISTControlIsValid()
        {
            NISTControl nc = new NISTControl();
            Assert.True(nc != null);
        }
    
        [Fact]
        public void Test_CciItemWithDataIsValid()
        {
            NISTControl nc = new NISTControl();

            // test things out
            Assert.True(nc != null);
        }
    }
}
