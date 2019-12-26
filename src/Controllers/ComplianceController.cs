// Copyright (c) Cingulara 2019. All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE Version 3, 29 June 2007 license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using openrmf_api_compliance.Classes;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace openrmf_api_compliance.Controllers
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
