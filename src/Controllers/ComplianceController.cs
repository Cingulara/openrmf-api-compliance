using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using openstig_api_compliance.Classes;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

// using DocumentFormat.OpenXml;
// using DocumentFormat.OpenXml.Packaging;
// using DocumentFormat.OpenXml.Spreadsheet;

namespace openstig_api_compliance.Controllers
{
    [Route("/")]
    public class ComplianceController : Controller
    {
        private readonly ILogger<ComplianceController> _logger;

        public ComplianceController(ILogger<ComplianceController> logger)
        {
            _logger = logger;
        }

        // GET the compliance listing for a system
        [HttpGet("system/{id}")]
        [Authorize(Roles = "Administrator,Reader,Editor,Assessor")]
        public async Task<IActionResult> GetCompliancBySystem(string id, string filter, bool pii)
        {
            if (!string.IsNullOrEmpty(id)) {
                try {
                    var result = ComplianceGenerator.GetSystemControls(id, filter, pii);
                    if (result != null && result.Result != null && result.Result.Count > 0)
                        return Ok(result);
                    else
                        return NotFound(); // bad system reference
                }
                catch (Exception ex) {
                    _logger.LogError(ex, "Error listing all checklists for system {0}", id);
                    return BadRequest();
                }
            }
            else
                return BadRequest(); // no term entered
        }
    }
}
