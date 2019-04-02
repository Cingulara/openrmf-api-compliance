# openstig-api-compliance
This is the openSTIG Compliance API that reads in all system checklists via the openSTIG Read API, 
and for each matches the 1-or-more CCI per STIG vulnerability to the NIST to CCI listing based on 
the XML file from DISA. It then generates a report of Open (STIG is open or not reviewed or Closed
(STIG is Not a Finding or N/A) grouped by the NIST major controls for the system based on the C-I-A 
low/moderate/high classification.

GET to /system/systemId to generate the NIST major controls listing with all relevant STIG checklist data

/swagger/ gives you the API structure.

## Making your local Docker image
docker build --rm -t openstig-api-compliance:0.4 .
