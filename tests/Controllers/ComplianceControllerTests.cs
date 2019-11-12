using Xunit;
using openrmf_api_compliance.Controllers;
using openrmf_api_compliance.Models.Compliance;
using openrmf_api_compliance.Classes;


using Moq;
using Microsoft.Extensions.Logging;

namespace tests.Controllers
{

    public class ComplianceControllerTests
    {
        private readonly Mock<ILogger<ComplianceController>> _mockLogger;
        private readonly ComplianceController _complianceController; 
        //private readonly Mock<ComplianceController> _context;

        public ComplianceControllerTests() {
            _mockLogger = new Mock<ILogger<ComplianceController>>();
            _complianceController = new ComplianceController(_mockLogger.Object);
        }

        [Fact]
        public void Test_ComplianceControllerIsValid()
        {
            Assert.True(_complianceController != null);
        }
    }
}
