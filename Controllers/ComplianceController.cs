using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using openstig_api_compliance.Classes;
using openstig_api_compliance.Models;
using System.IO;
using System.Text;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Xml.Serialization;
using System.Xml;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

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
        public async Task<IActionResult> GetCompliancBySystem(string id)
        {
            if (!string.IsNullOrEmpty(id)) {
                try {
                    var result = ComplianceGenerator.GetSystemControls(id);
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

        // GET the CCIs related to a NIST higher level control
        [HttpGet("cci/{control}")]
        public async Task<IActionResult> GetCCIListingByControl(string control)
        {
            if (!string.IsNullOrEmpty(control)) {
                try {
                    var result = CCIListGenerator.GetCCIListing(control);
                    if (result != null && result.Result != null && result.Result.Count > 0)
                        return Ok(result.Result);
                    else
                        return NotFound(); // bad system reference
                }
                catch (Exception ex) {
                    _logger.LogError(ex, "Error listing all CCIs for control {0}", control);
                    return BadRequest();
                }
            }
            else
                return BadRequest(); // no term entered
        }
    }
}
