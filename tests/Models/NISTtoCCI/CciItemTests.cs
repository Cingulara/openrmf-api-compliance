using Xunit;
using openstig_api_compliance.Models.NISTtoCCI;

namespace tests.Models
{
    public class CciItemTests
    {
        [Fact]
        public void Test_NewCciItemIsValid()
        {
            CciItem cci = new CciItem();
            Assert.True(cci != null);
            Assert.True(cci.references != null);
            Assert.True(cci.references.Count == 0);
        }
    
        [Fact]
        public void Test_CciItemWithDataIsValid()
        {
            CciItem cci = new CciItem();
            cci.cciId = "cciId";
            cci.status = "status";
            cci.publishDate = "mydate";
            cci.contributor = "mycontributor";
            cci.definition = "mydefinition";
            cci.type = "mytype";
            cci.parameter = "param1";
            cci.note = "mynote";
            CciReference ccir = new CciReference();
            ccir.creator = "me";
            ccir.title = "mytitle";
            ccir.version = "v1";
            ccir.location = "mylocation";
            ccir.index = "2.3";
            ccir.majorControl = "AC-2";
            cci.references.Add(ccir);

            // test things out
            Assert.True(cci != null);
            Assert.True(cci.references != null);
            Assert.True(cci.references.Count == 1);
            Assert.True(cci.references[0] != null);
        }
    }
}
