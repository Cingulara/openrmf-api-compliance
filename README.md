# openrmf-api-compliance
This is the openRMF Compliance API that reads in all system checklists via the openRMF Read API, 
and for each matches the 1-or-more CCI per STIG vulnerability to the NIST to CCI listing based on 
the XML file from DISA. It then generates a report of Open (STIG is open), Not Reviewed, or Not a Finding/Closed
(STIG is Not a Finding or N/A) grouped by the NIST major controls for the system based on the C-I-A 
low/moderate/high classification.

# API Calls

* GET to /system/{systemId} to generate the NIST major controls listing with all relevant STIG checklist data
* GET to /swagger/ gives you the API structure.
* GET to cci/{control} to return the CCIs related to a NIST higher level control

# Additional Information
More documentation is at https://github.com/Cingulara/openrmf-docs/.

# To Do
* [ ] Add Moq tests on static methods via a wrapper https://stackoverflow.com/questions/9052736/moq-a-static-method-in-static-class