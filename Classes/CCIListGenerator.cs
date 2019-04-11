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
    public static class CCIListGenerator 
    {
      public static async Task<List<string>> GetCCIListing(string control)
      {
        try {
          // load the current XML document to get all CCI to NIST Major Controls
          List<CciItem> cciItems = NistCciGenerator.LoadNistToCci();
          // find all records where the NIST control is in the CciReference and add the parent CCI
          // return the list of strings
          List<string> cciList = new List<string>();
          foreach (CciItem item in cciItems) {
              if (item.references.Where(x => x.majorControl == control).FirstOrDefault() != null){
                cciList.Add(item.cciId); // add this listing
              }
          }

          return cciList.Distinct().ToList();
        }
        catch (Exception ex) {
            // log it here
            throw ex;
        }
      }
    }
}