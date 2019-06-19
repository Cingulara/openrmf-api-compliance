using Xunit;
using openstig_api_compliance.Models.Compliance;
using System;

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
            cs.id = Guid.NewGuid();
            cs.family = "Program Management";
            cs.number = "PM-12";
            cs.title = "Program Mgmt Title";
            cs.priority = "P1";
            cs.lowimpact = true;
            cs.moderateimpact = true;
            cs.highimpact = false;
            cs.supplementalGuidance = "My supplemental text";
            cs.subControlDescription = "my description";
            cs.subControlNumber = "1.1.1.1.1";
            // test things out
            Assert.True(cs != null);
            Assert.True (!string.IsNullOrEmpty(cs.family));
            Assert.True (!string.IsNullOrEmpty(cs.number));
            Assert.True (!string.IsNullOrEmpty(cs.title));
            Assert.True (!string.IsNullOrEmpty(cs.priority));
            Assert.True (!string.IsNullOrEmpty(cs.supplementalGuidance));
            Assert.True (!string.IsNullOrEmpty(cs.subControlDescription));
            Assert.True (!string.IsNullOrEmpty(cs.subControlNumber));
            Assert.True (cs.lowimpact);
            Assert.True (cs.moderateimpact);
            Assert.False (cs.highimpact);
            Assert.True (Guid.Empty != cs.id); // it is not empty
        }
    }
}
