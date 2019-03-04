using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using openstig_api_compliance.Models.Artifact;
using openstig_api_compliance.Models.Compliance;
using openstig_api_compliance.Models.NISTtoCCI;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace openstig_api_compliance.Classes
{
    public static class ComplianceGenerator 
    {
        public static async Task<List<NISTControl>> GetSystemControls(string systemId)
        {
            // for each system
            //  for each checklist in the system
            //    for each VULN listing
            //      for each CCI within the VULN listed
            //        match up the CCI to the NIST, then get the status, checklist and ID, STIG ID , VULN ID, and type and return it
            try {
                List<CciItem> cciItems = NistCciGenerator.LoadNistToCci();                

                // get all the variables ready
                List<NISTControl> controls = new List<NISTControl>();
                NISTControl control;
                List<STIG_DATA> sd;
                Artifact art;
                List<Artifact> checklists = WebClient.GetChecklistsBySystem(systemId).GetAwaiter().GetResult();
                if (checklists != null && checklists.Count > 0) {
                    foreach (Artifact a in checklists) {
                        art = WebClient.GetChecklistAsync(a.InternalId.ToString()).GetAwaiter().GetResult();
                        foreach (VULN v in art.CHECKLIST.STIGS.iSTIG.VULN){
                            // grab each CCI and then match to NIST
                            // fill in the compliance record for the control and add to the list
                            sd = v.STIG_DATA.Where(x => x.VULN_ATTRIBUTE == "CCI_REF").ToList();
                            foreach (STIG_DATA d in sd) {
                                control = new NISTControl();
                                control.CCI = d.ATTRIBUTE_DATA;
                                control.control = "AU-9";
                                control.index = "AU-9.1";
                                ComplianceRecord rec = new ComplianceRecord();
                                rec.artifactId = art.InternalId;
                                rec.severity = v.STIG_DATA.Where(y => y.VULN_ATTRIBUTE == "Severity").FirstOrDefault().ATTRIBUTE_DATA;
                                rec.status = v.STATUS;
                                rec.stigId = v.STIG_DATA.Where(y => y.VULN_ATTRIBUTE == "Rule_ID").FirstOrDefault().ATTRIBUTE_DATA;
                                rec.vulnId = v.STIG_DATA.Where(y => y.VULN_ATTRIBUTE == "Vuln_Num").FirstOrDefault().ATTRIBUTE_DATA;
                                rec.updatedOn = art.updatedOn.Value;
                                rec.title = art.title;
                                rec.type = art.type;
                                rec.vulnTitle = v.STIG_DATA.Where(y => y.VULN_ATTRIBUTE == "Rule_Title").FirstOrDefault().ATTRIBUTE_DATA;
                                control.complianceRecords.Add(rec); // add the rec to the control we are making
                                controls.Add(control); // add to the control
                            }
                        }
                    }
                    return controls;
                }
                else
                    return null;
            }
            catch (Exception ex) {
                // log it here
                throw ex;
            }
        }
    }
}