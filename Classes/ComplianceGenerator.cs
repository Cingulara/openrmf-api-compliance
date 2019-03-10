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
                // load the current XML document to get all CCI to NIST Major Controls
                List<CciItem> cciItems = NistCciGenerator.LoadNistToCci();                
                List<NISTControl> controls = CreateListOfNISTControls(cciItems);

                // get all the variables ready
                List<STIG_DATA> sd;
                Artifact art;
                ComplianceRecord rec;
                string host = "";
                string domainName = "";
                List<Artifact> checklists = WebClient.GetChecklistsBySystem(systemId).GetAwaiter().GetResult();
                if (checklists != null && checklists.Count > 0) {
                    foreach (Artifact a in checklists) {
                        art = WebClient.GetChecklistAsync(a.InternalId.ToString()).GetAwaiter().GetResult();
                        if (art != null) {
                            host = !string.IsNullOrEmpty(art.CHECKLIST.ASSET.HOST_NAME)? art.CHECKLIST.ASSET.HOST_NAME : "";
                            domainName = !string.IsNullOrEmpty(art.CHECKLIST.ASSET.HOST_FQDN)? art.CHECKLIST.ASSET.HOST_FQDN : "";
                            foreach (VULN v in art.CHECKLIST.STIGS.iSTIG.VULN){
                                // grab each CCI and then match to one or more NIST Control records
                                // fill in the compliance record for the control and add the compliance record to that control w/in the larger list
                                sd = v.STIG_DATA.Where(x => x.VULN_ATTRIBUTE == "CCI_REF").ToList();
                                foreach (STIG_DATA d in sd) {
                                    foreach (NISTControl ctrl in controls.Where(x => x.CCI == d.ATTRIBUTE_DATA).ToList()) {
                                        rec = new ComplianceRecord();
                                        rec.artifactId = art.InternalId;
                                        rec.severity = v.STIG_DATA.Where(y => y.VULN_ATTRIBUTE == "Severity").FirstOrDefault().ATTRIBUTE_DATA;
                                        rec.status = v.STATUS;
                                        rec.stigId = v.STIG_DATA.Where(y => y.VULN_ATTRIBUTE == "Rule_ID").FirstOrDefault().ATTRIBUTE_DATA;
                                        rec.vulnId = v.STIG_DATA.Where(y => y.VULN_ATTRIBUTE == "Vuln_Num").FirstOrDefault().ATTRIBUTE_DATA;
                                        rec.updatedOn = art.updatedOn.Value;
                                        rec.title = art.title;
                                        rec.type = art.type;
                                        rec.hostName = host;
                                        rec.domainName = domainName;
                                        rec.vulnTitle = v.STIG_DATA.Where(y => y.VULN_ATTRIBUTE == "Rule_Title").FirstOrDefault().ATTRIBUTE_DATA;
                                        ctrl.complianceRecords.Add(rec); // add the rec to the control we are making
                                }
                                }
                            }
                        }
                    }
                    // order by the index, which also groups them by the major control
                    return controls.OrderBy(x => x.index).ToList();
                }
                else
                    return null;
            }
            catch (Exception ex) {
                // log it here
                throw ex;
            }
        }

        // for each of the CCI items in the list 
        //   get the CCI ID
        //   cycle through all references
        //     for each reference add a new control record to the list
        //   when done return the list of controls, somewhere around 6,368 of them
        private static List<NISTControl> CreateListOfNISTControls (List<CciItem> cciItems) {
            List<NISTControl> controls = new List<NISTControl>();
            NISTControl control;
            foreach (CciItem item in cciItems) {
                foreach (CciReference reference in item.references) {
                    control = new NISTControl();
                    control.CCI = item.cciId;
                    control.control = reference.majorControl;
                    control.index = reference.index;
                    control.location = reference.location;
                    control.title = reference.title;
                    control.version = reference.version;
                    // now add the unique record from the xml file to the list of all controls
                    controls.Add(control); // add each one as the combination of index and CCI is unique
                }
            }
            return controls;
        }
    }
}