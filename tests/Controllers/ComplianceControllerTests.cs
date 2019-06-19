using System;
using Xunit;
using openstig_api_compliance.Controllers;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace tests.Controllers
{

    public class ComplianceControllerTests
    {
        private readonly Mock<ILogger<ComplianceController>> _mockLogger;
        private readonly ComplianceController _complianceController; 
        private readonly Mock<ComplianceController> _context;

        public ComplianceControllerTests() {
            _mockLogger = new Mock<ILogger<ComplianceController>>();
            _complianceController = new ComplianceController(_mockLogger.Object);
        }

        [Fact]
        public void Test_ComplianceControllerIsValid()
        {
            Assert.True(_complianceController != null);
        }

        [Fact]
        public void Test_ComplianceControllerGetComlianceBySystemIsValid()
        {
            var result = _complianceController.GetCompliancBySystem("myid", "low", false);
            Assert.True(_complianceController != null);
            Assert.Equal(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result).StatusCode); // returns a status code HTTP 200
        }

        [Fact]
        public void Test_ComplianceControllerGetCCIListingByControlIsValid()
        {
            var result = _complianceController.GetCCIListingByControl("AC-1");
            Assert.True(_complianceController != null);
            Assert.Equal(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result).StatusCode); // returns a status code HTTP 200
        }

    }
}
