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
            nc.control = "AC-1";
            nc.index = "1.2";
            nc.title = "My Title here";
            nc.version = "23333";
            nc.location = "this location";
            nc.CCI = "34234234234";
            
            // test things out
            Assert.True(nc != null);
            Assert.True (!string.IsNullOrEmpty(nc.control));
            Assert.True (!string.IsNullOrEmpty(nc.index));
            Assert.True (!string.IsNullOrEmpty(nc.title));
            Assert.True (!string.IsNullOrEmpty(nc.version));
            Assert.True (!string.IsNullOrEmpty(nc.location));
            Assert.True (!string.IsNullOrEmpty(nc.CCI));
        }
    }
}
