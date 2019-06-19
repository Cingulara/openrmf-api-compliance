using Xunit;
using openstig_api_compliance.Models.Compliance;

namespace tests.Models
{
    public class ControlSetTests
    {
        [Fact]
        public void Test_NewControlSetIsValid()
        {
            ControlSet cs = new ControlSet();
            Assert.True(cs != null);
        }
    
        [Fact]
        public void Test_ControlSetWithDataIsValid()
        {
            ControlSet cs = new ControlSet();

            // test things out
            Assert.True(cs != null);
        }
    }
}
