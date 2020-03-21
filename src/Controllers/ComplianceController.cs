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
        /// <param name="majorcontrol">The filter to show only show compliance with the major control passed in</param>
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
        public async Task<IActionResult> GetCompliancBySystem(string id, string filter, bool pii, string majorcontrol = "")
        {
            if (!string.IsNullOrEmpty(id)) {
                try {
                    _logger.LogInformation("Calling GetCompliancBySystem({0}, {1}, {2})", id, filter, pii.ToString());
                    var result = ComplianceGenerator.GetSystemControls(id, filter, pii, majorcontrol);
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
                _logger.LogWarning("Called GetCompliancBySystem() but with an invalid or empty Id", id);
                return BadRequest(); // no term entered
            }
        }
        
        /// <summary>
        /// GET The CCI item information and references based on the CCI ID passed in
        /// </summary>
        /// <param name="cciid">The CCI Number/ID of the system to generate compliance</param>
        /// <returns>
        /// HTTP Status showing the CCI item is here and the CCI item record with references.
        /// </returns>
        /// <response code="200">Returns the CCI item record</response>
        /// <response code="400">If the item did not generate it correctly</response>
        /// <response code="404">If the ID passed in is not valid</response>
        [HttpGet("cci/{cciid}")]
        [Authorize(Roles = "Administrator,Reader,Editor,Assessor")]
        public async Task<IActionResult> GetCCIItem(string cciid)
        {
            if (!string.IsNullOrEmpty(cciid)) {
                try {
                    _logger.LogInformation("Calling GetCCIItem({0})", cciid);
                    var result = NATSClient.GetCCIItemReferences(cciid);
                    if (result != null) {
                        _logger.LogInformation("Called GetCCIItem({0}) successfully", cciid);
                        return Ok(result);
                    }
                    else {
                        _logger.LogWarning("Called GetCCIItem({0}) but had no returned data", cciid);
                        return NotFound(); // bad system reference
                    }
                }
                catch (Exception ex) {
                    _logger.LogError(ex, "GetCCIItem() Error getting CCI Item information for {0}", cciid);
                    return BadRequest();
                }
            }
            else {
                _logger.LogWarning("Called GetCCIItem() but with an invalid or empty Id", cciid);
                return BadRequest(); // no CCI Id entered
            }
        }
    }
}
