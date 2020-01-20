// Copyright (c) Cingulara LLC 2019 and Tutela LLC 2019. All rights reserved.
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

        /// <summary>
        /// GET The compliance of a system based on the system ID passed in, if it has PII, 
        /// and whatever filter we have such as the impact level.!-- 
        /// </summary>
        /// <param name="id">The ID of the system to generate compliance</param>
        /// <param name="filter">The filter to show impact level of Low, Moderate, High</param>
        /// <param name="pii">A boolean to say if this has PII or not.  There are 
        ///        specific things to check compliance if this has PII.
        /// </param>
        /// <returns>
        /// HTTP Status showing it was generated as well as the list of compliance records and status. 
        /// Also shows the status of each checklist per major control.
        /// </returns>
        /// <response code="200">Returns the generated compliance listing</response>
        /// <response code="400">If the item did not generate it correctly</response>
        /// <response code="404">If the ID passed in is not valid</response>
        [HttpGet("system/{id}")]
        [Authorize(Roles = "Administrator,Reader,Editor,Assessor")]
        public async Task<IActionResult> GetCompliancBySystem(string id, string filter, bool pii)
        {
            if (!string.IsNullOrEmpty(id)) {
                try {
                    _logger.LogInformation("Calling GetCompliancBySystem({0}, {1}, {2})", id, filter, pii.ToString());
                    var result = ComplianceGenerator.GetSystemControls(id, filter, pii);
                    if (result != null && result.Result != null && result.Result.Count > 0) {
                        _logger.LogInformation("Called GetCompliancBySystem({0}, {1}, {2}) successfully", id, filter, pii.ToString());
                        return Ok(result);
                    }
                    else {
                        _logger.LogWarning("Called GetCompliancBySystem({0}, {1}, {2}) but had no returned data", id, filter, pii.ToString());
                        return NotFound(); // bad system reference
                    }
                }
                catch (Exception ex) {
                    _logger.LogError(ex, "GetCompliancBySystem() Error listing all checklists for system {0}", id);
                    return BadRequest();
                }
            }
            else {
                _logger.LogWarning("Called GetCompliancBySystem() butwith an invalid or empty Id", id);
                return BadRequest(); // no term entered
            }
        }
    }
}
