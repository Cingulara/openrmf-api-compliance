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
      public static async Task<List<NISTCompliance>> GetSystemControls(string systemId)
      {
        // for each system
        //  for each checklist in the system
        //    for each VULN listing
        //      for each CCI within the VULN listed
        //        match up the CCI to the NIST, then get the status, checklist and ID, STIG ID , VULN ID, and type and return it
        try {
          // load the current XML document to get all CCI to NIST Major Controls
          List<CciItem> cciItems = NistCciGenerator.LoadNistToCci();
          // list of the NIST controls down to the index-to-CCI level we cycle through
          List<NISTControl> controls = CreateListOfNISTControls(cciItems);
          // the end result grouped by control and listing checklists and their status
          List<NISTCompliance> complianceList = new List<NISTCompliance>();

          // get all the variables ready
          List<STIG_DATA> sd;
          Artifact art;
          ComplianceRecord rec;
          NISTCompliance compliance;
          string host = "";
          List<Artifact> checklists = WebClient.GetChecklistsBySystem(systemId).GetAwaiter().GetResult();
          if (checklists != null && checklists.Count > 0) {
            foreach (Artifact a in checklists) {
              art = WebClient.GetChecklistAsync(a.InternalId.ToString()).GetAwaiter().GetResult();
              if (art != null) {
                  host = !string.IsNullOrEmpty(art.CHECKLIST.ASSET.HOST_NAME)? art.CHECKLIST.ASSET.HOST_NAME : "";
                  foreach (VULN v in art.CHECKLIST.STIGS.iSTIG.VULN){
                      // grab each CCI and then match to one or more NIST Control records
                      // fill in the compliance record for the control and add the compliance record to that control w/in the larger list
                      sd = v.STIG_DATA.Where(x => x.VULN_ATTRIBUTE == "CCI_REF").ToList();
                      foreach (STIG_DATA d in sd) {
                        foreach (NISTControl ctrl in controls.Where(x => x.CCI == d.ATTRIBUTE_DATA).ToList()) {
                          // for each CTRL, if it already has a complianceList record for the checklist and this control, then update the record
                          // if no record, then make a new one
                          if (complianceList.Where(z => z.index == ctrl.index).Count() > 0 ) { // should at most be 1
                            compliance = complianceList.Where(z => z.index == ctrl.index).First();
                          }
                          else {
                            compliance = new NISTCompliance();
                            compliance.index = ctrl.index; // add the control index
                            compliance.title = "I need to go get these :(";
                            complianceList.Add(compliance); // add it to the listing
                          }
                          // For the compliance record, does it have a listing for the checklist/artifactId
                          if (compliance.complianceRecords.Where(c => c.artifactId == a.InternalId).Count() > 0) { // if a new record, will be 0
                            rec = compliance.complianceRecords.Where(c => c.artifactId == a.InternalId).First(); //grab the the record to update the status
                            rec.status = GenerateStatus(rec.status, v.STATUS);
                          }
                          else {
                            rec = new ComplianceRecord();
                            rec.artifactId = a.InternalId;
                            rec.status = v.STATUS;
                            rec.updatedOn = a.updatedOn.Value;
                            rec.title = a.title;
                            rec.type = a.type;
                            rec.hostName = host;
                            compliance.complianceRecords.Add(rec); // add the new compliance record to the control we are making
                          }
                        }
                      }
                  }
                }
              }
              // order by the index, which also groups them by the major control
              return complianceList.OrderBy(x => x.index).ToList();
          }
          else
              return null;
        }
        catch (Exception ex) {
            // log it here
            throw ex;
        }
      }

      // use the rules in https://github.com/Cingulara/openstig-api-compliance/issues/1: 
      //   if anything is open, mark it open
      //   else if anything is not reviewed, mark it not reviewed
      //   else if the rest are not applicable and not a finding, mark it green (should be a catch-all else)
      private static string GenerateStatus(string oldStatus, string newStatus) {
        if (newStatus.ToLower() == "open")
          return "open";
        else if (newStatus.ToLower() == "not_reviewed")
          return "not_reviewed";
        else 
          return "notafinding";
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