using System;
using Xunit;
using openstig_api_compliance.Controllers;
using openstig_api_compliance.Models.Compliance;
using openstig_api_compliance.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;

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
