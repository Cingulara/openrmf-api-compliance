using Xunit;
using openstig_api_compliance.Models.Artifact;
using System;

namespace tests.Models
{
    public class ArtifactTests
    {
        [Fact]
        public void Test_NewArtifactIsValid()
        {
            Artifact art = new Artifact();
            Assert.True(art != null);
        }
    
        [Fact]
        public void Test_ArtifactWithDataIsValid()
        {
            Artifact art = new Artifact();
            art.created = DateTime.Now;
            art.description = "my description";
            art.rawChecklist = "my XML goes here";
            art.system = "my system";
            art.InternalId = "som234872349087sdhfasdfhoddq908457098457sdajajstring";
            art.hostName = "myHost";
            art.stigType = "Google Chrome";
            art.stigRelease = "Version 1";
            art.updatedOn = DateTime.Now;

            // test things out
            Assert.True(art != null);
            Assert.True (!string.IsNullOrEmpty(art.created.ToShortDateString()));
            Assert.True (!string.IsNullOrEmpty(art.description));
            Assert.True (!string.IsNullOrEmpty(art.rawChecklist));
            Assert.True (!string.IsNullOrEmpty(art.system));
            Assert.True (!string.IsNullOrEmpty(art.InternalId));
            Assert.True (!string.IsNullOrEmpty(art.hostName));
            Assert.True (!string.IsNullOrEmpty(art.stigType));
            Assert.True (!string.IsNullOrEmpty(art.stigRelease));
            Assert.True (!string.IsNullOrEmpty(art.title));  // readonly from other fields
            Assert.True (art.updatedOn.HasValue);
            Assert.True (!string.IsNullOrEmpty(art.updatedOn.Value.ToShortDateString()));
            Assert.True (art.CHECKLIST != null);
        }
    }
}
