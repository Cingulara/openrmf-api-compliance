![.NET Core Build and Test](https://github.com/Cingulara/openrmf-api-compliance/workflows/.NET%20Core%20Build%20and%20Test/badge.svg)

# openrmf-api-compliance
This is the OpenRMF Compliance API that reads in all system checklists via the openRMF Read API, 
and for each matches the 1-or-more CCI per STIG vulnerability to the NIST to CCI listing based on 
the XML file from DISA. It then generates a report of Open (STIG is open), Not Reviewed, or Not a Finding/Closed
(STIG is Not a Finding or N/A) grouped by the NIST major controls for the system based on the C-I-A 
low/moderate/high classification.

# API Calls

* GET to /system/{systemId} to generate the NIST major controls listing with all relevant STIG checklist data
* GET to /system/{systemId}/export to generate the NIST major controls listing with all relevant STIG checklist data into an XLSX
* GET to /cci/{control} to return the CCIs related to a NIST higher level control
* GET to /swagger/ gives you the API structure.

# Message Calls
* openrmf.system.compliance with payload of the system group ID. The System MSG client reads this and updates the system record with the date for last compliance.
* openrmf.system with a payload of the system group ID to get the system info.
* openrmf.system.checklists.read gets a list of all checklists from the system group ID
* openrmf.checklist.read gets a particular full checklist record
* openrmf.controls get a list of all controls
* openrmf.compliance.cci get a list of all CCI items
* openrmf.compliance.cci.references get the list of CCI NIST references

# Additional Information
More documentation is at https://github.com/Cingulara/openrmf-docs/.

# To Do
* [ ] Add Moq tests on static methods via a wrapper https://stackoverflow.com/questions/9052736/moq-a-static-method-in-static-class